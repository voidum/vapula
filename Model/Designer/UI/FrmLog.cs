using System;
using System.Windows.Forms;
using TCM.Helper;
using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmLog : DockContent, ILogger
    {
        public FrmLog()
        {
            InitializeComponent();
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
            LsvLog.Items.Add(lvi);
        }

        public void ClearLog()
        {
            LsvLog.Items.Clear();
        }
    }
}
