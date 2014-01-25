using System;
using System.Windows.Forms;
using Vapula.Helper;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.MDE
{
    public partial class FrmLog : Window, ILogger
    {
        public FrmLog()
        {
            Id = "logger";
            DefaultDock = DockState.DockBottomAutoHide;
            InitializeComponent();
        }

        private void UI_AddItem(ListViewItem lvi)
        {
            if (InvokeRequired)
                Invoke(new Action<ListViewItem>(UI_AddItem), lvi);
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
            UI_AddItem(lvi);
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

        public override object Sync(string cmd, object attach)
        {
            if (cmd == "write-log")
                WriteLog(LogType.Debug, attach as string);
            return null;
        }
    }
}
