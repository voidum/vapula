using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vapula
{
    public enum CoreType
    {
        Driver = 0,
        Library = 1,
        Stack = 2,
        Aspect = 3
    };

    public enum State
    {
        Idle = 0,
        Queue = 1,
        BusyBack = 2,
        BusyFront = 3,
        Rollback = 4,
        Pause = 5
    }

    public enum ControlCode
    {
        Null = 0,
        Pause = 1,
        Resume = 2,
        Cancel = 3
    }

    public enum ReturnCode
    {
        Error = 0,
        Normal = 1,
        Cancel = 2,
        Terminate = 3,
        NullTask = 4,
        Unhandled = 5
    }

    public class Base
    {
        /// <summary>
        /// current runtime
        /// </summary>
        public const string RuntimeId = "clr";

        /// <summary>
        /// get runtime directory
        /// </summary>
        public static string RuntimeDir
        {
            get { return Sartrey.IOHelper.GetTypeDirectory(typeof(Base)); }
        }

        public static byte[] ToBytes(IntPtr ptr, int size) 
        {
            byte[] data = new byte[size];
            Marshal.Copy(ptr, data, 0, size);
            return data;
        }
        public static IntPtr ToIntPtr(byte[] data)
        {
            IntPtr ptr = Bridge.NewData((UInt32)data.Length);
            Marshal.Copy(data, 0, ptr, data.Length);
            return ptr;
        }

        /// <summary>
        /// convert IntPtr into unicode string
        /// </summary>
        public static string ToStringUni(IntPtr ptr)
        {
            return Marshal.PtrToStringUni(ptr);
        }

        /// <summary>
        /// convert IntPtr into ansi string
        /// </summary>
        public static string ToStringAnsi(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }
    }
}