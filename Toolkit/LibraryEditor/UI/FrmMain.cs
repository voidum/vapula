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

        private void UI_UpdateAppMode()
        {
            if (App.Library == null)
            {
                MnuFile_New.Enabled = true;
                MnuFile_Open.Enabled = true;
                MnuFile_Save.Enabled = false;
                MnuFile_SaveAs.Enabled = false;
                MnuFile_Close.Enabled = false;
            }
            else 
            {
                MnuFile_New.Enabled = false;
                MnuFile_Open.Enabled = false;
                MnuFile_Save.Enabled = true;
                MnuFile_SaveAs.Enabled = true;
                MnuFile_Close.Enabled = true;
            }
            _UctCore.UI_ValidNonNull();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            UI_UpdateAppMode();
        }

        private void MnuFile_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MnuFile_New_Click(object sender, EventArgs e)
        {
            Library lib = new Library();
            lib.Id = null;
            Function func = new Function();
            func.Id = null;
            lib.Functions.Add(func);

            App.Library = lib;
            App.PathDpt = null;
            App.PathBin = null;
            App.PathTarget = null;

            UI_UpdateAppMode();
            _UctCore.UI_UpdateLibrary();
        }

        private void MnuFile_Open_Click(object sender, EventArgs e)
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

            App.Library = Library.Load(files[0]);
            if (App.Library != null)
            {
                App.PathDpt = files[0];
                App.PathTarget = dlg.SelectedPath;
                MnuFile_Save.Enabled = true;
            }
            else
            {
                MessageBox.Show("指定的库描述文件未通过验证。");
            }

            UI_UpdateAppMode();
            _UctCore.UI_UpdateLibrary();
        }

        private void MnuFile_Save_Click(object sender, EventArgs e)
        {
            if (App.PathTarget != null)
                _UctCore.Publish();
            else
                MnuFile_SaveAs_Click(sender, e);
        }

        private void MnuFile_SaveAs_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "选择组件的发布目录";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            App.PathTarget = dlg.SelectedPath;
            App.PathDpt = Path.Combine(
                App.PathTarget,
                App.Library.Id + ".library");
            _UctCore.Publish();
        }

        private void MnuFile_Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "未保存的更改将会丢弃。\n您是否确认关闭组件？",
                "询问",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question)
                == DialogResult.Cancel)
                return;
            App.Library = null;
            App.PathDpt = null;
            App.PathBin = null;
            App.PathTarget = null;
            _UctCore.UI_UpdateLibrary();
            UI_UpdateAppMode();
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

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (App.Library == null)
                return;
            if (MessageBox.Show(
                "未保存的更改将会丢弃。\n您是否确认关闭组件？",
                "询问",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question)
                == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
