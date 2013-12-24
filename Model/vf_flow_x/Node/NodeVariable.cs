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
    }
}
