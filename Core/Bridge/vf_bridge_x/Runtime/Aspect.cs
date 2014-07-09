using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vapula.Runtime
{
    public class Aspect : Sartrey.DisposableObject
    {
        protected IntPtr _Handle 
            = IntPtr.Zero;

        protected Aspect(IntPtr handle)
        {
            _Handle = handle;
        }

        /// <summary>
        /// load aspect from file
        /// </summary>
        public static Aspect Load(string path)
        {
            IntPtr handle = Bridge.LoadAspect(path);
            if (handle == IntPtr.Zero)
                return null;
            Aspect aspect = new Aspect(handle);
            return aspect;
        }

        public void Link() 
        {
            Bridge.LinkAspect(_Handle);
        }

        public void Kick()
        {
            Bridge.KickAspect(_Handle);
        }

        protected override void DisposeManaged()
        {
        }

        protected override void DisposeNative()
        {
            if (_Handle != IntPtr.Zero)
            {
                Bridge.DeleteData(_Handle);
                _Handle = IntPtr.Zero;
            }
        }
    }
}
