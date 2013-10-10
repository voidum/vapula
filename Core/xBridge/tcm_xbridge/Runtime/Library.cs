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

		//获取指定功能的执行器
        public Executor CreateExecutor(int fid)
        {
            IntPtr ptr = Bridge.CreateExecutor(_Handle, fid);
            Executor exec = null;
            if (RuntimeId == Base.RuntimeId)
                exec = ExecutorCLR.GetExecutor(ptr);
            else
                exec = new Executor(ptr);
            return exec;
        }
		
		//装载库
        public virtual bool Mount()
        {
            return Bridge.MountLibrary(_Handle);
        }

		//卸载库
        public virtual void Unmount()
        {
            Bridge.UnmountLibrary(_Handle);
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
