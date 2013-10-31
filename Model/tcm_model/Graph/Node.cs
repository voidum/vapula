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
        protected List<Link> _InLinks 
            = new List<Link>();
        protected List<Link> _OutLinks 
            = new List<Link>();
        protected List<ParamStub> _ParamStubs
            = new List<ParamStub>();

        protected int _LSI = 0;
        protected bool _SPP = false;
        protected Node _SDN = null;
        
        protected object _Tag;
        protected ISyncable _SyncTarget;
        #endregion;

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

        public List<Link> InLinks
        {
            get { return _InLinks; }
        }

        public List<Link> OutLinks
        {
            get { return _OutLinks; }
        }

        public List<ParamStub> ParamStubs
        {
            get { return _ParamStubs; }
        }

        /// <summary>
        /// 获取或设置最新阶段标识
        /// </summary>
        public int LSI
        {
            get { return _LSI; }
            set { _LSI = value; }
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
        /// 获取或设置节点的附加数据
        /// </summary>
        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        /// <summary>
        /// 获取或设置同步目标
        /// </summary>
        public ISyncable SyncTarget
        {
            get { return _SyncTarget; }
            set { _SyncTarget = value; }
        }

        /// <summary>
        /// <para>获取节点是否就绪</para>
        /// <para>应当仅检验参数完备</para>
        /// </summary>
        public virtual bool IsReady
        {
            get { return false; }
        }
        #endregion

        #region 方法
        public virtual object Sync(string cmd, object attach)
        {
            return null;
        }

        public virtual void Dispose()
        {
            foreach (Link link in _InLinks)
                link.QuickSetter_To = null;
            _InLinks.Clear();
            foreach (Link link in _OutLinks)
                link.QuickSetter_From = null;
            _OutLinks.Clear();
            _SyncTarget = null;
        }
        #endregion
    }
}