using xDockPanel;
using System.Windows.Forms;

namespace TCM.Model.Designer
{
    public partial class FrmToolbox : DockContent
    {
        public FrmToolbox()
        {
            InitializeComponent();
        }

        private void FrmToolbox_Load(object sender, System.EventArgs e)
        {
            LsvTools.View = View.LargeIcon;
            ListViewGroup lvg1 = new ListViewGroup("test1");
            ListViewGroup lvg2 = new ListViewGroup("test2");
            LsvTools.Groups.Add(lvg1);
            LsvTools.Groups.Add(lvg2);
            LsvTools.SetGroupState(lvg1, ListViewGroupState.Collapsible);
            LsvTools.SetGroupState(lvg2, ListViewGroupState.Collapsible);
            LsvTools.SetGroupFooter(lvg1, "实验");
            ListViewItem lvi1 = new ListViewItem("abc", lvg1);
            ListViewItem lvi2 = new ListViewItem("abc", lvg1);
            ListViewItem lvi3 = new ListViewItem("abc", lvg2);
            ListViewItem lvi4 = new ListViewItem("abc", lvg2);
            LsvTools.Items.Add(lvi1);
            LsvTools.Items.Add(lvi2);
            LsvTools.Items.Add(lvi3);
            LsvTools.Items.Add(lvi4);
        }
    }
}
