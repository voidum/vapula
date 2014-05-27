using System;
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
        /// read data
        /// </summary>
        public IntPtr Read() 
        {
            return Bridge.ReadRecord(_Handle);
        }

        /// <summary>
        /// write data
        /// </summary>
        public void Write(IntPtr data, UInt32 size) 
        {
            Bridge.WriteRecord(_Handle, data, size);
        }

        /// <summary>
        /// read text
        /// </summary>
        public string ReadText()
        {
            IntPtr ptr = Read();
            string s = Bridge.ToStringUni(ptr);
            return s;
        }

        /// <summary>
        /// write text
        /// </summary>
        public void WriteText(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            string base64 = Convert.ToBase64String(bytes);
            IntPtr ptr = Bridge.Base64ToRaw(base64);
            Write(ptr, (uint)bytes.Length);
        }

        /// <summary>
        /// read data at offset (at) by type (T)
        /// </summary>
        public T ReadAt<T>(UInt32 at = 0) where T : struct 
        {
            IntPtr data = Read();
            ValueType type = Base.GetVapulaType(typeof(T));
            IntPtr ptr = Bridge.ReadAt(data, (Byte)type , at);
            string s = Bridge.ToStringAnsi(ptr);
            return Base.ConvertTo<T>(s);
        }

        /// <summary>
        /// <para>write data at offset (at) by type (T)</para>
        /// <para>write new data when size is not enough</para>
        /// </summary>
        public void WriteAt<T>(T data, UInt32 at = 0) where T : struct
        {
            ValueType type = Base.GetVapulaType(typeof(T));
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

        public void Deliver(Record to) 
        {
            Bridge.DeliverRecord(Handle, to.Handle);
        }
    }
}
