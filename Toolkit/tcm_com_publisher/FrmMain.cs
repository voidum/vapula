using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using TCM.Models;
using TCM.Toolkit;

namespace TCM.ComPublisher
{
    public partial class FrmMain : Form
    {
        private bool _IfPublishNew = false;

        private void FormLayout_UpdateForm()
        {
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            if(AppData.Component != null) 
            {
                string tntext = "组件：" +
                    (AppData.Component.Name != null ? AppData.Component.Name : "[未指定名称]") + "(" +
                    (AppData.Component.Id != null ? AppData.Component.Id : "未指定标识") + ")";
                TreeNode tn_com = new TreeNode(tntext);
                foreach (Function model_func in AppData.Component.Children)
                {
                    tntext = "功能：" + (model_func.Name != null ? model_func.Name : "[未指定名称]") + "(" + model_func.Id.ToString() + ")";
                    TreeNode tn_func = new TreeNode(tntext);
                    foreach (Parameter model_param in model_func.Children)
                    {
                        tntext = "参数：" + (model_param.Name != null ? model_param.Name : "[未指定名称]") + "(" + model_param.Id.ToString() + ")";
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
            DlgFileOpen.FileName = "";
            DlgFileOpen.Filter = filter;
            DlgFileOpen.Title = title;
            DlgFileOpen.DefaultExt = ext;
            return (DlgFileOpen.ShowDialog() == DialogResult.OK);
        }
        
        public FrmMain()
        {
            InitializeComponent();
        }
        
        private void MnuCom_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MnuCom_New_Click(object sender, EventArgs e)
        {
            if (!FormLayout_DlgOpenFile("组件PE文件|*.dll", "选择组件PE文件", "dll")) return;
            string comid = IOHelper.GetFileNameNoExt(DlgFileOpen.SafeFileName);
            string compath = IOHelper.GetFullPath(DlgFileOpen.FileName, true);
            if (Regex.IsMatch(comid, "[^a-zA-Z0-9_]"))
            {
                if (MessageBox.Show("目标文件名称包含不符合要求的字符，是否自动重命名?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        comid = Regex.Replace(comid, "[^a-zA-Z0-9_]+", "");
                        if (comid.Trim() == "")
                        {
                            MessageBox.Show("目标文件名称完全由不符合要求的字符组成。\n请自行重命名后再发布。", "注意");
                            return;
                        }
                        File.Move(DlgFileOpen.FileName, compath + comid + ".dll");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("重命名目标时发生错误。\n" + ex.Message);
                        return;
                    }
                }
                else return;
            }
            if (AppData.IfExistCom(comid))
            {
                MessageBox.Show("目标组件标识和已有组件标识冲突。\n请重命名目标组件。","注意");
                return;
            }

            MnuCom_Publish.Enabled = true;
            AppData.Component = null;
            AppData.Component = new Component();
            AppData.Component.Id = comid;
            AppData.PathLibSrc = compath + comid + ".dll";
            Function model_func = new Function();
            AppData.Component.Children.Add(model_func);
            FormLayout_UpdateForm();
            propertygrid.SelectedObject = AppData.Component;
            _IfPublishNew = true;
        }
        private void MnuCom_Load_Click(object sender, EventArgs e)
        {
            if (!FormLayout_DlgOpenFile("组件配置文件|*.tcm.xml", "选择组件配置文件", ".tcm.xml")) return;
            XDocument xmldoc = XDocument.Load(DlgFileOpen.FileName);
            XElement xecom = xmldoc.Element("root").Element("component");
            string comid = xecom.Attribute("id").Value;
            AppData.Component = null;
            AppData.Component = Component.Parse(xecom);
            foreach (Function func in AppData.Component.Children)
            {
                string funcicon = AppData.Config["DirRes"] + comid + "." + func.Id.ToString() + ".tcm.png";
                if (File.Exists(funcicon))
                    func.Icon = Image.FromFile(funcicon);
            }
            MnuCom_Publish.Enabled = true;
            FormLayout_UpdateForm();
            _IfPublishNew = false;
        }
        
        private void MnuCom_Publish_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument xmldoc = AppData.Component.ToFullXml();
                xmldoc.Save(AppData.PathCfgDst);
                XElement xecom = xmldoc.Element("root").Element("component");
                var xes = (from xe in AppData.XmlComLst.Element("root").Elements("component")
                           where xe.Attribute("id").Value == xecom.Attribute("id").Value
                           select xe);
                xes.Remove();
                AppData.XmlComLst.Element("root").Add(xecom);
                AppData.XmlComLst.Save(AppData.Config["PathLst"]);
                if (_IfPublishNew)
                {
                    File.Copy(AppData.PathLibSrc, AppData.PathLibDst);
                    foreach (Function func in AppData.Component.Children)
                    {
                        if (func.Icon != null)
                            func.Icon.Save(AppData.Config["DirRes"] + AppData.Component.Id + "." + func.Id.ToString() + ".tcm.png");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发布时发生错误。\n" + ex.Message, "注意");
                return;
            }
            MessageBox.Show("组件发布成功。");
        }
        private void treeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = treeview.SelectedNode;
            switch (tn.Level) 
            {
                case 0:
                    propertygrid.SelectedObject = AppData.Component;
                    break;
                case 1:
                    propertygrid.SelectedObject = AppData.Component.Children[tn.Index];
                    break;
                case 2:
                    propertygrid.SelectedObject = AppData.Component.Children[tn.Parent.Index].Children[tn.Index];
                    break;
            }
        }
        private void propertygrid_Leave(object sender, EventArgs e)
        {
            FormLayout_UpdateForm();
        }

        private void BtAddParam_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeview.SelectedNode;
            if (tn == null)
            {
                if (AppData.Component == null)
                {
                    MessageBox.Show("请先新建发布任务或打开配置。","提示");
                    return;
                }
            }
            else
            {
                Parameter model_param = null;
                switch (tn.Level)
                {
                    case 1:
                        model_param = new Parameter();
                        AppData.Component.Children[tn.Index].Children.Add(model_param);
                        propertygrid.SelectedObject = model_param;
                        AppData.Component.Children[tn.Index].UpdateID();
                        break;
                    case 2:
                        model_param = new Parameter();
                        AppData.Component.Children[tn.Parent.Index].Children.Insert(tn.Index, model_param);
                        propertygrid.SelectedObject = model_param;
                        AppData.Component.Children[tn.Parent.Index].UpdateID();
                        break;
                }
            }
            FormLayout_UpdateForm();
        }
        private void BtAddFunc_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeview.SelectedNode;
            if (tn == null)
            {
                if (AppData.Component == null)
                {
                    MessageBox.Show("请先新建发布任务或打开配置。", "提示");
                    return;
                }
            }
            else
            {
                Function model_func = null;
                switch (tn.Level)
                {
                    case 0:
                        model_func = new Function();
                        AppData.Component.Children.Add(model_func);
                        propertygrid.SelectedObject = model_func;
                        break;
                    case 1:
                        model_func = new Function();
                        AppData.Component.Children.Insert(tn.Index, model_func);
                        propertygrid.SelectedObject = model_func;
                        break;
                }
            }
            AppData.Component.UpdateID();
            FormLayout_UpdateForm();
        }
        private void BtRemoveItem_Click(object sender, EventArgs e)
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
                        AppData.Component.Children[tn.Index].Clear();
                        AppData.Component.Children.RemoveAt(tn.Index);
                        AppData.Component.UpdateID();
                        break;
                    case 2:
                        AppData.Component.Children[tn.Parent.Index].Children.RemoveAt(tn.Index);
                        AppData.Component.Children[tn.Parent.Index].UpdateID();
                        break;
                }
            }
            propertygrid.SelectedObject = null;
            FormLayout_UpdateForm();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            MnuCom_Publish.Enabled = false;
        }

        private void MnuTool_SetPublish_Click(object sender, EventArgs e)
        {
            FrmSetPublish DlgSetPublish = new FrmSetPublish();
            DlgSetPublish.ShowDialog();
        }

        private void MnuHelp_Guide_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\用户手册_组件发布器.pdf");
        }
    }

}
