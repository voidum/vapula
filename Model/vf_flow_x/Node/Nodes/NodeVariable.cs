using System;

namespace Vapula.Flow
{
    /// <summary>
    /// 节点：变量
    /// </summary>
    public class NodeVariable : Node
    {
        public override NodeType Type
        {
            get { return NodeType.Variable; }
        }

        public override bool Valid()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Wait()
        {
            throw new NotImplementedException();
        }
    }
}
