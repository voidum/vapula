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
        protected List<Link> _InLinks 
            = new List<Link>();
        protected List<Link> _OutLinks 
            = new List<Link>();
        protected List<ParamProxy> _Params
            = new List<ParamProxy>();

        protected Stage _LastStage
            = null;

        protected ISyncable _SyncTarget;
        #endregion;

        #region 属性
        public abstract NodeType Type { get; }

        public Stage LastStage
        {
            get { return _LastStage; }
        }

        public List<Link> InLinks
        {
            get { return _InLinks; }
        }

        public List<Link> OutLinks
        {
            get { return _OutLinks; }
        }

        public List<ParamProxy> Parameters
        {
            get { return _Params; }
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
            get
            {
                for (int i=0;i<_Params.Count;i++)
                {
                    var param = _Params[i];
                    if (param.HasValue) continue;
                    Link link_capture = null;
                    foreach (var link in _InLinks)
                    {
                        if (!link.HasMap(i, false))
                            continue;
                        if(link.From != null 
                            && link.From.LastStage != null)
                        {
                            link_capture = link;
                            break;
                        }
                    }
                    if (link_capture == null)
                        return false;
                }
                return true;
            }
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