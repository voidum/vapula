using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.Designer
{
    public partial class FrmExplorer : Window
    {
        public FrmExplorer()
        {
            Id = "explorer";
            DefaultDock = DockState.DockRight;
            InitializeComponent();
            treeview.ImageList = new ImageList();
            treeview.ImageList.Images.Add("lib", Properties.Resources.library_s);
            treeview.ImageList.Images.Add("func", Properties.Resources.function_s);
            treeview.ImageList.Images.Add("param", Properties.Resources.parameter_s);
        }

        public void UI_UpdateLibrary()
        {
            if (!UI_ValidNonNull())
                return;
            var lib = App.CurrentLibrary;
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
            var lib = App.CurrentLibrary;
            if (lib == null)
            {
                //toolbar.Enabled = false;
                //treeview.Nodes.Clear();
                return false;
            }
            else
            {
                //toolbar.Enabled = true;
                return true;
            }
        }

        public override object Sync(string cmd, object attach)
        {
            if (cmd == "update")
            {
                //doc = new FrmDocument();
                //doc.Show(paneldock, DockState.Document);
                UI_UpdateLibrary();
            }
            return null;
        }

        private void treeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = treeview.SelectedNode;
            if (tn == null)
                return;
            tn.SelectedImageIndex = tn.Level;
        }

        private void BtRefresh_Click(object sender, System.EventArgs e)
        {
            UI_UpdateLibrary();
        }
    }
}
