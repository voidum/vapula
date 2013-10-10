using System;
using System.Windows.Forms;
using TCM.API;

namespace TCM.Runtime
{
    public class DriverEntry
    {
        /// <summary>
        /// Library Handle, Path
        /// </summary>
        public static int Mount(string arg)
        {
            string[] args = arg.Split(new char[] { '|' });
            IntPtr ptr = new IntPtr(int.Parse(args[0]));
            string path = args[1];
            LibraryCLR lib = LibraryCLR.GetLibrary(ptr);
            if (lib == null) lib = new LibraryCLR(ptr);
            if(!lib.MountEx(path)) return 1;
            return 0;
        }

        /// <summary>
        /// Library Handle
        /// </summary>
        public static int Unmount(string arg)
        {
            IntPtr ptr = new IntPtr(int.Parse(arg));
            LibraryCLR lib = LibraryCLR.GetLibrary(ptr);
            lib.UnmountEx();
            return 0;
        }

        /// <summary>
        /// Executor Handle, Library Handle
        /// </summary>
        public static int InitExec(string arg)
        {
            string[] args = arg.Split(new char[] { '|' });
            IntPtr ptr1 = new IntPtr(int.Parse(args[0]));
            IntPtr ptr2 = new IntPtr(int.Parse(args[1]));
            LibraryCLR lib_clr = LibraryCLR.GetLibrary(ptr2);
            ExecutorCLR exec = new ExecutorCLR(ptr1, lib_clr);
            return 0;
        }

        /// <summary>
        /// Library Handle
        /// </summary>
        public static int CallEntry(string arg)
        {
            IntPtr ptr = new IntPtr(int.Parse(arg));
            ExecutorCLR exec = ExecutorCLR.GetExecutor(ptr);
            if (exec == null) return (int)ReturnCode.NullEntry;
            int retcode = exec.CallEntry();
            return retcode;
        }
    }
}
