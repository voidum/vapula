namespace Vapula.Flow
{
    public abstract class NodeFactory
    {
        public abstract string Type { get; }

        public abstract Node CreateNode();
    }
}
