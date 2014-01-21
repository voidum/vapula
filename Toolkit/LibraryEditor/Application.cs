using System.IO;
using Vapula.Helper;
using Vapula.Model;

namespace Vapula.Toolkit
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

        private string _PathDpt = null;
        private string _PathBin = null;
        private string _PathTarget = null;

        /// <summary>
        /// 获取或设置当前组件的库描述文件路径
        /// </summary>
        public string PathDpt
        {
            get { return _PathDpt; }
            set
            {
                _PathDpt = 
                    string.IsNullOrWhiteSpace(value) 
                    ? null : value;
            }
        }

        /// <summary>
        /// 获取或设置当前组件的库主体文件路径
        /// </summary>
        public string PathBin
        {
            get { return _PathBin; }
            set
            {
                _PathBin = 
                    string.IsNullOrWhiteSpace(value)
                    ? null : value;
            }
        }

        /// <summary>
        /// 获取或设置组件的目标发布目录
        /// </summary>
        public string PathTarget
        {
            get { return _PathTarget; }
            set 
            {
                _PathTarget 
                    = string.IsNullOrWhiteSpace(value)
                    ? null : value;
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
