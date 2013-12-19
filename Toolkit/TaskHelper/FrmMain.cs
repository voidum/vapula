using System;
using System.Windows.Forms;
using System.Xml.Linq;
using Vapula.Model;

namespace Vapula.Toolkit.TaskHelper
{
    public partial class FrmMain : Form
    {
        public void FormLayout_NewTask()
        {
            CobxFuncId.Enabled = false;
            LblCom.Text = "";
            LsvParam.Items.Clear();
            CobxXproc.SelectedIndex = 0;
            BtConfigXproc.Enabled = false;
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FormLayout_NewTask();
        }

        private void BtNew_Click(object sender, EventArgs e)
        {
            FormLayout_NewTask();
        }

        private void BtBrowse_Click(object sender, EventArgs e)
        {
            if(dlgopen.ShowDialog() != DialogResult.OK) return;
            string dllname = dlgopen.FileName;
            string comdesc = IOHelper.GetFullDir(dllname, true) 
                + IOHelper.GetFileNameNoExt(dllname) 
                + ".tcm.xml";
            try
            {
                XDocument xdoc = XDocument.Load(comdesc);
                AppData.Component = Component.Parse(xdoc.Element("root").Element("component"));
            }
            catch
            {
                MessageBox.Show("加载组件描述时发生未知错误。");
            }
            LblCom.Text = dllname;
            CobxFuncId.Enabled = true;
            CobxFuncId.Items.Clear();
            foreach (Function func in AppData.Component.Functions)
                CobxFuncId.Items.Add(func.Id);
        }

        private void CobxFuncId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CobxFuncId.SelectedItem == null) return;
            Function func = AppData.Component[CobxFuncId.SelectedIndex];
            LsvParam.Items.Clear();
            LblFuncName.Text = func.Name;
            foreach(Parameter param in func.Parameters)
            {
                ListViewItem lvi = new ListViewItem(
                    new string[]{
                        param.Id.ToString(),
                        param.Name,
                        AppData.GetDataTypeName(param.Type),
                        ""
                    });
                LsvParam.Items.Add(lvi);
            }
        }
    }
}
