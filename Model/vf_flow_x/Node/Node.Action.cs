using Vapula.Helper;

namespace Vapula.Flow
{
    public abstract partial class Node
    {
        public ILogger Logger 
        {
            get 
            {
                if (_Parent == null)
                    return null;
                return _Parent.Logger; 
            }
        }

        public abstract bool Valid();

        public abstract void Start();

        public abstract void Wait();
    }
}
