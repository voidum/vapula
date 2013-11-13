using System;
using System.IO;
using System.Windows.Forms;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] cmds = Environment.GetCommandLineArgs();
            if (cmds.Length == 3)
            {
                if (!CheckParam(cmds))
                {
                    ShowHelp();
                    //return 0;
                }
                FrmBrowser form = new FrmBrowser();
                form.ShowDialog();
            }
            else
        	{
                ShowHelp();
        	}
            return 0;
        }

        static bool CheckParam(string[] cmds)
        {
            AppData app = AppData.Instance;
            app.PathLib = cmds[1];
            app.DataId = cmds[2];
            if (!Directory.Exists(cmds[1]))
            {
                MessageBox.Show("没有找到目录：\n" + cmds[1], "TCM xHost");
                return false;
            }
            string tempfile = Path.Combine(
                Path.GetTempPath(), 
                cmds[2] + ".tcm.de");
            if (!File.Exists(tempfile))
            {
                MessageBox.Show("没有找到数据交换页：" + cmds[2], "TCM xHost");
                return false;
            }
            return true;
        }

        static void ShowHelp()
        {
            string str = "command lines:\n";
            str += " tcm_xhost [library directory] [data id]\n";
            MessageBox.Show(str, "TCM xHost");
        }
    }
}
