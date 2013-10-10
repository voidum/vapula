using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using TCM.Model;
using TCM.Toolkit;

namespace TCM.ComPubMini
{
    public partial class FrmMain : Form
    {
        private void FormLayout_UpdateForm()
        {
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            if(AppData.Component != null) 
            {
                string tntext = string.Format("组件：{0}({1})",
                    AppData.Component.Name != null ? AppData.Component.Name : "[未指定名称]",
                    AppData.Component.Id);
                TreeNode tn_com = new TreeNode(tntext);
                foreach (Function model_func in AppData.Component.Functions)
                {
                    tntext = string.Format("功能：{0}({1})",
                        model_func.Name != null ? model_func.Name : "[未指定名称]",
                        model_func.Id);
                    TreeNode tn_func = new TreeNode(tntext);
                    foreach (Parameter model_param in model_func.Parameters)
                    {
                        tntext = string.Format("参数：{0}({1})",
                            model_param.Name != null ? model_param.Name : "[未指定名称]",
                            model_param.Id);
                        TreeNode tn_param = new TreeNode(tntext);
                        tn_func.Nodes.Add(tn_param);
                    }
                    tn_com.Nodes.Add(tn_func);
                }
                treeview.Nodes.Add(tn_com);
            }
            treeview.ExpandAll();
            treeview.EndUpdate();
        }

        private bool FormLayout_DlgOpenFile(string filter,string title,string ext)
        {
            dlgfile.FileName = "";
            dlgfile.Filter = filter;
            dlgfile.Title = title;
            dlgfile.DefaultExt = ext;
            return (dlgfile.ShowDialog() == DialogResult.OK);
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            MnuCom_Publish.Enabled = false;
        }

        private void MnuCom_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MnuCom_New_Click(object sender, EventArgs e)
        {
            if (!FormLayout_DlgOpenFile("组件二进制文件|*.dll", "选择组件二进制文件", "dll")) return;
            string cid = IOHelper.GetFileNameNoExt(dlgfile.SafeFileName);
            string dirbin = IOHelper.GetFullDir(dlgfile.FileName, true);
            if (Regex.IsMatch(cid, "[^a-zA-Z0-9_]"))
            {
                MessageBox.Show("目标文件名称包含不符合要求的字符。\n请依据字符集[^a-zA-Z0-9_]自行重命名", "注意");
                return;
            }
        
            AppData.Component = new Component();
            AppData.Component.Id = cid;
            Function model_func = new Function();
            AppData.Component.Functions.Add(model_func);
            AppData.PathCfg = dirbin + cid + ".tcm.xml";

            MnuCom_Publish.Enabled = true;
            FormLayout_UpdateForm();
        }

        private void MnuCom_Load_Click(object sender, EventArgs e)
        {
            if (!FormLayout_DlgOpenFile("组件配置文件|*.tcm.xml", "选择组件配置文件", ".tcm.xml")) return;
            XDocument xdoc = XDocument.Load(dlgfile.FileName);
            XElement xecom = xdoc.Element("root").Element("component");
            string cid = xecom.Attribute("id").Value;
            AppData.Component = Component.Parse(xecom);
            AppData.PathCfg = dlgfile.FileName;

            MnuCom_Publish.Enabled = true;
            FormLayout_UpdateForm();
        }
        
        private void MnuCom_Publish_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument xdoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("root", AppData.Component.ToXml()));
                xdoc.Save(AppData.PathCfg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发布时发生错误。\n" + ex.Message, "注意");
                return;
            }
            MessageBox.Show("组件发布成功。");
        }

        private void BtAddParam_Click(object sender, EventArgs e)
        {
            if (AppData.Component == null)
            {
                MessageBox.Show("首先需要新建发布任务或打开已有的配置文件。", "提示");
                return;
            }
            TreeNode tn = treeview.SelectedNode;
            if (tn == null) return;
            Parameter model_param = null;
            switch (tn.Level)
            {
                case 1:
                    model_param = new Parameter();
                    AppData.Component.Functions[tn.Index].Parameters.Add(model_param);
                    AppData.Component.Functions[tn.Index].UpdateId();
                    break;
                case 2:
                    model_param = new Parameter();
                    AppData.Component.Functions[tn.Parent.Index].Parameters.Insert(tn.Index, model_param);
                    AppData.Component.Functions[tn.Parent.Index].UpdateId();
                    break;
            }
            FormLayout_UpdateForm();
        }

        private void BtAddFunc_Click(object sender, EventArgs e)
        {
            if (AppData.Component == null)
            {
                MessageBox.Show("首先需要新建发布任务或打开已有的配置文件。", "提示");
                return;
            }
            TreeNode tn = treeview.SelectedNode;
            if (tn == null) return;
            Function model_func = null;
            switch (tn.Level)
            {
                case 0:
                    model_func = new Function();
                    AppData.Component.Functions.Add(model_func);
                    break;
                case 1:
                    model_func = new Function();
                    AppData.Component.Functions.Insert(tn.Index, model_func);
                    break;
            }
            AppData.Component.UpdateId();
            FormLayout_UpdateForm();
        }

        private void BtRemove_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeview.SelectedNode;
            if (tn == null)
            {
                MessageBox.Show("请选择一个项。", "提示");
                return;
            }
            if (MessageBox.Show("确认移除选中项吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                switch (tn.Level)
                {
                    case 0:
                        AppData.Component = null;
                        MnuCom_Publish.Enabled = false;
                        break;
                    case 1:
                        AppData.Component.Functions[tn.Index].Clear();
                        AppData.Component.Functions.RemoveAt(tn.Index);
                        AppData.Component.UpdateId();
                        break;
                    case 2:
                        AppData.Component.Functions[tn.Parent.Index].Parameters.RemoveAt(tn.Index);
                        AppData.Component.Functions[tn.Parent.Index].UpdateId();
                        break;
                }
                FormLayout_UpdateForm();
            }
        }

        private void BtProperty_Click(object sender, EventArgs e)
        {
            if (AppData.Component == null)
            {
                MessageBox.Show("首先需要新建发布任务或打开已有的配置文件。", "提示");
                return;
            }
            TreeNode tn = treeview.SelectedNode;
            if (tn == null) return;
            switch (tn.Level)
            {
                case 0:
                    FrmDetailCom dlgcom = new FrmDetailCom(AppData.Component);
                    dlgcom.ShowDialog();
                    break;
                case 1:
                    FrmDetailFunc dlgfunc = new FrmDetailFunc(AppData.Component.Functions[tn.Index]);
                    dlgfunc.ShowDialog();
                    break;
                case 2:
                    FrmDetailParam dlgparam = new FrmDetailParam(AppData.Component.Functions[tn.Parent.Index].Parameters[tn.Index]);
                    dlgparam.ShowDialog();
                    break;
            }
            FormLayout_UpdateForm();
        }

        private void MnuHelp_Guide_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\用户手册_组件发布器MINI.pdf");
        }
    }
}
