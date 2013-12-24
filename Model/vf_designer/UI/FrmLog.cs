using System;
using System.Windows.Forms;
using Vapula.Helper;
using xDockPanel;

namespace Vapula.Designer
{
    public partial class FrmLog : DockContent, ILogger
    {
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
    }
}
