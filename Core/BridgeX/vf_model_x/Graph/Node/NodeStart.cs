namespace Vapula.Model
{
    /// <summary>
    /// 节点：起点
    /// </summary>
    public class NodeStart : Node
    {
        public override NodeType Type
        {
            get { return NodeType.Start; }
        }
    }
}
