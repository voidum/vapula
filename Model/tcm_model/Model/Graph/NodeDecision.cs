using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCM.Model
{
    public class NodeDecision : Node
    {
        public override NodeType Type
        {
            get { return NodeType.Decision; }
        }

        private string _Code;

    }
}
