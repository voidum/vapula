using System;
using System.Windows.Forms;
using Vapula.Helper;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.Designer
{
    public partial class FrmLog : DockContent, ILogger, IWindow
    {
        private WindowHub.State _State;

        private AppData App
        {
            get { return AppData.Instance; }
        }

        public FrmLog()
        {
            InitializeComponent();
        }

        private void FormLayout_AddItem(ListViewItem lvi)
        {
            if (InvokeRequired)
                Invoke(new Action<ListViewItem>(FormLayout_AddItem), lvi);
            else
                LsvLog.Items.Add(lvi);
        }

        public void WriteLog(LogType type, params object[] values)
        {
            ListViewItem lvi = new ListViewItem(
                new string[]
                {
                    type.ToString(),
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    (string)values[0]
                });
            FormLayout_AddItem(lvi);
        }

        public void ClearLog()
        {
            LsvLog.Items.Clear();
        }

        private void BtClear_Click(object sender, EventArgs e)
        {
            ClearLog();
        }

        private void FrmLog_Resize(object sender, EventArgs e)
        {
            ColhContent.Width 
                = LsvLog.Width 
                - ColhLevel.Width 
                - ColhTime.Width - 20;
        }

        public string Id
        {
            get { return "logger"; }
        }

        public WindowHub.State State
        {
            get { return _State; }
            set
            {
                if (_State == value)
                    return;
                if (value == WindowHub.State.Visible)
                    App.MainWindow.UI_ShowWindow(this, DockState.DockBottomAutoHide);
                else Hide();
                _State = value;
            }
        }

        private void FrmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            State = WindowHub.State.Hidden;
            e.Cancel = true;
        }
    }
}
