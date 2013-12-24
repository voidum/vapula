using System.IO;
using System.Windows.Forms;
using Vapula.Helper;

namespace Vapula.Designer
{
    public class AppData
    {
        #region 单例
        private static AppData _Instance = null;
        public static AppData Instance
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new AppData();
                return _Instance; 
            }
        }
        #endregion

        #region 字段
        private AppConfig _Config = null;
        private LibraryManager _LibManager = null;
        private FrmMain _FormMain = null;
        private FrmToolbox _FormToolbox = null;
        private FrmDebug _FormDebug = null;
        private FrmLog _FormLog = null;
        #endregion

        #region 属性
        public AppConfig Config
        {
            get
            {
                if (_Config == null)
                    _Config = new AppConfig();
                return _Config;
            }
        }

        public LibraryManager LibManager
        {
            get
            {
                if (_LibManager == null)
                    _LibManager = new LibraryManager();
                return _LibManager;
            }
        }

        public string PathLibrary
        {
            get
            {
                string path = Path.Combine(
                    Application.StartupPath,
                    Config["PathLibrary"]);
                return path;
            }
        }

        public string PathResource
        {
            get
            {
                string path = Path.Combine(
                    Application.StartupPath,
                    Config["PathResource"]);
                return path;
            }
        }

        public FrmMain FormMain
        {
            get
            {
                if (_FormMain == null)
                    _FormMain = new FrmMain();
                return _FormMain;
            }
        }

        public FrmToolbox FormToolbox
        {
            get
            {
                if (_FormToolbox == null)
                    _FormToolbox = new FrmToolbox();
                return _FormToolbox;
            }
        }

        public FrmDebug FormDebug
        {
            get
            {
                if (_FormDebug == null)
                    _FormDebug = new FrmDebug();
                return _FormDebug;
            }
        }

        public FrmLog FormLog
        {
            get
            {
                if (_FormLog == null)
                    _FormLog = new FrmLog();
                return _FormLog;
            }
        }
        #endregion
    }
}
