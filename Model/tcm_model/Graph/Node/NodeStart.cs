namespace TCM.Model
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

        public override object Sync(string cmd, object attach)
        {
            if (cmd == "get-text")
            {
                return "起点";
            }
            else if (cmd == "get-id")
            {
                return "节点" + Id.ToString();
            }
            return base.Sync(cmd, attach);
        }
    }
}
