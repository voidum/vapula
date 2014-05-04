using Vapula.API;

namespace Vapula.Runtime
{
    public class Weaver
    {
        private static Weaver _Instance 
            = null;
        private static readonly object _SyncCtor 
            = new object();

        public static Weaver Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_SyncCtor) 
                    {
                        _Instance = new Weaver();
                    }
                }
                return _Instance;
            }
        }

        private Weaver()
        {
        }

        /// <summary>
        /// link aspect from file
        /// </summary>
        public bool Link(string path)
        {
            return Bridge.LinkAspect(path);
        }

        /// <summary>
        /// reach key frame
        /// </summary>
        public void Reach(string frame) 
        {
            Bridge.ReachFrame(frame);
        }
    }
}
