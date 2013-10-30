using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCM.Model
{
    /// <summary>
    /// 节点：决策
    /// </summary>
    public class NodeDecision : Node
    {
        private bool _Priority = false;

        public override NodeType Type
        {
            get { return NodeType.Decision; }
        }

        public override bool Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        private string _Code;

    }
}
