using Vapula.API;

namespace Vapula.Runtime
{
    /// <summary>
    /// 驱动坞
    /// </summary>
    public class DriverHub
    {
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

        private DriverHub()
        {
        }

        public int Count
        {
            get { return Bridge.GetDriverCount(); }
        }

        public bool Link(string id)
        {
            return Bridge.LinkDriver(id);
        }

        public void Kick(string id)
        {
            Bridge.KickDriver(id);
        }

        public void KickAll()
        {
            Bridge.KickAllDrivers();
        }
    }
}
