using System.IO;
using System.Windows.Forms;
using TCM.Helper;

namespace TCM.Model.Designer
{
    public class AppData
    {
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

        private AppConfig _Config = null;
        public AppConfig Config
        {
            get 
            {
                if (_Config == null)
                    _Config = new AppConfig();
                return _Config;
            }
        }

        private LibraryManager _LibManager = null;
        public LibraryManager LibManager
        {
            get 
            {
                if (_LibManager == null)
                    _LibManager = new LibraryManager();
                return _LibManager;
            }
        }

        private FrmMain _FormMain = null;
        public FrmMain FormMain
        {
            get
            {
                if (_FormMain == null)
                    _FormMain = new FrmMain();
                return _FormMain;
            }
        }

        private FrmToolbox _FormToolbox = null;
        public FrmToolbox FormToolbox
        {
            get
            {
                if (_FormToolbox == null)
                    _FormToolbox = new FrmToolbox();
                return _FormToolbox;
            }
        }

        private FrmProperty _FormProperty = null;
        public FrmProperty FormProperty
        {
            get
            {
                if (_FormProperty == null)
                    _FormProperty = new FrmProperty();
                return _FormProperty;
            }
        }
    }
}
