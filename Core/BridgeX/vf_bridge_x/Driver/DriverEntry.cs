using System;
using System.Runtime.InteropServices;
using Vapula.API;

namespace Vapula.Runtime
{
    public class DriverEntry
    {
        internal const int S_True = 1;
        internal const int S_False = 0;

        /// <summary>
        /// request to mount library
        /// </summary>
        /// <param name="arg">library handle + library path</param>
        public static int Mount(string arg)
        {
            string[] args = arg.Split(new char[] { '|' });
            IntPtr ptr = new IntPtr(long.Parse(args[0]));
            string path = args[1];
            LibraryCLR lib = LibraryCLR.GetLibrary(ptr);
            if (lib == null)
                lib = new LibraryCLR(ptr);
            if(!lib.MountEx(path))
                return S_False;
            return S_True;
        }

        /// <summary>
        /// request to unmount library
        /// </summary>
        /// <param name="arg">library handle</param>
        public static int Unmount(string arg)
        {
            IntPtr ptr = new IntPtr(int.Parse(arg));
            LibraryCLR lib = LibraryCLR.GetLibrary(ptr);
            lib.UnmountEx();
            return S_True;
        }

        /// <summary>
        /// request to init invoker
        /// </summary>
        /// <param name="arg">invoker handle + library handle</param>
        public static int InitInvoker(string arg)
        {
            string[] args = arg.Split(new char[] { '|' });
            IntPtr ptr1 = new IntPtr(int.Parse(args[0]));
            IntPtr ptr2 = new IntPtr(int.Parse(args[1]));
            LibraryCLR lib_clr = LibraryCLR.GetLibrary(ptr2);
            InvokerCLR inv = new InvokerCLR(ptr1, lib_clr);
            return S_True;
        }

        /// <summary>
        /// request to raise process
        /// </summary>
        /// <param name="arg">invoker handle</param>
        public static int OnProcess(string arg)
        {
            IntPtr ptr = new IntPtr(int.Parse(arg));
            InvokerCLR inv = InvokerCLR.GetInvoker(ptr);
            if (inv == null)
                return S_False;
            inv.OnProcess();
            return S_True;
        }

        /// <summary>
        /// request to raise rollback
        /// </summary>
        /// <param name="arg">invoker handle</param>
        public static int OnRollback(string arg)
        {
            IntPtr ptr = new IntPtr(int.Parse(arg));
            InvokerCLR inv = InvokerCLR.GetInvoker(ptr);
            if (inv == null)
                return S_False;
            inv.OnRollback();
            return S_True;
        }
    }
}
