using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using TCM.Helper;
using TCM.Model;
using System.IO;

namespace TCM.Toolkit
{
    public partial class FrmMain : Form
    {
        private void UpdateID<T>(List<T> source, Action<T, int> setter)
        {
            int i = 0;
            foreach (T e in source) 
            {
                setter(e, i);
                i++;
            }
        }

        private void FormLayout_LoadLibrary()
        {
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            Library lib = AppData.Instance.Library;
            if (lib != null)
            {
                string title = string.Format("{0}[{1}]",
                    lib.Name != "" ? lib.Name : "（未命名）",
                    lib.Id);
                TreeNode tn_lib = new TreeNode(title);
                foreach (var model_func in lib.Functions)
                {
                    title = string.Format("功能{1}：{0}",
                        model_func.Name != "" ? model_func.Name : "（未命名）",
                        model_func.Id);
                    TreeNode tn_func = new TreeNode(title);
                    foreach (Parameter model_param in model_func.Parameters)
                    {
                        title = string.Format("参数{1}：{0}",
                            model_param.Name != "" ? model_param.Name : "（未命名）",
                            model_param.Id);
                        TreeNode tn_param = new TreeNode(title);
                        tn_func.Nodes.Add(tn_param);
                    }
                    tn_lib.Nodes.Add(tn_func);
                }
                treeview.Nodes.Add(tn_lib);
            }
            treeview.ExpandAll();
            treeview.EndUpdate();
        }

        private bool FormLayout_DlgOpenFile(string filter, string title, string ext)
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
            MnuLib_Publish.Enabled = false;
        }

        private void MnuLib_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MnuLib_New_Click(object sender, EventArgs e)
        {
            if (!FormLayout_DlgOpenFile("组件主体|*.*", "选择组件的主体文件", "")) return;
            string id = Path.GetFileNameWithoutExtension(dlgfile.SafeFileName);
            string dir = Path.GetDirectoryName(dlgfile.FileName);
            if (Regex.IsMatch(id, "[^A-Za-z0-9._]"))
            {
                MessageBox.Show("目标名称中包含不符合规范的字符。\n请依据字符集正则约束[a-zA-Z0-9._]自行重命名。", "注意");
                return;
            }

            Library lib = new Library();
            lib.Id = id;
            Function func = new Function();
            lib.Functions.Add(func);
            AppData.Instance.Library = lib;
            AppData.Instance.PathConfig = dir + id + ".tcm.xml";

            MnuLib_Publish.Enabled = true;
            FormLayout_LoadLibrary();
        }

        private void MnuLib_Open_Click(object sender, EventArgs e)
        {
            if (!FormLayout_DlgOpenFile("组件描述文件|*.tcm.xml", "选择组件描述文件", ".tcm.xml"))
                return;
            AppData.Instance.Library = Library.Load(dlgfile.FileName);
            if (AppData.Instance.Library != null)
            {
                AppData.Instance.PathConfig = dlgfile.FileName;
                MnuLib_Publish.Enabled = true;
                FormLayout_LoadLibrary();
            }
            else
            {
                MessageBox.Show("指定的组件描述文件未通过验证。");
            }
        }

        private void MnuLib_Publish_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument xdoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    AppData.Instance.Library.ToXML());
                xdoc.Save(AppData.Instance.PathConfig);
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
            Library lib = AppData.Instance.Library;
            if (lib == null)
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
                    lib.Functions[tn.Index].Parameters.Add(model_param);
                    UpdateID(
                        lib.Functions[tn.Index].Parameters,
                        (d, v) => { d.Id = v; });
                    break;
                case 2:
                    model_param = new Parameter();
                    lib.Functions[tn.Parent.Index].Parameters.Insert(tn.Index, model_param);
                    UpdateID(
                        lib.Functions[tn.Parent.Index].Parameters,
                        (d, v) => { d.Id = v; });
                    break;
            }
            FormLayout_LoadLibrary();
        }

        private void BtAddFunc_Click(object sender, EventArgs e)
        {
            Library lib = AppData.Instance.Library;
            if (lib == null)
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
                    lib.Functions.Add(model_func);
                    break;
                case 1:
                    model_func = new Function();
                    lib.Functions.Insert(tn.Index, model_func);
                    break;
            }
            UpdateID(
                lib.Functions,
                (d, v) => { d.Id = v; });
            FormLayout_LoadLibrary();
        }

        private void BtRemove_Click(object sender, EventArgs e)
        {
            Library lib = AppData.Instance.Library;
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
                        lib = null;
                        MnuLib_Publish.Enabled = false;
                        break;
                    case 1:
                        lib.Functions[tn.Index].Clear();
                        lib.Functions.RemoveAt(tn.Index);
                        UpdateID(
                            lib.Functions,
                            (d, v) => { d.Id = v; });
                        break;
                    case 2:
                        lib.Functions[tn.Parent.Index].Parameters.RemoveAt(tn.Index);
                        UpdateID(
                            lib.Functions[tn.Parent.Index].Parameters,
                            (d, v) => { d.Id = v; });
                        break;
                }
                FormLayout_LoadLibrary();
            }
        }

        private void BtProperty_Click(object sender, EventArgs e)
        {
            Library lib = AppData.Instance.Library;
            if (lib == null)
            {
                MessageBox.Show("首先需要新建发布任务或打开已有的配置文件。", "提示");
                return;
            }
            TreeNode tn = treeview.SelectedNode;
            if (tn == null) return;
            switch (tn.Level)
            {
                case 0:
                    FrmDetailLib dlgcom = new FrmDetailLib();
                    dlgcom.Library = lib;
                    dlgcom.ShowDialog();
                    break;
                case 1:
                    FrmDetailFunc dlgfunc = new FrmDetailFunc();
                    dlgfunc.Function = lib.Functions[tn.Index];
                    dlgfunc.ShowDialog();
                    break;
                case 2:
                    FrmDetailParam dlgparam = new FrmDetailParam();
                    dlgparam.Parameter = lib.Functions[tn.Parent.Index].Parameters[tn.Index];
                    dlgparam.ShowDialog();
                    break;
            }
            FormLayout_LoadLibrary();
        }

        private void MnuHelp_Guide_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\manual.pdf");
        }

        private void MnuHelp_About_Click(object sender, EventArgs e)
        {

        }
    }
}
