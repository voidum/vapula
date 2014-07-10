using System;

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
        public void Write(IntPtr data, UInt32 size, bool copy = false)
        {
            Bridge.WriteRecord(_Handle, data, size, copy);
        }

        /// <summary>
        /// read data
        /// </summary>
        public IntPtr Read(bool copy = false) 
        {
            return Bridge.ReadRecord(_Handle, copy);
        }
    }
}
