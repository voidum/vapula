using System;
using System.Windows.Forms;
using TCM.Helper;
using TCM.Runtime;
using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmMain : Form
    {
        private FrmDocument doc;

        private AppData App
        {
            get { return AppData.Instance; }
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            App.FormToolbox.Show(paneldock, DockState.Left);
            App.FormDebug.Show(paneldock, DockState.AutoHideRight);
            App.FormProperty.Show(paneldock, DockState.Right);
            App.FormLog.Show(paneldock, DockState.AutoHideBottom);
            doc = new FrmDocument();
            doc.Show(paneldock, DockState.Document);
        }

        private void MnuModel_Start_Click(object sender, EventArgs e)
        {
            App.FormLog.WriteLog(LogType.Event, "开始执行模型");
            ModelProxy proxy = new ModelProxy(doc.Graph);
            proxy.Logger = App.FormLog;
            proxy.Start();
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
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
