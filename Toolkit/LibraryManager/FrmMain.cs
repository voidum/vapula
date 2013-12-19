using System.Windows.Forms;

namespace Vapula.ComManager
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
