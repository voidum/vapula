using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Vapula.Flow;

namespace Vapula.Flow
{
    public enum NodeType
    {
        Process = 0,
        Decision = 1,
        Start = 2,
        Variable = 3,
        Batch = 4, 
        Model = 5,
        Code = 6
    }

    /// <summary>
    /// node of flow model, DG vertex
    /// </summary>
    public abstract partial class Node : Sartrey.DisposableObject
    {
        #region Fields
        protected int _Id = -1;
        protected List<Link> _InLinks 
            = new List<Link>();
        protected List<Link> _OutLinks 
            = new List<Link>();
        protected Graph _Container 
            = null;
        protected Sartrey.Table<object> _Attach 
            = null;
        #endregion

        #region Indexer
        #endregion

        #region Properties
        /// <summary>
        /// get node id
        /// </summary>
        public int Id
        {
            get 
            {
                if (_Id < 1)
                    throw new Exception("未设置节点标识");
                return _Id; 
            }
        }

        /// <summary>
        /// 获取节点所属图
        /// 设置所属图会执行完备附加操作
        /// </summary>
        public Graph Parent
        {
            get 
            {
                if (_Parent == null)
                    throw new Exception("未设置节点所属图");
                return _Parent;
            }
            set
            {
                if (value == null)
                    return;
                if (value.Nodes.Contains(this))
                    return;
                _Parent = value;
                _Id = _Parent.NewNodeId;
                _Parent.Nodes.Add(this);
            }
        }

        /// <summary>
        /// 获取节点的类型
        /// </summary>
        public abstract NodeType Type { get; }

        /// <summary>
        /// get links into node
        /// </summary>
        public List<Link> InLinks
        {
            get { return _InLinks; }
        }

        /// <summary>
        /// get links out of nodes
        /// </summary>
        public List<Link> OutLinks
        {
            get { return _OutLinks; }
        }

        /// <summary>
        /// 获取节点的参数集合
        /// </summary>
        public abstract List<Parameter> Parameters { get; }

        /// <summary>
        /// 获取节点的参数存根集合
        /// </summary>
        public List<ParamStub> ParamStubs
        {
            get { return _ParamStubs; }
        }

        /// <summary>
        /// get attached data
        /// </summary>
        public Sartrey.Table<object> Attach
        {
            get 
            {
                if (_Attach == null)
                    _Attach = new Sartrey.Table<object>();
                return _Attach;
            }
        }
        #endregion

        #region Serialization
        /// <summary>
        /// 由XML解析节点对象
        /// </summary>
        public static Node Parse(XElement xml)
        {
            var type = (NodeType)int.Parse(xml.Element("type").Value);
            switch (type)
            {
                case NodeType.Process:
                    return NodeProcess.Parse(xml);
                case NodeType.Start:
                    return NodeStart.Parse(xml);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 将组件序列化成XML元素
        /// </summary>
        public virtual XElement ToXML()
        {
            var xml = new XElement("node", 
                new XAttribute("id", _Id),
                new XElement("type", (int)Type));
            var xe_params = new XElement("params");
            foreach (var stub in _ParamStubs)
                xe_params.Add(stub.ToXML());
            xml.Add(xe_params);
            return xml;
        }
        #endregion

        protected override void DisposeManaged()
        {
            foreach (var node in _InNodes)
                node.OutNodes.Remove(this);
            _InNodes.Clear();
            foreach (var node in _OutNodes)
                node.InNodes.Remove(this);
            _OutNodes.Clear();
            _SyncTarget = null;
        }

        protected override void DisposeNative()
        {
        }
    }
}