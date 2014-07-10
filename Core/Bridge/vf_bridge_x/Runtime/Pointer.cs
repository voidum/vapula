using System;
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

        public UInt32 Size
        {
            get { return (UInt32)_Size; }
        }

        public void Capture(IntPtr data, UInt32 size)
        {
            _Data = data;
            _Size = (int)size;
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
            if (offset == 0 && unit == 1)
            {
                T[] data = new T[_Size];
                PtrToArray(_Data, data);
                return data;
            }
            int size = _Size - offset;
            int length = size / unit;
            if (length > 0)
            {
                T[] data = new T[length];
                IntPtr data_offset =
                    Bridge.OffsetData(_Data, (UInt32)offset);
                PtrToArray(data_offset, data);
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
                if (_Data != IntPtr.Zero)
                {
                    Bridge.CopyData(data_new, _Data, (UInt32)_Size);
                    Bridge.DeleteData(_Data);
                }
                _Data = data_new;
                _Size = size_new;
            }
            IntPtr data_offset = Bridge.OffsetData(_Data, (UInt32)offset);
            ArrayToPtr(data, data_offset);
        }

        public T ReadValue<T>(int offset = 0)
            where T : struct
        {
            if (_Size <= offset)
                return default(T);
            int size = _Size - offset;
            if (size >= Marshal.SizeOf(typeof(T)))
            {
                T[] data = new T[1];
                PtrToArray<T>(
                    Bridge.OffsetData(_Data, (UInt32)offset), data);
                T value = data[0];
                return value;
            }
            return default(T);
        }

        public void WriteValue<T>(T value, int offset = 0)
            where T : struct
        {
            int size = _Size - offset;
            int unit = Marshal.SizeOf(typeof(T));
            if (size <= unit)
            {
                //expand data
                int size_new = unit + offset;
                IntPtr data_new = Bridge.NewData((UInt32)size_new);
                if (_Data != IntPtr.Zero)
                {
                    Bridge.CopyData(data_new, _Data, (UInt32)_Size);
                    Bridge.DeleteData(_Data);
                }
                _Data = data_new;
                _Size = size_new;
            }
            T[] data = new T[1] { value };
            IntPtr ptr = Bridge.NewData((UInt32)unit);
            ArrayToPtr(data, ptr);
            Bridge.CopyData(Bridge.OffsetData(_Data, (UInt32)offset), ptr, (UInt32)unit);
            Bridge.DeleteData(ptr);
        }

        private void PtrToArray<T>(IntPtr src, T[] dst)
            where T : struct
        {
            var type = typeof(T);
            if (type == typeof(byte))
                Marshal.Copy(src, (byte[])((object)dst), 0, dst.Length);
            else if (type == typeof(short))
                Marshal.Copy(src, (short[])((object)dst), 0, dst.Length);
            else if (type == typeof(int))
                Marshal.Copy(src, (int[])((object)dst), 0, dst.Length);
            else if (type == typeof(long))
                Marshal.Copy(src, (long[])((object)dst), 0, dst.Length);
            else if (type == typeof(float))
                Marshal.Copy(src, (float[])((object)dst), 0, dst.Length);
            else if (type == typeof(double))
                Marshal.Copy(src, (double[])((object)dst), 0, dst.Length);
            else if (type == typeof(char))
                Marshal.Copy(src, (char[])((object)dst), 0, dst.Length);
            else
                throw new NotImplementedException();
        }

        private void ArrayToPtr<T>(T[] src, IntPtr dst)
            where T : struct
        {
            var type = typeof(T);
            if (type == typeof(byte))
                Marshal.Copy((byte[])(object)src, 0, dst, src.Length);
            else if (type == typeof(short))
                Marshal.Copy((short[])(object)src, 0, dst, src.Length);
            else if (type == typeof(int))
                Marshal.Copy((int[])(object)src, 0, dst, src.Length);
            else if (type == typeof(long))
                Marshal.Copy((long[])(object)src, 0, dst, src.Length);
            else if (type == typeof(float))
                Marshal.Copy((float[])(object)src, 0, dst, src.Length);
            else if (type == typeof(double))
                Marshal.Copy((double[])(object)src, 0, dst, src.Length);
            else if (type == typeof(char))
                Marshal.Copy((char[])(object)src, 0, dst, src.Length);
            else
                throw new NotImplementedException();
        }
    }
}