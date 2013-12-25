using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Vapula.Model;

namespace Vapula.Flow
{
    public enum NodeType
    {
        Unknown = -1,
        Process = 0,
        Decision = 1,
        Start = 2,
        Variable = 3,
        Batch = 4, 
        Model = 5,
        Code = 6
    }

    /// <summary>
    /// 模型图节点，对应有向图的顶点
    /// </summary>
    public abstract partial class Node : IDisposable
    {
        #region 字段
        protected int _Id = -1;
        protected List<Node> _InNodes 
            = new List<Node>();
        protected List<Node> _OutNodes 
            = new List<Node>();
        protected List<ParamStub> _ParamStubs
            = new List<ParamStub>();
        protected Graph _Parent 
            = null;
        protected TagList _Attach 
            = null;
        #endregion

        #region 索引器
        /// <summary>
        /// 根据指定标识获取参数存根
        /// </summary>
        public ParamStub this[int id]
        {
            get
            {
                foreach (var stub in _ParamStubs)
                    if (stub.Self.ParamId == id) 
                        return stub;
                return null;
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置节点的标识
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
        /// 获取是否支持自定义参数
        /// </summary>
        public abstract bool CanCustomParam { get; }

        /// <summary>
        /// 获取节点的输入节点
        /// </summary>
        public List<Node> InNodes
        {
            get { return _InNodes; }
        }

        /// <summary>
        /// 获取节点的输出节点
        /// </summary>
        public List<Node> OutNodes 
        {
            get { return _OutNodes; }
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
        /// 获取节点的附加数据
        /// </summary>
        public TagList Attach
        {
            get 
            {
                if (_Attach == null)
                    _Attach = new TagList();
                return _Attach;
            }
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 由XML解析节点对象
        /// </summary>
        public static Node Parse(XElement xml)
        {
            return null;
        }

        /// <summary>
        /// 将组件序列化成XML元素
        /// </summary>
        public virtual XElement ToXML()
        {
            var xml = new XElement("node", 
                new XAttribute("id", _Id));
            return xml;
        }
        #endregion

        #region 方法
        public virtual void Dispose()
        {
            foreach (Node node in _InNodes)
                node.OutNodes.Remove(this);
            _InNodes.Clear();
            foreach (Node node in _OutNodes)
                node.InNodes.Remove(this);
            _OutNodes.Clear();
            _SyncTarget = null;
        }
        #endregion
    }
}