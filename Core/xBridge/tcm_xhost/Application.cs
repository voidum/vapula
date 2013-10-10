using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCM
{
    public class AppData
    {
        #region 数据
        private static Dictionary<string,bool> _Flags = new Dictionary<string,bool>();
        /// <summary>
        /// 应用程序标志
        /// </summary>
        public static Dictionary<string,bool> Flags
        {
            get { return _Flags; }
        }
        #endregion
    }
}
