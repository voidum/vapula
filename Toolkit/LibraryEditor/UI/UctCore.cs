using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using Vapula.Model;

namespace Vapula.Toolkit
{
    public partial class UctCore : UserControl
    {
        private void UpdateParamID(List<Parameter> param_arr)
        {
            int i = 1;
            foreach (var param in param_arr)
                param.Id = i++;
        }

        private bool ValidId(string id)
        {
            return Regex.IsMatch(id, "[^A-Za-z0-9._]");
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
            if(!UI_ValidNonNull()) 
                return;
            var lib = AppData.Instance.Library;
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            string title = string.Format("[{1}] {0}",
                !string.IsNullOrWhiteSpace(lib.Name) ? lib.Name : "（未命名）",
                !string.IsNullOrWhiteSpace(lib.Id) ? lib.Id : "（未选择库）");
            var tn_lib = new TreeNode(title);
            tn_lib.ImageKey = "lib";
            foreach (var func in lib.Functions)
            {
                title = string.Format("[{1}] {0}",
                    !string.IsNullOrWhiteSpace(func.Name) ? func.Name : "（未命名）",
                    !string.IsNullOrWhiteSpace(func.Id) ? func.Id : "（未指定标识）");
                var tn_func = new TreeNode(title);
                tn_func.ImageKey = "func";
                foreach (var param in func.Parameters)
                {
                    title = string.Format("[{1}] {0}",
                        param.Name != "" ? param.Name : "（未命名）",
                        param.Id);
                    var tn_param = new TreeNode(title);
                    tn_param.ImageKey = "param";
                    tn_func.Nodes.Add(tn_param);
                }
                tn_lib.Nodes.Add(tn_func);
            }
            treeview.Nodes.Add(tn_lib);
            treeview.ExpandAll();
            treeview.EndUpdate();
        }

        public bool UI_ValidNonNull()
        {
            Library lib = AppData.Instance.Library;
            if (lib == null)
            {
                toolbar.Enabled = false;
                treeview.Nodes.Clear();
                return false;
            }
            else 
            {
                toolbar.Enabled = true;
                return true;
            }
        }

        public void UI_Clear()
        {
            treeview.Nodes.Clear();
        }

        public UctCore()
        {
            InitializeComponent();
            treeview.ImageList = new ImageList();
            treeview.ImageList.Images.Add("lib", Properties.Resources.library_s);
            treeview.ImageList.Images.Add("func", Properties.Resources.function_s);
            treeview.ImageList.Images.Add("param", Properties.Resources.parameter_s);
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
            if (ValidId(id))
            {
                MessageBox.Show(
                    "目标名称中包含不符合规范的字符。\n" + 
                    "请依据命名规范自行重命名。", 
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
                    UpdateParamID(lib.Functions[tn.Index].Parameters);
                    break;
                case 2:
                    lib.Functions[tn.Parent.Index].Parameters.Insert(tn.Index, model_param);
                    UpdateParamID(lib.Functions[tn.Parent.Index].Parameters);
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
                        break;
                    case 2:
                        lib.Functions[tn.Parent.Index].Parameters.RemoveAt(tn.Index);
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

        private void treeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = treeview.SelectedNode;
            if (tn == null)
                return;
            tn.SelectedImageIndex = tn.Level;
        }
    }
}
