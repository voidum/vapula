using System;
using System.IO;

namespace Vapula
{
    /// <summary>
    /// 执行状态
    /// </summary>
    public enum State
    {
        Idle = 0,
        Pause = 1,
        BusyBack = 2,
        BusyFront = 3,
        Rollback = 4
    }

    /// <summary>
    /// 执行控制码
    /// </summary>
    public enum ControlCode
    {
        Null = 0,
        Pause = 1,
        Resume = 2,
        Cancel = 3,
        Restart = 4
    }

    /// <summary>
    /// 执行返回值
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
    /// 支持数据类型
    /// </summary>
    public enum DataType
    {
        Raw = 0,
        Value = 1,
        Text = 2
    };

    /// <summary>
    /// 支持值类型
    /// </summary>
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

    /// <summary>
    /// 数据访问模式
    /// </summary>
    public enum AccessMode
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
        /// 获取Vapula值类型对应的CLR类型
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
        /// 获取Vapula值类型对应的CLR可空类型
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
    }
}
