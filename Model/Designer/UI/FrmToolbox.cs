using System;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Vapula.Model;
using xDockPanel;

namespace Vapula.Designer
{
    public partial class FrmToolbox : DockContent
    {
        private string AppResDir
        {
            get 
            {
                string dir = Path.Combine(
                    Application.StartupPath,
                    AppData.Instance.Config["PathResource"]);
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

        private void FormLayout_LoadCommonRes()
        {
            ResourceManager mng = 
                Properties.Resources.ResourceManager;
            _LargeIcons.Images.Add("!process", 
                (Image)mng.GetObject("function"));
            _SmallIcons.Images.Add("!process", 
                (Image)mng.GetObject("function_s"));
        }

        private void FormLayout_LoadLibraries()
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
                foreach (var func in lib.Functions)
                {
                    string path_pre = Path.Combine(
                        AppData.Instance.PathResource,
                        func.Library.Id + "." + func.Id.ToString());
                    string path1 = path_pre + ".tcm.png";
                    string path2 = path_pre + "_s.tcm.png";
                    Image icon1 = File.Exists(path1) ? Image.FromFile(path1) : null;
                    Image icon2 = File.Exists(path2) ? Image.FromFile(path2) : null;
                    func.Tag["LargeIcon"] = icon1;
                    func.Tag["SmallIcon"] = icon2;
                    string lvi_text =
                        (func.Name == "" ? "（" + func.Id + "）" : func.Name);
                    string icon_key = "!process";
                    if (func.Tag["LargeIcon"] != null || func.Tag["SmallIcon"] != null)
                    {
                        icon_key = lib.Id + ":" + func.Id.ToString();
                        _LargeIcons.Images.Add(icon_key, (Image)func.Tag["LargeIcon"]);
                        _SmallIcons.Images.Add(icon_key, (Image)func.Tag["SmallIcon"]);
                    }
                    ListViewItem lvi = new ListViewItem(lvi_text, icon_key, lvg);
                    lvi.Tag = func;

                    LsvTools.Items.Add(lvi);
                }
            }
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
            LsvTools.View = View.LargeIcon;
            FormLayout_LoadCommonRes();
            FormLayout_LoadAdvancedTools();
            FormLayout_LoadLibraries();
            FormLayout_SwitchCollapse();
        }

        private void LsvTools_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem lvi = e.Item as ListViewItem;
            if (lvi != null)
                LsvTools.DoDragDrop(lvi, DragDropEffects.Copy);
        }

        private void MnuCollapseGroup_Click(object sender, EventArgs e)
        {
            FormLayout_SwitchCollapse(true);
        }

        private void MnuExpandGroup_Click(object sender, EventArgs e)
        {
            FormLayout_SwitchCollapse(false);
        }

        private void MnuSwitchView_Click(object sender, EventArgs e)
        {
            if (LsvTools.View == View.LargeIcon)
                LsvTools.View = View.SmallIcon;
            else
                LsvTools.View = View.LargeIcon;
        }
    }
}
