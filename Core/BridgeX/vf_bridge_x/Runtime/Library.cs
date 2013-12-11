using System;
using TCM.API;

namespace TCM.Runtime
{
    public class Library : IDisposable
    {
        public static int Count
        {
            get { return Bridge.GetLibraryCount(); }
        }

        protected IntPtr _Handle;

        public string Id
        {
            get 
            {
                string value = 
                    Bridge.MarshalString(
                    Bridge.GetLibraryId(_Handle));
                return value;
            }
        }

        public string RuntimeId
        {
            get 
            { 
                string value = 
                    Bridge.MarshalString(
                    Bridge.GetLibraryRuntime(_Handle), false);
                return value;
            }
        }

        public Library(IntPtr handle)
        {
            _Handle = handle;
        }

        /// <summary>
        /// 从文件加载库
        /// </summary>
        public static Library Load(string path)
        {
            IntPtr ptr = Bridge.LoadLibrary(path);
            if (ptr == IntPtr.Zero) return null;
            Library lib = null;
            string runtime = 
                Bridge.MarshalString(
                Bridge.GetLibraryRuntime(ptr), false);
            if (runtime == Base.RuntimeId)
                lib = new LibraryCLR(ptr);
            else
                lib = new Library(ptr);
            return lib;
        }

		/// <summary>
        /// 获取指定功能的调用器
		/// </summary>
        public Invoker CreateInvoker(int fid)
        {
            IntPtr ptr = Bridge.CreateInvoker(_Handle, fid);
            Invoker inv = null;
            if (RuntimeId == Base.RuntimeId)
                inv = InvokerCLR.GetInvoker(ptr);
            else
                inv = new Invoker(ptr);
            return inv;
        }
		
		/// <summary>
		/// 装载库
		/// </summary>
        public virtual bool Mount()
        {
            return Bridge.MountLibrary(_Handle);
        }

		/// <summary>
        /// 卸载库
		/// </summary>
        public virtual void Unmount()
        {
            Bridge.UnmountLibrary(_Handle);
        }

        /// <summary>
        /// 销毁库
        /// </summary>
        public virtual void Dispose()
        {
            Unmount();
            Bridge.DeleteObject(_Handle);
        }
    }
}
