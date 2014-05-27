using System;

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
            Library lib = new Library(ptr);
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
                    Bridge.ToStringAnsi(
                    Bridge.GetLibraryId(_Handle));
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
                    Bridge.ToStringAnsi(
                    Bridge.GetRuntime(_Handle));
                return value;
            }
        }

		/// <summary>
        /// create task for method
		/// </summary>
        public Task CreateTask(string id)
        {
            IntPtr ptr = Bridge.CreateTask(_Handle, id);
            Task inv = new Task(ptr);
            return inv;
        }

        /// <summary>
        /// get method process symbol
        /// </summary>
        public string GetProcessSym(string id)
        {
            string sym = 
                Bridge.ToStringAnsi(
                Bridge.GetProcessSym(_Handle, id));
            return sym;
        }

        /// <summary>
        /// get method rollback symbol
        /// </summary>
        public string GetRollbackSym(string id)
        {
            string sym =
                Bridge.ToStringAnsi(
                Bridge.GetRollbackSym(_Handle, id));
            return sym;
        }

		/// <summary>
		/// mount library
		/// </summary>
        public bool Mount()
        {
            return Bridge.MountLibrary(_Handle);
        }

		/// <summary>
        /// unmount library
		/// </summary>
        public void Unmount()
        {
            Bridge.UnmountLibrary(_Handle);
        }

        /// <summary>
        /// dispose library
        /// </summary>
        public void Dispose()
        {
            Unmount();
            Bridge.DeleteRaw(_Handle);
        }
    }
}
