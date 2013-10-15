using System;
using System.Windows.Forms;

namespace TCM.ComManager
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
            //Init
            if (!AppData.Config.Load(Application.StartupPath + "\\tcm_com_manager.config"))
            {
                MessageBox.Show("没有找到配置文件，请重新部署[组件管理器]。");
            }
            else
            {
                Application.Run(new FrmMain());
                AppData.Config.Save();
            }
        }
    }
}
