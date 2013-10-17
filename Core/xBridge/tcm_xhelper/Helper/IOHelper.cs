using System;

namespace TCM.Helper
{
    public class IOHelper
    {
        /// <summary>
        /// 获取当前路径
        /// </summary>
        public static string AppDir
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }
    }
}
