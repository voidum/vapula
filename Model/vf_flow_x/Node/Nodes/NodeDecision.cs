
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
