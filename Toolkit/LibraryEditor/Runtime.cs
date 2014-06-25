using Sartrey;
using System.IO;
using Vapula.Helper;
using Vapula.Model;

namespace Vapula.Toolkit
{
    public class Runtime
    {
        private static Runtime _Instance 
            = null;
        private static object _CtorLock 
            = new object();

        public static Runtime Instance
        {
            get 
            {
                if (_Instance == null)
                    lock (_CtorLock) {
                        if (_Instance == null)
                            _Instance = new Runtime();
                    }
                return _Instance;
            }
        }

        private Setting _Setting = null;

        public Setting Setting
        {
            get 
            {
                if (_Setting == null)
                    _Setting = new Setting(); ;
                return _Setting;
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
