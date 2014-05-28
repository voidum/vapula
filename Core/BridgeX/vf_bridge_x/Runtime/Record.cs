using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Vapula.Runtime
{
    public class Record
    {
        private IntPtr _Handle = IntPtr.Zero;

        /// <summary>
        /// get handle
        /// </summary>
        public IntPtr Handle
        {
            get { return _Handle; }
        }

        public Record(IntPtr handle)
        {
            _Handle = handle;
        }

        /// <summary>
        /// get size of record
        /// </summary>
        public UInt32 Size 
        {
            get { return Bridge.GetRecordSize(_Handle); }
        }

        /// <summary>
        /// write data
        /// </summary>
        public void Write(IntPtr data, UInt32 size)
        {
            Bridge.WriteRecord(_Handle, data, size);
        }

        /// <summary>
        /// read data
        /// </summary>
        public IntPtr Read() 
        {
            return Bridge.ReadRecord(_Handle);
        }

        /// <summary>
        /// write text
        /// </summary>
        public void WriteText(string text)
        {
            byte[] bytes_src = Encoding.Unicode.GetBytes(text);
            byte[] bytes_dst = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, bytes_src);
            IntPtr ptr = Bridge.NewData((byte)ValueType.UInt8, (uint)bytes_dst.Length);
            Marshal.Copy(bytes_dst, 0, ptr, bytes_dst.Length);
            Write(ptr, (uint)bytes_dst.Length);
            Bridge.DeleteRaw(ptr);
        }

        /// <summary>
        /// read text
        /// </summary>
        public string ReadText()
        {
            IntPtr ptr = Read();
            byte[] bytes_src = new byte[Size];
            byte[] bytes_dst = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, bytes_src);
            string text = Encoding.Unicode.GetString(bytes_dst);
            return text;
        }

        /// <summary>
        /// <para>write data at offset (at) by type (T)</para>
        /// <para>write new data when size is not enough</para>
        /// </summary>
        public void WriteAt<T>(T data, UInt32 at = 0) where T : struct
        {
            ValueType type = Base.GetValueType(typeof(T));
            uint x = (at + 1) * Bridge.GetValueUnit((byte)type);
            IntPtr data_all = IntPtr.Zero;
            if (Size >= x)
            {
                data_all = Read();
                Bridge.WriteAt(data_all, (byte)type, at, data.ToString());
            }
            else
            {
                data_all = Bridge.NewData((byte)type, x);
                Bridge.WriteAt(data_all, (byte)type, at, data.ToString());
                Write(data_all, x);
                Bridge.DeleteRaw(data_all);
            }
        }

        /// <summary>
        /// read data at offset (at) by type (T)
        /// </summary>
        public T ReadAt<T>(UInt32 at = 0) where T : struct
        {
            IntPtr data = Read();
            ValueType type = Base.GetValueType(typeof(T));
            IntPtr ptr = Bridge.ReadAt(data, (Byte)type, at);
            string s = Base.ToStringAnsi(ptr);
            return Base.ConvertTo<T>(s);
        }

        public void Deliver(Record to) 
        {
            Bridge.DeliverRecord(Handle, to.Handle);
        }
    }
}
