using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Vapula.Model;
using Vapula.Runtime;

namespace Vapula.Flow
{
    /// <summary>
    /// 节点：处理
    /// </summary>
    public class NodeProcess : Node
    {
        private Function _Function 
            = null;
        
        private string _LibraryId = null;
        private string _FunctionId = null;

        public override NodeType Type
        {
            get { return NodeType.Process; }
        }

        public override bool CanCustomParam
        {
            get { return false; }
        }

        public override List<Parameter> Parameters
        {
            get { return _Function.Parameters; }
        }

        public Function Function
        {
            get { return _Function; }
            set
            {
                if (_Function == value)
                    return;
                _ParamStubs.Clear();
                foreach (var param in value.Parameters)
                {
                    var stub = new ParamStub();
                    stub.Parent = this;
                    stub.Prototype = param;
                    _ParamStubs.Add(stub);
                }
                _Function = value;
                _LibraryId = _Function.Library.Id;
                _FunctionId = _Function.Id;
            }
        }

        public string LibraryId
        {
            get { return _LibraryId; }
        }

        public string FunctionId
        {
            get { return _FunctionId; }
        }

        public override object Sync(string cmd, object attach)
        {
            if (cmd == "get-icon") 
            {
                if(_Function.Attach["SmallIcon"] != null)
                    return _Function.Attach["SmallIcon"];
            }
            else if(cmd == "get-name")
                return _Function.Name;
            return base.Sync(cmd, attach);
        }

        public override bool Valid()
        {
            foreach (var stub in ParamStubs)
            {
                var param = stub.Prototype;
                if (param.Mode == ParamMode.Out)
                    continue;
                if (stub.IsExport)
                {
                    if (!stub.HasValue)
                    {
                        throw new Exception(
                            string.Format("节点{0}的参数{1}没有指定数值",
                            _Id, param.Id));
                    }
                }
                else
                {
                    if (stub.Supply == ParamPoint.Null)
                    {
                        throw new Exception(
                            string.Format("节点{0}的参数{1}没有关联参数供给",
                            _Id, param.Id));
                    }
                }
            }
            return true;
        }

        public override void Start()
        {
            var library = Runtime.Library.Load(_Function.Library.Path);
            library.Mount();
            var invoker = library.CreateInvoker(_Function.Id);
            Attach["Invoker"] = invoker;
            var stack = invoker.Stack;
            var env = stack.Envelope;
            foreach(var stub in ParamStubs)
            {
                var param = stub.Prototype;
                if (param.Mode == ParamMode.Out)
                    continue;
                if (!stub.IsExport)
                {
                    var stub_supply = Parent.FindParamStub(stub.Supply);
                    stub.Value = stub_supply.Value;
                }
                env.Write(stub.Prototype.Id, stub.Value);
            }
            bool ret = invoker.Start();
            if(!ret)
                throw new Exception(
                    string.Format("节点{0}的功能{1}启动失败", _Id, _Function.Name));
        }

        public override void Wait()
        {
            var invoker = Attach["Invoker"] as Invoker;
            var stack = invoker.Stack;
            var ctx = stack.Context;
            while (ctx.CurrentState != State.Idle)
                Thread.Sleep(50);
        }

        public static NodeProcess Parse(XElement xml)
        {
            var node = new NodeProcess();
            node._Id = int.Parse(xml.Attribute("id").Value);
            node._LibraryId = xml.Element("library").Value;
            node._FunctionId = xml.Element("function").Value;
            return node;
        }

        public override XElement ToXML()
        {
            var xml = base.ToXML();
            xml.Add(new XElement("library", _LibraryId));
            xml.Add(new XElement("function", _FunctionId));
            return xml;
        }
    }
}
