using Sartrey;
using System;

namespace Vapula.Runtime
{
    /// <summary>
    /// runtime
    /// </summary>
    public class Runtime
    {
        private static Runtime _Instance 
            = null;
        private static readonly object _CtorLock 
            = new object();

        public static Runtime Instance
        {
            get
            {
                if (_Instance == null) {
                    lock (_CtorLock)  {
                        if (_Instance == null)
                            _Instance = new Runtime();
                    }
                }
                return _Instance;
            }
        }

        private Runtime()
        {
        }

        public void Start() 
        {
            Bridge.StartRuntime();
        }

        public void Stop() 
        {
            Bridge.StopRuntime();
        }
    }
}
