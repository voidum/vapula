using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.MDE
{
    public partial class FrmDebug : Window
    {
        public FrmDebug() 
        {
            Id = "debug";
            DefaultDock = DockState.DockRight;
            InitializeComponent();
        }

        public void SelectObject(object obj)
        {
            property.SelectedObject = obj;
        }

        public override object Sync(string cmd, object attach)
        {
            throw new System.NotImplementedException();
        }
    }
}
