using System.Collections.Generic;
using Vapula.Model;

namespace Vapula.Flow
{
    /// <summary>
    /// 节点：决策
    /// </summary>
    public class NodeDecision : Node
    {
        private string _Code;

        public override NodeType Type
        {
            get { return NodeType.Decision; }
        }

        public override bool CanCustomParam
        {
            get { return true; }
        }

        public override List<Parameter> Parameters
        {
            get { return new List<Parameter>(); }
        }

        public override bool Valid()
        {
            throw new System.NotImplementedException();
        }

        public override void Start()
        {
            throw new System.NotImplementedException();
        }

        public override void Wait()
        {
            throw new System.NotImplementedException();
        }
    }
}
