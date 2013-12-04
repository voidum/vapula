using System;
using System.Windows.Forms;

namespace TCM.xHost.CLR
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

            FrmHost form = new FrmHost();
            form.Show();
            
            Application.Run();
        }
    }
}
