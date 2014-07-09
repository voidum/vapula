using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vapula.Runtime
{
    public class Pointer
    {
        private IntPtr _Data
            = IntPtr.Zero;
        private int _Size 
            = 0;

        public IntPtr Data 
        {
            get { return _Data; }
        }

        public int Size 
        {
            get { return _Size; }
        }

        public void Capture(IntPtr data, int size) 
        {
            _Data = data;
            _Size = size;
        }

        public void Release() 
        {
            _Data = IntPtr.Zero;
            _Size = 0;
        }

        public T[] ReadArray<T>(int offset = 0)
            where T : struct
        {
            if (_Size <= offset)
                return default(T[]);
            int unit = Marshal.SizeOf(typeof(T));
            if (offset == 0 &&  unit == 1)
            {
                T[] data = new T[_Size];
                Marshal.PtrToStructure(_Data, data);
                return data;
            }
            int size = _Size - offset;
            int length = size / unit;
            if (length > 0)
            {
                T[] data = new T[size];
                IntPtr data_offset = 
                    Bridge.OffsetData(_Data, (UInt32)offset);
                Marshal.PtrToStructure(data_offset, data);
                return data;
            }
            return default(T[]);
        }

        public void WriteArray<T>(T[] data, int offset = 0)
            where T : struct
        {
            if (data == null)
                return;
            int size_need = Marshal.SizeOf(typeof(T)) * data.Length;
            int size = _Size - offset;
            if (size < size_need)
            {
                int size_new = size_need + offset;
                IntPtr data_new = Bridge.NewData((UInt32)size_new);
                if (_Data != null)
                {
                    Bridge.CopyData(data_new, _Data, (UInt32)_Size);
                    Bridge.DeleteData(_Data);
                }
                _Data = data_new;
                _Size = size_new;
            }
            IntPtr data_offset = Bridge.OffsetData(_Data, (UInt32)offset);
            Marshal.StructureToPtr(data, data_offset, false);
        }
    }
}
