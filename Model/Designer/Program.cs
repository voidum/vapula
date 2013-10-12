using System;
using System.Windows.Forms;

namespace TCM.Model.Designer
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

            AppData.Instance.FormMain.Show();
            
            Application.Run();
        }
    }
}
