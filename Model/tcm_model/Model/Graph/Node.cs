using System;
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

        protected List<Link> _InLinks = null;
        protected List<Link> _OutLinks = null;
    }
}