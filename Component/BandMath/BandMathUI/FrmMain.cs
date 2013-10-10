using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using TCM.Runtime;

namespace BandMathUI
{
    public partial class FrmMain : Form
    {
        private XElement _XmlDataVar;
        private XElement _XmlOutput;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtDataVar_Click(object sender, EventArgs e)
        {
            PIE.Controls.DlgDataDef dlgdata = new PIE.Controls.DlgDataDef();
            if (dlgdata.ShowDialog() == DialogResult.OK)
            {
                _XmlDataVar = dlgdata.XmlDataVar;
                _XmlOutput = dlgdata.XmlOutput;
            }
        }

        private void BtExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbxExpr.Text))
            {
                MessageBox.Show("请输入表达式。");
                return;
            }
            string path = Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".datadef.xml";
            try
            {
                SaveDataDef(path);
                ExecutorFunction exec = new ExecutorFunction(AppData.TcmModHandle);
                if (!exec.Mount(AppData.TcmFuncDesc))
                {
                    MessageBox.Show("装载算法失败。");
                    return;
                }
                exec.Envelope.Write(0, TbxExpr.Text);
                exec.Envelope.Write(1, path);
                Thread thread = new Thread(new ParameterizedThreadStart(ThreadProc_ExecBandMath));
                thread.Start(exec);
                AppData.FormProgress = new PIE.Controls.DlgProgress();
                AppData.FormProgress.OnCancel = FormProg_Cancel;
                AppData.FormProgress.ShowDialog();
                File.Delete(path + ".datadef.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("准备算法时发生错误。");
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveDataDef(string path)
        {
            XDocument xdoc = new XDocument(
                new XDeclaration("1.0","utf-8","yes"),
                _XmlDataVar,
                _XmlOutput);
            xdoc.Save(path);
        }

        private bool FormProg_Cancel()
        {
            return false;
        }

        private void ThreadProc_ExecBandMath(object param)
        {
            while (AppData.FormProgress == null || !AppData.FormProgress.IsShown) Thread.Sleep(50);
            ExecutorFunction exec = param as ExecutorFunction;
            if (!exec.Start())
            {
                MessageBox.Show("启动算法失败。");
                AppData.FormProgress.Invoke_CloseForm();
                return;
            }
            AppData.FormProgress.Invoke_UpdateText("正在执行波段运算...");
            while (exec.State != State.Idle)
            {
                AppData.FormProgress.Invoke_UpdateProgress(exec.Progress);
                Thread.Sleep(100);
            }
            AppData.FormProgress.Invoke_CloseForm();
            if (exec.ReturnCode == ReturnCode.Normal)
                MessageBox.Show("算法执行完成。");
            else
                MessageBox.Show("算法执行失败。");
        }
    }
}
