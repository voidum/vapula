using System;
using System.Windows.Forms;

namespace TCM.ComPublisher
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
            if (!AppData.Config.Load(Application.StartupPath + "\\tcm_com_publisher.config"))
            {
                MessageBox.Show("加载配置文件失败，需要重建配置。","注意");
                FrmSetPublish DlgSetPublish = new FrmSetPublish();
                if (DlgSetPublish.ShowDialog() != DialogResult.OK) return;
            }
            Application.Run(new FrmMain());
        }
    }
}
