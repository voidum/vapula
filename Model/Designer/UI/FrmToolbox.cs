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
            _LargeIcons.Images.Add("!process", Properties.Resources.function);
            _LargeIcons.Images.Add("!code", Properties.Resources.script);
            _LargeIcons.Images.Add("!decision", Properties.Resources.branch);
            _LargeIcons.Images.Add("!variable",Properties.Resources.datatable);
            //小图标
            _SmallIcons.Images.Add("!process", Properties.Resources.function_s);
            _SmallIcons.Images.Add("!code", Properties.Resources.script_s);
            _SmallIcons.Images.Add("!decision", Properties.Resources.branch_s);
            _SmallIcons.Images.Add("!variable", Properties.Resources.datatable_s);
        }

        private void FormLayout_AddAdvancedTools()
        {
            ListViewGroup lvg = new ListViewGroup("!expert", "专家工具");
            LsvTools.Groups.Add(lvg);
            LsvTools.SetGroupState(lvg,
                ListViewGroupState.Normal |
                ListViewGroupState.Collapsible);

            ListViewItem lvi1 = new ListViewItem("决策", "!decision", lvg);
            lvi1.Tag = "decision";

            ListViewItem lvi2 = new ListViewItem("代码", "!code", lvg);
            lvi2.Tag = "code";

            ListViewItem lvi3 = new ListViewItem("变量", "!variable", lvg);
            lvi3.Tag = "variable";
            
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
                    string icon_key = "!process";
                    if (tags["LargeIcon"] != null || tags["SmallIcon"] != null)
                    {
                        icon_key = lib.Id + ":" + func.Id.ToString();
                        _LargeIcons.Images.Add(icon_key, (Image)tags["LargeIcon"]);
                        _SmallIcons.Images.Add(icon_key, (Image)tags["SmallIcon"]);
                    }
                    ListViewItem lvi = new ListViewItem(lvi_text, icon_key, lvg);
                    lvi.Tag = func;

                    LsvTools.Items.Add(lvi);
                }
            }
        }

        public bool IsAdvancedTool(ListViewItem lvi)
        {
            return !(lvi.Tag is Function);
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
            FormLayout_AddAdvancedTools();
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
            if (lvi != null)
                LsvTools.DoDragDrop(lvi, DragDropEffects.Copy);
        }
    }
}
