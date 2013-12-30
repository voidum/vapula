using System.IO;
using System.Windows.Forms;
using Vapula.Helper;

namespace Vapula.Designer
{
    public class AppData
    {
        #region 实例
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
        private LibraryHub _LibraryHub = null;
        private WindowHub _WindowHub = null;
        private MainWindow _MainWindow = null;
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

        public LibraryHub LibraryHub
        {
            get
            {
                if (_LibraryHub == null)
                    _LibraryHub = new LibraryHub();
                return _LibraryHub;
            }
        }

        public WindowHub WindowHub
        {
            get
            {
                if (_WindowHub == null)
                    _WindowHub = new WindowHub();
                return _WindowHub;
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

        public MainWindow MainWindow
        {
            get
            {
                if (_MainWindow == null)
                    _MainWindow = new MainWindow();
                return _MainWindow;
            }
        }
        #endregion
    }
}