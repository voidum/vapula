using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DecisionTreeUI
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

            TCM.Global.Logger = new TCM.Toolkit.FileLogger();

            string [] cmd = Environment.GetCommandLineArgs();
            string langpath = Application.StartupPath + "\\";
            if (cmd.Length == 2) langpath += cmd[1] + ".xml";
            else langpath += "zh-CN.xml";
            if (!AppData.LangPack.Load(langpath))
            {
                MessageBox.Show("Language pack is lost.\nPlease repair the application.");
                return;
            }
            AppData.FormMain.Show();
            Application.Run();
        }
    }
}
