using Vapula.API;

namespace Vapula.Runtime
{
    /// <summary>
    /// 库坞
    /// </summary>
    public class LibraryHub
    {
        private static LibraryHub _Instance = null;

        public static LibraryHub Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new LibraryHub();
                return _Instance;
            }
        }

        private LibraryHub()
        {
        }

        public int Count
        {
            get { return Bridge.GetLibraryCount(); }
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
    }
}