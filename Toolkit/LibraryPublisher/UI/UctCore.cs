using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Vapula.Model;
using System.Xml.Linq;

namespace Vapula.Toolkit
{
    public partial class UctCore : UserControl
    {
        private void UpdateID<T>(List<T> source, Action<T, int> setter)
        {
            int i = 1;
            foreach (T e in source)
            {
                setter(e, i);
                i++;
            }
        }

        public void Publish()
        {
            AppData app = AppData.Instance;
            try
            {
                var xdoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    app.Library.ToXML());
                xdoc.Save(app.PathDpt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发布时发生错误。\n" + ex.Message, "注意");
                return;
            }
            MessageBox.Show("组件发布成功。\n发布位置：" + app.PathDpt, "组件发布器");
        }

        public void UI_UpdateLibrary()
        {
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            Library lib = AppData.Instance.Library;
            if (lib != null)
            {
                string title = string.Format("{0}[{1}]",
                    !string.IsNullOrWhiteSpace(lib.Name) ? lib.Name : "（未命名）",
                    !string.IsNullOrWhiteSpace(lib.Id) ? lib.Id : "（未选择库）" );
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

        public bool UI_ValidNonNull()
        {
            Library lib = AppData.Instance.Library;
            if (lib == null)
            {
                MessageBox.Show(
                    "没有打开的发布任务。\n" + 
                    "请新建或打开发布任务。", "提示");
                return false;
            }
            return true;
        }

        public void UI_Clear()
        {
            treeview.Nodes.Clear();
        }

        public UctCore()
        {
            InitializeComponent();
        }

        private void BtLoadLibrary_Click(object sender, EventArgs e)
        {
            if (!UI_ValidNonNull())
                return;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "选择组件的库主体文件";
            dlg.Filter = "任意文件|*.*";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            string id = Path.GetFileNameWithoutExtension(dlg.SafeFileName);
            if (Regex.IsMatch(id, "[^A-Za-z0-9._]"))
            {
                MessageBox.Show(
                    "目标名称中包含不符合规范的字符。\n" + 
                    "请依据字符集正则约束[a-zA-Z0-9._]自行重命名。", 
                    "注意");
                return;
            }
            AppData app = AppData.Instance;
            app.PathBin = dlg.FileName;
            app.Library.Id = id;
            UI_UpdateLibrary();
        }

        private void BtAddParam_Click(object sender, EventArgs e)
        {
            if (!UI_ValidNonNull())
                return;
            Library lib = AppData.Instance.Library;
            TreeNode tn = treeview.SelectedNode;
            if (tn == null) 
                return;
            Parameter model_param = new Parameter();
            model_param.Type = DataType.Int32;
            model_param.Mode = ParamMode.In;
            switch (tn.Level)
            {
                case 1:
                    lib.Functions[tn.Index].Parameters.Add(model_param);
                    UpdateID(
                        lib.Functions[tn.Index].Parameters,
                        (d, v) => { d.Id = v; });
                    break;
                case 2:
                    lib.Functions[tn.Parent.Index].Parameters.Insert(tn.Index, model_param);
                    UpdateID(
                        lib.Functions[tn.Parent.Index].Parameters,
                        (d, v) => { d.Id = v; });
                    break;
            }
            UI_UpdateLibrary();
        }

        private void BtAddFunc_Click(object sender, EventArgs e)
        {
            if (!UI_ValidNonNull())
                return;
            Library lib = AppData.Instance.Library;
            TreeNode tn = treeview.SelectedNode;
            if (tn == null)
                return;
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
            UI_UpdateLibrary();
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
            if (MessageBox.Show("确认移除选中项吗？", "询问", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question) 
                == DialogResult.Yes)
            {
                switch (tn.Level)
                {
                    case 0:
                        lib = null;
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
                UI_UpdateLibrary();
            }
        }

        private void BtProperty_Click(object sender, EventArgs e)
        {
            if (!UI_ValidNonNull())
                return;
            Library lib = AppData.Instance.Library;
            TreeNode tn = treeview.SelectedNode;
            if (tn == null) 
                return;
            switch (tn.Level)
            {
                case 0:
                    FrmLibrary dlgcom = new FrmLibrary();
                    dlgcom.Library = lib;
                    dlgcom.ShowDialog();
                    break;
                case 1:
                    FrmFunction dlgfunc = new FrmFunction();
                    dlgfunc.Function = lib.Functions[tn.Index];
                    dlgfunc.ShowDialog();
                    break;
                case 2:
                    FrmParameter dlgparam = new FrmParameter();
                    dlgparam.Parameter = lib.Functions[tn.Parent.Index].Parameters[tn.Index];
                    dlgparam.ShowDialog();
                    break;
            }
            UI_UpdateLibrary();
        }

    }
}
