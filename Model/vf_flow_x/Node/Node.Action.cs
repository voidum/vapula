using Vapula.Helper;

namespace Vapula.Flow
{
    public abstract partial class Node
    {
        public ILogger Logger 
        {
            get { return Parent.Logger; }
        }

        public abstract bool Valid();

        public abstract void Start();

        public abstract void Wait();
    }
}
