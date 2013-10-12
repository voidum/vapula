using System;
using TCM.Model.Designer;

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
