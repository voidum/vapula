using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TCM.Net;

namespace TCM
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Server server = new Server();
            server.Config("server.config");
            server.Start();
            //FrmMain form = new FrmMain();
            //form.Show();
            
            Application.Run();
        }
    }
}
