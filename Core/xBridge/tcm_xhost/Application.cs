using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TCM
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

        #region 数据
        private string _PathLib;
        public string PathLib 
        {
            get { return _PathLib; }
            set { _PathLib = value; }
        }

        public string PathUI
        {
            get { return Path.Combine(PathLib, "UI"); }
        }

        public string PathUIIndex
        {
            get { return Path.Combine(PathLib, "UI", "index.html"); }
        }

        private string _DataId;
        public string DataId
        {
            get { return _DataId; }
            set { _DataId = value; }
        }
        #endregion
    }
}
