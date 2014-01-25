using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Irisecol;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.MDE
{
    public partial class FrmToolbox : Window
    {
        private ImageList _LargeIcons = new ImageList();
        private ImageList _SmallIcons = new ImageList();

        private string AppResDir
        {
            get 
            {
                string dir = Path.Combine(
                    Application.StartupPath,
                    App.Config["PathResource"]);
                return dir;
            }
        }

        private void UI_SwitchCollapse(bool collapse = false)
        {
            foreach (ListViewGroup lvg in LsvTools.Groups)
                LsvTools.SetGroupState(lvg,
                    (collapse ? IricListView.GroupState.Collapsed : IricListView.GroupState.Normal) |
                    IricListView.GroupState.Collapsible);
        }

        private void UI_LoadCommonRes()
        {
            var mng = 
                Properties.Resources.ResourceManager;
            _LargeIcons.Images.Add("!process", 
                (Image)mng.GetObject("function_l"));
            _SmallIcons.Images.Add("!process", 
                (Image)mng.GetObject("function_s"));
        }

        public FrmToolbox()
        {
            Id = "toolbox";
            DefaultDock = DockState.DockLeft;
            InitializeComponent();
            _LargeIcons.ImageSize = new Size(32, 32);
            _SmallIcons.ImageSize = new Size(16, 16);
            LsvTools.LargeImageList = _LargeIcons;
            LsvTools.SmallImageList = _SmallIcons;
        }

        private void FrmToolbox_Load(object sender, EventArgs e)
        {
            LsvTools.View = View.LargeIcon;
            UI_LoadCommonRes();
            FormLayout_LoadAdvancedTools();
            FormLayout_LoadLibraries();
            UI_SwitchCollapse();
        }

        private void LsvTools_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var lvi = e.Item as ListViewItem;
            if (lvi != null)
                LsvTools.DoDragDrop(lvi, DragDropEffects.Copy);
        }

        private void MnuCollapseGroup_Click(object sender, EventArgs e)
        {
            UI_SwitchCollapse(true);
        }

        private void MnuExpandGroup_Click(object sender, EventArgs e)
        {
            UI_SwitchCollapse(false);
        }

        private void MnuSwitchView_Click(object sender, EventArgs e)
        {
            if (LsvTools.View == View.LargeIcon)
                LsvTools.View = View.SmallIcon;
            else
                LsvTools.View = View.LargeIcon;
        }

        public override object Sync(string cmd, object attach)
        {
            throw new NotImplementedException();
        }
    }
}
