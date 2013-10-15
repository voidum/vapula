using TCM.Model;

namespace TCM.Toolkit.TaskHelper
{
    public class AppData
    {
        public static string GetDataTypeName(DataType type)
        {
            switch (type)
            {
                case DataType.Pointer: return "指针";
                case DataType.Int8: return "8位整数";
                case DataType.Int16: return "16位整数";
                case DataType.Int32: return "32位整数";
                case DataType.Int64: return "64位整数";
                case DataType.UInt8: return "8位无符号整数";
                case DataType.UInt16: return "16位无符号整数";
                case DataType.UInt32: return "32位无符号整数";
                case DataType.UInt64: return "64位无符号整数";
                case DataType.Real32: return "32位浮点数";
                case DataType.Real64: return "64位浮点数";
                case DataType.Bool: return "布尔值";
                case DataType.StringA: return "多字节字符串";
                case DataType.StringW: return "宽字节字符串";
                default: return "";
            }
        }

        private static AppConfig _Config;
        public static AppConfig Config
        {
            get
            {
                if (_Config == null) _Config = new AppConfig(); ;
                return _Config;
            }
        }

        private static Component _Component = null;
        public static Component Component
        {
            get { return _Component; }
            set
            {
                if (value == null && _Component != null) _Component.Clear();
                _Component = value;
            }
        }

        public static string DirLib = "";
        public static int FuncId = 0;
    }
}
