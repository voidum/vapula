using System;
using System.Windows.Forms;

namespace Vapula.Toolkit
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

            FrmMain form = new FrmMain();
            form.Show();

            Application.Run();


        }
    }
}
