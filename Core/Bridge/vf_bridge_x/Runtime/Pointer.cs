using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace Vapula.Runtime
{
    public class Pointer
    {
        private IntPtr _Handle
            = IntPtr.Zero;

        public IntPtr Data 
        {
            get { return Bridge.GetPointerData(_Handle); }
        }

        public UInt32 Size 
        {
            get { return Bridge.GetPointerSize(_Handle); }
        }

        public T[] ReadArray<T>(UInt32 offset = 0)
            where T : struct
        {
            return default(T[]);
        }

        public void WriteArray<T>(T[] data, UInt32 offset = 0)
            where T : struct
        {
            UInt32 unit = (UInt32)Marshal.SizeOf(typeof(T));
            UInt32 size = (UInt32)(data.Length * unit);
            IntPtr temp = Bridge.NewData(size);
            //MemoryStream ms;
            //BinaryFormatter;
            //Marshal.Copy(data, 0, temp, data.Length);
        }
    }
}
