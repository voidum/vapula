using System;

namespace TCM.Model
{
    public class NodeProcess : Node
    {
        private Function _Function 
            = null;

        public Function Function
        {
            get { return _Function; }
            set { _Function = value; }
        }

        public override NodeType Type
        {
            get { return NodeType.Process; }
        }
    }
}
