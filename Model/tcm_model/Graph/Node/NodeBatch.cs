using System;

namespace TCM.Model
{
    public class NodeBatch : Node
    {
        public override NodeType Type
        {
            get { return NodeType.Batch; }
        }
    }
}
