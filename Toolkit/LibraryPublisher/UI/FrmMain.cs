using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Vapula.Model;

namespace Vapula.Toolkit
{
    public partial class FrmMain : Form
    {
        private UctUI _UctUI = new UctUI();
        private UctCore _UctCore = new UctCore();
        private UctLicence _UctLic = new UctLicence();

        public FrmMain()
        {
            InitializeComponent();
            _UctCore.Dock = DockStyle.Fill;
            _UctUI.Dock = DockStyle.Fill;
            _UctLic.Dock = DockStyle.Fill;
            TabCore.Controls.Add(_UctCore);
            TabUI.Controls.Add(_UctUI);
            TabLic.Controls.Add(_UctLic);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            MnuLib_Publish.Enabled = false;
        }

        private void MnuLib_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MnuLib_New_Click(object sender, EventArgs e)
        {
            Library lib = new Library();
            lib.Id = null;
            Function func = new Function();
            func.Id = 1;
            lib.Functions.Add(func);

            AppData app = AppData.Instance;
            app.Library = lib;
            app.PathDpt = null;
            app.PathBin = null;
            app.PathTarget = null;
            MnuLib_Publish.Enabled = true;

            _UctCore.UI_UpdateLibrary();
        }

        private void MnuLib_Open_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "选择组件的发布目录";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            string[] files = Directory.GetFiles(dlg.SelectedPath, "*.library");
            if (files.Length != 1)
            {
                MessageBox.Show(
                    "选择的目录不是有效的发布目录。\n" + 
                    "目录中不存在唯一库描述。", "提示");
                return;
            }

            AppData app = AppData.Instance;
            app.Library = Library.Load(files[0]);
            if (app.Library != null)
            {
                app.PathDpt = files[0];
                app.PathTarget = dlg.SelectedPath;
                MnuLib_Publish.Enabled = true;
            }
            else
            {
                MessageBox.Show("指定的库描述文件未通过验证。");
            }
            _UctCore.UI_UpdateLibrary();
        }

        private void MnuLib_Publish_Click(object sender, EventArgs e)
        {
            AppData app = AppData.Instance;
            if (app.PathTarget == null)
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.Description = "选择组件的发布目录";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                app.PathTarget = dlg.SelectedPath;
                app.PathDpt = Path.Combine(
                    app.PathTarget, 
                    app.Library.Id + ".library");
            }
            _UctCore.Publish();
        }

        private void MnuHelp_Guide_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(
                    Path.Combine(
                    Application.StartupPath,
                    "manual.pdf"));
            }
            catch
            {
                MessageBox.Show("打开用户手册失败。");
            }
        }

        private void MnuHelp_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "当前版本：" + Application.ProductVersion,
                "关于组件发布器");
        }
    }
}
