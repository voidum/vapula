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

        protected ISyncable _SyncTarget;
        #endregion;

        #region 属性
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

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

        public ISyncable SyncTarget
        {
            get { return _SyncTarget; }
            set { _SyncTarget = value; }
        }

        /// <summary>
        /// 获取节点是否就绪
        /// </summary>
        public virtual bool IsReady
        {
            get { return false; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 执行节点
        /// </summary>
        public bool Execute()
        {
            throw new NotImplementedException();
        }

        public void Sync(string cmd, object attach)
        {
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