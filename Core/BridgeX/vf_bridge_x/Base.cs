using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vapula
{
    public enum DataType
    {
        Raw = 0,
        Value = 1,
        Text = 2
    };

    public enum ValueType
    {
        Int8 = 1,
        Int16 = 2,
        Int32 = 3,
        Int64 = 4,
        UInt8 = 5,
        UInt16 = 6,
        UInt32 = 7,
        UInt64 = 8,
        Real32 = 10,
        Real64 = 11,
    }

    public enum AccessMode
    {
        In = 0,
        Out = 1,
        InOut = 2
    }

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
    public enum CoreObject
    {
        Unknown = 0,
        Driver = 1,
        Library = 2,
        Stack = 3,
        Aspect = 4
    };

    public class Base
    {
        /// <summary>
        /// current runtime
        /// </summary>
        public const string RuntimeId = "clr";

        /// <summary>
        /// get directory for assembly with type
        /// </summary>
        public static string GetTypeDir(Type type)
        {
            return Path.GetDirectoryName(type.Assembly.Location);
        }

        /// <summary>
        /// get runtime directory
        /// </summary>
        public static string RuntimeDir
        {
            get { return GetTypeDir(typeof(Base)); }
        }

        /// <summary>
        /// get CLR type for Vapula value type
        /// </summary>
        public static Type GetCLRType(ValueType type)
        {
            switch (type)
            {
                case ValueType.Int8: return typeof(char);
                case ValueType.Int16: return typeof(short);
                case ValueType.Int32: return typeof(int);
                case ValueType.Int64: return typeof(long);
                case ValueType.UInt8: return typeof(byte);
                case ValueType.UInt16: return typeof(ushort);
                case ValueType.UInt32: return typeof(uint);
                case ValueType.UInt64: return typeof(ulong);
                case ValueType.Real32: return typeof(float);
                case ValueType.Real64: return typeof(double);
                default: return typeof(IntPtr);
            }
        }

        /// <summary>
        /// get CLR nullable for Vapula value type
        /// </summary>
        public static Type GetNullableCLRType(ValueType type)
        {
            switch (type)
            {
                case ValueType.Int8: return typeof(Nullable<char>);
                case ValueType.Int16: return typeof(Nullable<short>);
                case ValueType.Int32: return typeof(Nullable<int>);
                case ValueType.Int64: return typeof(Nullable<long>);
                case ValueType.UInt8: return typeof(Nullable<byte>);
                case ValueType.UInt16: return typeof(Nullable<ushort>);
                case ValueType.UInt32: return typeof(Nullable<uint>);
                case ValueType.UInt64: return typeof(Nullable<ulong>);
                case ValueType.Real32: return typeof(Nullable<float>);
                case ValueType.Real64: return typeof(Nullable<double>);
                default: return typeof(Nullable<IntPtr>);
            }
        }

        /// <summary>
        /// get value type for CLR type
        /// </summary>
        public static ValueType GetValueType(Type type)
        {
            if (!type.IsValueType)
                return ValueType.UInt32;
            if (type == typeof(SByte))
                return ValueType.Int8;
            else if (type == typeof(Int16))
                return ValueType.Int16;
            else if (type == typeof(Int32))
                return ValueType.Int32;
            else if (type == typeof(Int64))
                return ValueType.Int64;
            else if (type == typeof(Byte))
                return ValueType.UInt8;
            else if (type == typeof(UInt16))
                return ValueType.UInt16;
            else if (type == typeof(UInt32))
                return ValueType.UInt32;
            else if (type == typeof(UInt64))
                return ValueType.UInt64;
            else if (type == typeof(Single))
                return ValueType.Real32;
            else if (type == typeof(Double))
                return ValueType.Real64;
            else
                return ValueType.UInt32;
        }

        /// <summary>
        /// convert string to value
        /// </summary>
        public static T ConvertTo<T>(string src) where T : struct
        {
            Type type = typeof(T);
            if (type == typeof(SByte))
                return (T)(object)SByte.Parse(src);
            else if (type == typeof(Int16))
                return (T)(object)Int16.Parse(src);
            else if (type == typeof(Int32))
                return (T)(object)Int32.Parse(src);
            else if (type == typeof(Int64))
                return (T)(object)Int64.Parse(src);
            else if (type == typeof(Byte))
                return (T)(object)Byte.Parse(src);
            else if (type == typeof(UInt16))
                return (T)(object)UInt16.Parse(src);
            else if (type == typeof(UInt32))
                return (T)(object)UInt32.Parse(src);
            else if (type == typeof(UInt64))
                return (T)(object)UInt64.Parse(src);
            else if (type == typeof(Single))
                return (T)(object)Single.Parse(src);
            else if (type == typeof(Double))
                return (T)(object)Double.Parse(src);
            else
                return default(T);
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