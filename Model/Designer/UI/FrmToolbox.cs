using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmToolbox : DockContent
    {
        private string AppResDir
        {
            get 
            {
                string dir = Application.StartupPath 
                    + "\\" 
                    + AppData.Instance.Config["AppResDir"];
                return dir;
            }
        }
        private ImageList _LargeIcons = new ImageList();
        private ImageList _SmallIcons = new ImageList();

        private void FormLayout_SwitchCollapse(bool collapse = false)
        {
            if (collapse)
            {
                foreach (ListViewGroup lvg in LsvTools.Groups)
                    LsvTools.SetGroupState(lvg, 
                        ListViewGroupState.Collapsed |
                        ListViewGroupState.Collapsible);
            }
            else 
            {
                foreach (ListViewGroup lvg in LsvTools.Groups)
                    LsvTools.SetGroupState(lvg,
                        ListViewGroupState.Normal |
                        ListViewGroupState.Collapsible);
            }
        }

        private void FormLayout_LoadResource()
        {
            //大图标
            _LargeIcons.Images.Add("!basic_process", Properties.Resources.function);
            _LargeIcons.Images.Add("!basic_decision",Properties.Resources.branch);
            _LargeIcons.Images.Add("!basic_variable",Properties.Resources.datatable);
            _LargeIcons.Images.Add("!basic_batch", Properties.Resources.copies);
            //小图标
            _SmallIcons.Images.Add("!basic_process", Properties.Resources.function_s);
            _SmallIcons.Images.Add("!basic_decision", Properties.Resources.branch_s);
            _SmallIcons.Images.Add("!basic_variable", Properties.Resources.datatable_s);
            _SmallIcons.Images.Add("!basic_batch", Properties.Resources.copies_s);
        }

        private void FormLayout_AddBasicTools()
        {
            ListViewGroup lvg = new ListViewGroup("!basic", "基本工具");
            LsvTools.Groups.Add(lvg);
            LsvTools.SetGroupState(lvg,
                ListViewGroupState.Normal |
                ListViewGroupState.Collapsible);
            ListViewItem lvi1 = new ListViewItem("决策", "!basic_decision", lvg);
            ListViewItem lvi2 = new ListViewItem("变量表", "!basic_variable", lvg);
            ListViewItem lvi3 = new ListViewItem("批处理", "!basic_batch", lvg);
            LsvTools.Items.Add(lvi1);
            LsvTools.Items.Add(lvi2);
            LsvTools.Items.Add(lvi3);
        }

        private void FormLayout_LoadAllLibraries()
        {
            LibraryManager mng = AppData.Instance.LibManager;
            var libs = mng.Libraries;
            foreach (Library lib in libs)
            {
                string lvg_header =
                    (lib.Name == "" ? "（" + lib.Id + "）" : lib.Name);
                ListViewGroup lvg = new ListViewGroup(lib.Id, lvg_header);
                LsvTools.Groups.Add(lvg);
                LsvTools.SetGroupState(lvg,
                    ListViewGroupState.Normal |
                    ListViewGroupState.Collapsible);
                foreach (Function func in lib.Functions)
                {
                    string lvi_text =
                        (func.Name == "" ? "（" + func.Id + "）" : func.Name);
                    var tags = (Dictionary<string, object>)func.Tag;
                    string icon_key = "!basic_process";
                    if (tags.ContainsKey("LargeIcon"))
                    {
                        _LargeIcons.Images.Add(icon_key, (Image)tags["LargeIcon"]);
                        _SmallIcons.Images.Add(icon_key, (Image)tags["SmallIcon"]);
                        icon_key = lib.Id + ":" + func.Id.ToString();
                    }
                    ListViewItem lvi = new ListViewItem(lvi_text, icon_key, lvg);
                    lvi.Tag = func;

                    LsvTools.Items.Add(lvi);
                }
            }
        }

        private string GetBasicToolId(ListViewItem lvi)
        {
            string key = lvi.ImageKey;
            if (key.Contains("!basic_") &&
                !key.Contains("!basic_process"))
                return key.Substring("!basic_".Length);
            else
                return null;
        }

        public FrmToolbox()
        {
            InitializeComponent();
            _LargeIcons.ImageSize = new Size(32, 32);
            _SmallIcons.ImageSize = new Size(16, 16);
            LsvTools.LargeImageList = _LargeIcons;
            LsvTools.SmallImageList = _SmallIcons;
        }

        private void FrmToolbox_Load(object sender, EventArgs e)
        {
            FormLayout_LoadResource();
            LsvTools.View = View.LargeIcon;
            FormLayout_AddBasicTools();
            FormLayout_LoadAllLibraries();
            FormLayout_SwitchCollapse();
        }

        private void MnuCollapseGroup_Click(object sender, EventArgs e)
        {
            FormLayout_SwitchCollapse(true);
        }

        private void MnuExpandGroup_Click(object sender, EventArgs e)
        {
            FormLayout_SwitchCollapse(false);
        }

        private void LsvTools_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem lvi = e.Item as ListViewItem;
            string key = GetBasicToolId(lvi);
            if (key == null)
            {
                NodeProcess node_process = new NodeProcess();
                node_process.Function = lvi.Tag as Function;
                LsvTools.DoDragDrop(node_process, DragDropEffects.Copy);
                return;
            }
            else if(key == "decision")
            {
                NodeDecision node_decision = new NodeDecision();
                LsvTools.DoDragDrop(node_decision, DragDropEffects.Copy);
                return;
            }
            else if(key == "variable")
            {
            }
            else if(key == "batch")
            {
            }
        }
    }
}
