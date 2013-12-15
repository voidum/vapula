using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Vapula.Designer
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
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            AppData app = AppData.Instance;
            if (!app.Config.Load(Path.Combine(
                Application.StartupPath, "designer.config")))
            {
                MessageBox.Show("加载Designer配置失败。");
                return;
            }
            if (!app.LibManager.Load(Path.Combine(
                Application.StartupPath, app.Config["LibraryList"])))
            {
                MessageBox.Show("加载组件库清单失败。");
                return;
            }

            app.FormMain.Show();
            
            Application.Run();
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string ex = "Message: " + e.Exception.Message;
            ex += Environment.NewLine + "Source: " + e.Exception.Source;
            ex += Environment.NewLine + "StackTrace: " + e.Exception.StackTrace;
            MessageBox.Show(ex, "Designer Exception");
            Application.Exit();
        }
    }
}
