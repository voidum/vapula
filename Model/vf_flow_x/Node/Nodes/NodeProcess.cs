using System.Collections.Generic;
using System.Threading;
using Vapula.Helper;
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
            }
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
                        Logger.WriteLog(LogType.Error,
                            string.Format("节点{0}的参数{1}没有指定数值",
                            _Id, param.Id));
                        return false;
                    }
                }
                else
                {
                    if (stub.Supply == ParamPoint.Null)
                    {
                        Logger.WriteLog(LogType.Error,
                            string.Format("节点{0}的参数{1}没有关联参数供给",
                            _Id, param.Id));
                        return false;
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
            var envelope = invoker.Envelope;
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
                envelope.Write(stub.Prototype.Id, stub.Value);
            }
            bool ret = invoker.Start();
            Logger.WriteLog(LogType.Event,
                string.Format("节点{0}的功能{1}启动",
                    _Id, _Function.Name,
                    ret ? "成功" : "失败"));
        }

        public override void Wait()
        {
            var invoker = Attach["Invoker"] as Invoker;
            while (invoker.Context.State != State.Idle)
                Thread.Sleep(50);
            var ret = invoker.Context.ReturnCode;
            if (ret == ReturnCode.Normal) 
            {
                foreach (var stub in ParamStubs)
                {
                    var param = stub.Prototype;
                    if (param.Mode != ParamMode.In)
                    {
                        stub.Value = invoker.Envelope.Read(param.Id);
                        Logger.WriteLog(LogType.Debug,
                            string.Format("节点{0}的参数{1}的值为{2}",
                                Id, param.Name,
                                invoker.Envelope.Read(param.Id)));
                    }
                }
            }
            else if (ret == ReturnCode.Error)
            {
                Logger.WriteLog(LogType.Debug,
                    string.Format("节点{0}执行错误", Id));
            }
            else if (ret == ReturnCode.NullTask) 
            {
                Logger.WriteLog(LogType.Debug,
                    string.Format("节点{0}调用具体功能前返回", Id));
            }
        }
    }
}
