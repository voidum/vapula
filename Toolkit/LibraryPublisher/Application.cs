using TCM.Helper;
using TCM.Model;

namespace TCM.Toolkit
{
    public class AppData
    {
        private static AppData _Instance = null;
        /// <summary>
        /// 获取应用程序数据的实例
        /// </summary>
        public static AppData Instance
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new AppData();
                return _Instance;
            }
        }

        private AppConfig _Config = null;
        /// <summary>
        /// 获取应用程序配置
        /// </summary>
        public AppConfig Config
        {
            get 
            {
                if (_Config == null)
                    _Config = new AppConfig(); ;
                return _Config;
            }
        }

        private string _PathConfig = null;
        /// <summary>
        /// 获取当前组件的配置路径
        /// </summary>
        public string PathConfig
        {
            get
            {
                if (_PathConfig == null)
                    return "";
                return _PathConfig;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _PathConfig = null;
                else _PathConfig = value;
            }
        }

        private Library _Library = null;
        /// <summary>
        /// 获取当前库
        /// </summary>
        public Library Library
        {
            get { return _Library; }
            set
            {
                if (value == null && _Library != null)
                    _Library.Clear();
                _Library = value;
            }
        }
    }
}
