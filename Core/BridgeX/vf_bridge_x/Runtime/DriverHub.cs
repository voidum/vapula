using System;
using TCM.API;
using System.Windows.Forms;

namespace TCM.Runtime
{
    /// <summary>
    /// 驱动板
    /// </summary>
    public class DriverHub : IDisposable
    {
        private DriverHub()
        {
        }

        private static DriverHub _Instance = null;
        public static DriverHub Instance
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new DriverHub();
                return _Instance;
            }
        }

        public int Count
        {
            get { return Bridge.GetDriverCount(); }
        }

        public bool Link(string id)
        {
            return Bridge.LinkDriver(id);
        }

        public bool Kick(string id)
        {
            return Bridge.KickDriver(id);
        }

        public void KickAll()
        {
            Bridge.KickAllDrivers();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
