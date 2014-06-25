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

        /// <summary>
        /// count core objects
        /// </summary>
        public int CountObjects(CoreType type)
        {
            return Bridge.CountObjects((byte)type);
        }

        /// <summary>
        /// link object into runtime
        /// </summary>
        public void LinkObject(IntPtr handle)
        {
            Bridge.LinkObject(handle);
        }

        /// <summary>
        /// kick out object from runtime
        /// </summary>
        public void KickObject(CoreType type, string id)
        {
            Bridge.KickObject((byte)type, id);
        }

        /// <summary>
        /// kick out all objects
        /// </summary>
        public void KickAllObects(CoreType type)
        {
            Bridge.KickAllObjects((byte)type);
        }
    }
}
