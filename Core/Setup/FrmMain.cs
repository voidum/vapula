using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TCM.Setup
{
    public partial class FrmMain : Form
    {
        private List<Command> _Commands 
            = new List<Command>();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void Log(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(Log), text);
            }
            else
            {
                TbxLog.AppendText(
                    string.Format("({0}){1}",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        Environment.NewLine + text +
                        Environment.NewLine +
                        Environment.NewLine));
            }
        }

        private void MnuAction_Execute_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(
                new ThreadStart(() =>
                {
                    foreach (Command cmd in _Commands)
                        cmd.Execute();
                }));
            thread.Start();
        }

        private void MnuAction_Load_Click(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Load(
                Path.Combine(Application.StartupPath, "setup.list"));
            IEnumerable<XElement> xecmds = 
                xdoc.Element("root").Elements("cmd");
            int i = 0;
            foreach (XElement xe in xecmds)
            {
                Command cmd = Command.Parse(xe);
                _Commands.Add(cmd);
                i++;
            }
            Command.Log = Log;
            Log(string.Format("加载部署脚本，共{0}条指令", i));
        }
    }
}
