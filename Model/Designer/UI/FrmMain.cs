using System;
using System.Drawing;
using System.Windows.Forms;
using TCM.Model;
using RibbonLib;
using RibbonLib.Interop;

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
            canvas.AddConnection(new Point(50, 50), new Point(100, 100));
        }
    }
}
