using System;
using System.IO;
using System.Windows.Forms;
using TCM.Toolkit;

namespace TCM.ComPublisher
{
    public partial class FrmSetPublish : Form
    {
        public FrmSetPublish()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private bool FormLayout_DlgFolder(string description)
        {
            DlgFolder.Description = description;
            return (DlgFolder.ShowDialog() == DialogResult.OK);
        }

        private void BtBrowse_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            string flag = bt.Tag as string;
            switch (flag)
            {
                case "bin":
                    if (FormLayout_DlgFolder("请选择二进制文件输出目录。"))
                        TbxBin.Text = IOHelper.GetFullPath(DlgFolder.SelectedPath, false);
                    break;
                case "cfg":
                    if (FormLayout_DlgFolder("请选择组件配置文件输出目录。"))
                        TbxCfg.Text = IOHelper.GetFullPath(DlgFolder.SelectedPath, false);
                    break;
                case "lst":
                    if (FormLayout_DlgFolder("请选择组件清单文件输出目录。"))
                        TbxLst.Text = IOHelper.GetFullPath(DlgFolder.SelectedPath, false);
                    break;
                case "res":
                    if (FormLayout_DlgFolder("请选择资源文件输出目录。"))
                        TbxRes.Text = IOHelper.GetFullPath(DlgFolder.SelectedPath, false);
                    break;
            }
        }

        private void BtApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbxBin.Text) ||
                string.IsNullOrWhiteSpace(TbxCfg.Text) ||
                string.IsNullOrWhiteSpace(TbxLst.Text) ||
                string.IsNullOrWhiteSpace(TbxRes.Text))
            {
                MessageBox.Show("请为所有导出项选择路径。", "提示");
                return;
            }
            AppData.Config["DirBin"] = TbxBin.Text;
            AppData.Config["DirCfg"] = TbxCfg.Text;
            AppData.Config["DirRes"] = TbxRes.Text;
            AppData.Config["PathLst"] = TbxLst.Text + "tcmcoms.xml";
            AppData.Config["IfOverwrite"] = ChbxAutoOverwrite.Checked ? "true" : "false";
            AppData.Config.Save();
            DialogResult = DialogResult.OK;
        }
    }
}
