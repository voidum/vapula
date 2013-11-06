using System;
using System.Collections.Generic;

namespace TCM.Model
{
    public enum NodeType
    {
        Unknown = -1,
        Process = 0,
        Decision = 1,
        Variable = 2,
        Batch = 3
    }

    /// <summary>
    /// 模型图节点，对应有向图的顶点
    /// </summary>
    public abstract class Node : IDisposable, ISyncable
    {
        #region 字段
        protected int _Id = -1;
        protected List<Node> _InNodes 
            = new List<Node>();
        protected List<Node> _OutNodes 
            = new List<Node>();
        protected List<ParamStub> _ParamStubs
            = new List<ParamStub>();

        protected bool _SPP = false;
        protected Node _SDN = null;
        protected int _LSI = 0;

        protected Tag _Tag = new Tag();
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置节点的标识
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        
        /// <summary>
        /// 获取节点的类型
        /// </summary>
        public abstract NodeType Type { get; }

        public List<Node> InNodes
        {
            get { return _InNodes; }
        }

        public List<Node> OutNodes 
        {
            get { return _OutNodes; }
        }

        /// <summary>
        /// 获取节点的参数存根集合
        /// </summary>
        public List<ParamStub> ParamStubs
        {
            get { return _ParamStubs; }
        }

        /// <summary>
        /// 获取或设置起点优先权
        /// </summary>
        public bool SPP
        {
            get { return _SPP; }
            set { _SPP = value; }
        }

        /// <summary>
        /// 获取或设置强依赖节点
        /// </summary>
        public Node SDN 
        {
            get { return _SDN; }
            set { _SDN = value; }
        }

        /// <summary>
        /// 获取或设置最新阶段的标识
        /// </summary>
        public int LSI
        {
            get { return _LSI; }
            set { _LSI = value; }
        }

        /// <summary>
        /// 获取或设置节点的附加数据
        /// </summary>
        public Tag Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
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
            _Tag.Dispose();
            _SyncTarget = null;
        }
        #endregion

        #region ISyncable
        protected ISyncable _SyncTarget;

        /// <summary>
        /// 获取或设置同步目标
        /// </summary>
        public ISyncable SyncTarget
        {
            get { return _SyncTarget; }
            set { _SyncTarget = value; }
        }

        public virtual object Sync(string cmd, object attach)
        {
            return null;
        }
        #endregion
    }
}