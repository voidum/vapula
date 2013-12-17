using System;
using System.Windows.Forms;
using Vapula.Runtime;

namespace sample_xpipe
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FormLayout_WriteLog(string log)
        {
            TbxLog.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            TbxLog.Text += Environment.NewLine;
            TbxLog.Text += log;
            TbxLog.Text += Environment.NewLine;
            TbxLog.Text += Environment.NewLine;
        }

        private void BtSend_Click(object sender, EventArgs e)
        {
            AppData app = AppData.Instance;
            if (app.Pipe == null)
                return;
            app.Pipe.Write(TbxInput.Text);
            FormLayout_WriteLog(
                "发送数据：" +
                Environment.NewLine +  
                TbxInput.Text);
            TbxInput.Text = "";
        }

        private void BtStart_Click(object sender, EventArgs e)
        {
            AppData app = AppData.Instance;
            if (app.Pipe != null)
            {
                app.Pipe.Close();
                app.Pipe.Dispose();
                app.Pipe = null;
            }
            FrmPipe dlg = new FrmPipe();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            app.Pipe = new Pipe();
            if (dlg.AsServer)
            {
                app.Pipe.Listen();
                FormLayout_WriteLog(
                    "以服务端模式监听信道：" +
                    Environment.NewLine +
                    app.Pipe.Id);
            }
            else
            {
                app.Pipe.Connect(dlg.PipeId);
                FormLayout_WriteLog(
                    "以客户端模式连接信道：" +
                    Environment.NewLine +
                    app.Pipe.Id);
            }
        }

        private void BtClose_Click(object sender, EventArgs e)
        {
            AppData app = AppData.Instance;
            if (app.Pipe != null)
            {
                app.Pipe.Close();
                app.Pipe.Dispose();
                app.Pipe = null;
            }
            FormLayout_WriteLog("关闭信道");
        }
    }
}
