using System.IO;
using System.Windows.Forms;
using Vapula.Flow;
using Vapula.Helper;
using Vapula.Model;

namespace Vapula.MDE
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
        private Library _CurrentLibrary = null;
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

        public Library CurrentLibrary
        {
            get { return _CurrentLibrary; }
            set { _CurrentLibrary = value; }
        }

        public Graph CurrentGraph
        {
            get 
            {
                var doc = WindowHub.FindOne((e) => 
                    {
                        if (e.Id.StartsWith("document")) //TODO:无法获取哪个MDI子窗体是当前激活的
                            return true;
                        return false;
                    });
                if (doc != null)
                    return (doc as FrmDocument).Model;
                return null; 
            }
        }
        #endregion
    }
}