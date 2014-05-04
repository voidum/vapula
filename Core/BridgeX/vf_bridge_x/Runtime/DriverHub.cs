using Vapula.API;

namespace Vapula.Runtime
{
    /// <summary>
    /// hub for driver
    /// </summary>
    public class DriverHub
    {
        private static DriverHub _Instance 
            = null;
        private static readonly object _SyncCtor 
            = new object();

        public static DriverHub Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_SyncCtor) 
                    {
                        _Instance = new DriverHub();
                    }
                }
                return _Instance;
            }
        }

        private DriverHub()
        {
        }

        /// <summary>
        /// get driver count
        /// </summary>
        public int Count
        {
            get { return Bridge.GetDriverCount(); }
        }

        /// <summary>
        /// link driver into hub
        /// </summary>
        /// <param name="id">driver id</param>
        public bool Link(string id)
        {
            return Bridge.LinkDriver(id);
        }

        /// <summary>
        /// kick out driver from hub
        /// </summary>
        /// <param name="id">driver id</param>
        public void Kick(string id)
        {
            Bridge.KickDriver(id);
        }

        /// <summary>
        /// kick out all drivers
        /// </summary>
        public void KickAll()
        {
            Bridge.KickAllDrivers();
        }
    }
}
