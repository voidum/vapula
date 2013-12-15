using System;

namespace Vapula
{
    /// <summary>
    /// Vapula支持的数据类型
    /// </summary>
    public enum DataType
    {
        Pointer = 0,
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
	    Bool = 20,
	    String = 21
    };

    public class Base
    {
        /// <summary>
        /// 当前运行时标识
        /// </summary>
        public const string RuntimeId = "clr";

        /// <summary>
        /// 当前运行时的目录
        /// </summary>
        public static string AppDir
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        /// <summary>
        /// 获取Vapula数据类型对应的CLR类型
        /// </summary>
        public static Type GetCLRType(DataType type)
        {
            switch (type)
            {
                case DataType.Bool: return typeof(bool);
                case DataType.Int8: return typeof(char);
                case DataType.Int16: return typeof(short);
                case DataType.Int32: return typeof(int);
                case DataType.Int64: return typeof(long);
                case DataType.UInt8: return typeof(byte);
                case DataType.UInt16: return typeof(ushort);
                case DataType.UInt32: return typeof(uint);
                case DataType.UInt64: return typeof(ulong);
                case DataType.Real32: return typeof(float);
                case DataType.Real64: return typeof(double);
                case DataType.String:
                    return typeof(string);
                default: return typeof(IntPtr);
            }
        }

        /// <summary>
        /// 获取Vapula数据类型对应的CLR可空类型
        /// </summary>
        public static Type GetNullableCLRType(DataType type)
        {
            switch (type)
            {
                case DataType.Bool: return typeof(Nullable<bool>);
                case DataType.Int8: return typeof(Nullable<char>);
                case DataType.Int16: return typeof(Nullable<short>);
                case DataType.Int32: return typeof(Nullable<int>);
                case DataType.Int64: return typeof(Nullable<long>);
                case DataType.UInt8: return typeof(Nullable<byte>);
                case DataType.UInt16: return typeof(Nullable<ushort>);
                case DataType.UInt32: return typeof(Nullable<uint>);
                case DataType.UInt64: return typeof(Nullable<ulong>);
                case DataType.Real32: return typeof(Nullable<float>);
                case DataType.Real64: return typeof(Nullable<double>);
                case DataType.String:
                    return typeof(string);
                default: return typeof(Nullable<IntPtr>);
            }
        }
    }
}
