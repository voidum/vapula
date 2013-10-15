using System.Windows.Forms;

namespace TCM.ComManager
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            LsvCom.Items.Add(new ListViewItem("test"));
            LsvCom.Items.Add(new ListViewItem("test"));
        }
    }
}
