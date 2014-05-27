using System;

namespace Vapula.Driver
{
    public class Entry
    {
        internal const int S_True = 1;
        internal const int S_False = 0;

        /// <summary>
        /// request to mount library
        /// </summary>
        /// <param name="arg">library handle + library path + library id</param>
        public static int Mount(string arg)
        {
            string[] args = arg.Split(new char[] { '|' });
            IntPtr ptr = new IntPtr(long.Parse(args[0]));
            string path = args[1];
            Stub stub = Stub.GetStub(ptr);
            if (stub == null)
                stub = new Stub(ptr);
            if (!stub.Mount(path, args[2]))
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
            Stub stub = Stub.GetStub(ptr);
            stub.Unmount();
            return S_True;
        }

        /// <summary>
        /// request to invoke process
        /// </summary>
        /// <param name="arg">library handle + process sym</param>
        public static int OnProcess(string arg)
        {
            string[] args = arg.Split(new char[] { '|' });
            IntPtr ptr = new IntPtr(int.Parse(args[0]));
            Stub stub = Stub.GetStub(ptr);
            if (stub == null)
                return S_False;
            stub.OnProcess(args[1]);
            return S_True;
        }

        /// <summary>
        /// request to invoke rollback
        /// </summary>
        /// <param name="arg">library handle + rollback sym</param>
        public static int OnRollback(string arg)
        {
            string[] args = arg.Split(new char[] { '|' });
            IntPtr ptr = new IntPtr(int.Parse(args[0]));
            Stub stub = Stub.GetStub(ptr);
            if (stub == null)
                return S_False;
            stub.OnRollback(args[1]);
            return S_True;
        }
    }
}
