using System;

namespace Vapula.Runtime
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
        /// Invoker Handle, Library Handle
        /// </summary>
        public static int InitInvoker(string arg)
        {
            string[] args = arg.Split(new char[] { '|' });
            IntPtr ptr1 = new IntPtr(int.Parse(args[0]));
            IntPtr ptr2 = new IntPtr(int.Parse(args[1]));
            LibraryCLR lib_clr = LibraryCLR.GetLibrary(ptr2);
            InvokerCLR inv = new InvokerCLR(ptr1, lib_clr);
            return 0;
        }

        /// <summary>
        /// Library Handle
        /// </summary>
        public static int CallEntry(string arg)
        {
            IntPtr ptr = new IntPtr(int.Parse(arg));
            InvokerCLR inv = InvokerCLR.GetInvoker(ptr);
            if (inv == null)
                return (int)ReturnCode.NullEntry;
            int retcode = inv.CallEntry();
            return retcode;
        }
    }
}
