using System;

namespace TCM.xHost
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static int Main()
        {
            using (var app = new xHostAppImpl())
            {
                string[] args = Environment.GetCommandLineArgs();
                int ret = app.Run(args);
                return ret;
            }
        }
    }
}
