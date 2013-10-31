using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCM.Model
{
    public class NodeBatch : Node
    {
        public override NodeType Type
        {
            get { return NodeType.Batch; }
        }

        private string _Code;
    }
}
