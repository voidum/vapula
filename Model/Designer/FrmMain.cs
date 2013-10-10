using System;
using System.Windows.Forms;
using TCM.Model;

namespace Model
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Library library = Library.Load(@"E:\Projects\Vapula\Core\Sample\library\sample_lib.tcm.xml");
            library.Clear();
        }
    }
}
