using System;
using System.IO;

namespace Vapula
{
    /// <summary>
    /// 运行期状态
    /// </summary>
    public enum State
    {
        Idle = 0,
        BusyBack = 1,
        BusyFront = 2,
        Pause = 3
    }

    //控制码
    public enum CtrlCode
    {
        Null = 0,
        Pause = 1,
        Resume = 2,
        Cancel = 3,
        Restart = 4
    }

    /// <summary>
    /// 返回值
    /// </summary>
    public enum ReturnCode
    {
        Error = 0,
        Normal = 1,
        Cancel = 2,
        Terminate = 3,
        NullTask = 4
    }

    /// <summary>
    /// Vapula支持的数据类型
    /// </summary>
    public enum DataType
    {
        Object = 0,
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

    /// <summary>
    /// Vapula支持的参数模式
    /// </summary>
    public enum ParamMode
    {
        In = 0,
        Out = 1,
        InOut = 2
    }

    public class Base
    {
        /// <summary>
        /// 当前运行时标识
        /// </summary>
        public const string RuntimeId = "clr";

        /// <summary>
        /// 获取类型所在程序集的加载目录
        /// </summary>
        public static string GetTypeDir(Type type)
        {
            return Path.GetDirectoryName(type.Assembly.Location);
        }

        /// <summary>
        /// 当前运行时的目录
        /// </summary>
        public static string RuntimeDir
        {
            get { return GetTypeDir(typeof(Base)); }
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
