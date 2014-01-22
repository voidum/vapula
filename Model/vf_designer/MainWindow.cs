using System;
using System.Threading;
using System.Windows.Forms;
using Vapula.Model;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.Designer
{
    public partial class MainWindow : Form
    {
        private AppData App
        {
            get { return AppData.Instance; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void UI_ShowWindow(DockContent content, DockState state)
        {
            content.Show(paneldock, state);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //App.WindowHub.Show("startup");
            App.WindowHub.Show("toolbox");
            App.WindowHub.Show("logger");
            App.WindowHub.Show("debug");
            App.WindowHub.Show("explorer");
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                if(MessageBox.Show(
                    "是否确定退出模型设计器？", "询问",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    App.WindowHub.CloseAll();
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void MnuFile_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MnuModel_ExecuteModel_Click(object sender, EventArgs e)
        {
            var thread = new Thread(
                new ThreadStart(
                    () => { App.CurrentGraph.Start(); }));
            thread.Start();
        }

        private void MnuModel_SimulateStage_Click(object sender, EventArgs e)
        {
            var dlg = App.WindowHub.Show("stage") as FrmStage;
            var stage = App.CurrentGraph.FirstStage;
            while (stage != null)
            {
                dlg.UI_AddItem(stage);
                stage = stage.NextStage;
            }
        }

        private void MnuView_Workspace_Click(object sender, EventArgs e)
        {
            App.WindowHub.Show("explorer");
        }

        private void MnuView_Toolbox_Click(object sender, EventArgs e)
        {
            App.WindowHub.Show("toolbox");
        }

        private void MnuView_Stage_Click(object sender, EventArgs e)
        {
            App.WindowHub.Show("stage");
        }

        private void MnuView_Log_Click(object sender, EventArgs e)
        {
            App.WindowHub.Show("logger");
        }

        private void MnuFile_New_Click(object sender, EventArgs e)
        {
            if (App.CurrentLibrary != null)
            {
                if (MessageBox.Show(
                    "您已经打开一个模型库。\n继续新建操作会放弃当前模型库的更改。\n您确定继续吗？",
                    "询问", MessageBoxButtons.OKCancel)
                    == DialogResult.Cancel)
                    return;
            }
            var lib = new Library();
            lib.Id = "Library";
            var func = new Function();
            func.Id = "Graph1";
            lib.Functions.Add(func);
            App.CurrentLibrary = lib;
            App.WindowHub["explorer"].Sync("update", null);
        }
    }
}
