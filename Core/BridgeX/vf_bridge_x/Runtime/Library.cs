using System;
using Vapula.API;

namespace Vapula.Runtime
{
    public class Library : IDisposable
    {
        protected IntPtr _Handle 
            = IntPtr.Zero;

        public Library(IntPtr handle)
        {
            _Handle = handle;
        }

        /// <summary>
        /// load library from file
        /// </summary>
        public static Library Load(string path)
        {
            IntPtr ptr = Bridge.LoadLibrary(path);
            if (ptr == IntPtr.Zero)
                return null;
            Library lib = null;
            string runtime =
                Bridge.ToString(
                Bridge.GetRuntime(ptr), false);
            if (runtime == Base.RuntimeId)
                lib = new LibraryCLR(ptr);
            else
                lib = new Library(ptr);
            return lib;
        }

        /// <summary>
        /// get library id
        /// </summary>
        public string Id
        {
            get 
            {
                string value = 
                    Bridge.ToString(
                    Bridge.GetLibraryId(_Handle), false);
                return value;
            }
        }

        /// <summary>
        /// get runtime id
        /// </summary>
        public string RuntimeId
        {
            get 
            { 
                string value = 
                    Bridge.ToString(
                    Bridge.GetRuntime(_Handle), false);
                return value;
            }
        }

		/// <summary>
        /// create invoker for method
		/// </summary>
        public Invoker CreateInvoker(string id)
        {
            IntPtr ptr = Bridge.CreateInvoker(_Handle, id);
            Invoker inv = null;
            if (RuntimeId == Base.RuntimeId)
                inv = InvokerCLR.GetInvoker(ptr);
            else
                inv = new Invoker(ptr);
            return inv;
        }

        /// <summary>
        /// get method process symbol
        /// </summary>
        public string GetProcessSym(string id)
        {
            string sym = 
                Bridge.ToString(
                Bridge.GetProcessSym(_Handle, id), false);
            return sym;
        }

        /// <summary>
        /// get method rollback symbol
        /// </summary>
        public string GetRollbackSym(string id)
        {
            string sym =
                Bridge.ToString(
                Bridge.GetRollbackSym(_Handle, id), false);
            return sym;
        }

		/// <summary>
		/// mount library
		/// </summary>
        public virtual bool Mount()
        {
            return Bridge.MountLibrary(_Handle);
        }

		/// <summary>
        /// unmount library
		/// </summary>
        public virtual void Unmount()
        {
            Bridge.UnmountLibrary(_Handle);
        }

        /// <summary>
        /// dispose library
        /// </summary>
        public virtual void Dispose()
        {
            Unmount();
            Bridge.DeleteRaw(_Handle);
        }
    }
}
