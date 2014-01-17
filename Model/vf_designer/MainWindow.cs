using System;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.Designer
{
    public partial class MainWindow : Form
    {
        private FrmDocument doc;

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
            App.WindowHub.Show("toolbox");
            App.WindowHub.Show("logger");
            App.WindowHub.Show("debug");
            App.WindowHub.Show("workspace");
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
                    () => { doc.Model.Start(); }));
            thread.Start();
        }

        private void MnuModel_SimulateStage_Click(object sender, EventArgs e)
        {
            var dlg = App.WindowHub.Show("stage") as FrmStage;
            var stage = doc.Model.FirstStage;
            while (stage != null)
            {
                dlg.UI_AddItem(stage);
                stage = stage.NextStage;
            }
        }

        private void MnuView_Workspace_Click(object sender, EventArgs e)
        {
            App.WindowHub.Show("workspace");
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
            doc = new FrmDocument();
            doc.Show(paneldock, DockState.Document);
        }
    }
}
