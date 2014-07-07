using System;

namespace Vapula.Runtime
{
    public class Driver : Sartrey.DisposableObject
    {
        protected IntPtr _Handle 
            = IntPtr.Zero;

        protected Driver(IntPtr handle)
        {
            _Handle = handle;
        }

        /// <summary>
        /// load driver from file
        /// </summary>
        public static Driver Load(string path)
        {
            IntPtr handle = Bridge.LoadDriver(path);
            if (handle == IntPtr.Zero)
                return null;
            Driver driver = new Driver(handle);
            return driver;
        }

        public void LinkHub()
        {
            Bridge.LinkDriver(_Handle);
        }

        public void KickHub()
        {
            Bridge.KickDriver(_Handle);
        }

        protected override void DisposeManaged()
        {
        }

        protected override void DisposeNative()
        {
            if (_Handle != IntPtr.Zero)
            {
                Bridge.DeleteRaw(_Handle);
                _Handle = IntPtr.Zero;
            }
        }
    }
}
