﻿using System;
using System.Collections.Generic;

namespace TCM.Model
{
    public enum NodeType
    {
        Unknown = -1,
        Process = 0,
        Decision = 1
    }

    /// <summary>
    /// 模型图节点，对应有向图的顶点
    /// </summary>
    public class Node
    {
        protected NodeType _Type 
            = NodeType.Unknown;
        protected Stage _LastStage 
            = null;
        protected List<Link> _InLinks 
            = new List<Link>();
        protected List<Link> _OutLinks 
            = new List<Link>();

        public NodeType Type
        {
            get { return _Type; }
        }

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

        public virtual List<ParamProxy> Parameters
        {
            get { return new List<ParamProxy>(); }
        }

        /// <summary>
        /// 获取节点是否可执行
        /// </summary>
        public virtual bool Executable
        {
            get
            {
                for (int i=0;i<Parameters.Count;i++)
                {
                    var param = Parameters[i];
                    if (param.HasValue) continue;
                    foreach (var link in _InLinks)
                    {
                        if (link.HasMapTo(i))
                            if (link.From.LastStage == null)
                                return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// 执行节点
        /// </summary>
        public bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}