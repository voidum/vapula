using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.MDE
{
    public partial class FrmStartup : Window
    {
        public FrmStartup()
        {
            Id = "startup";
            DefaultDock = DockState.Document;
            InitializeComponent();
        }

        public override object Sync(string cmd, object attach)
        {
            throw new System.NotImplementedException();
        }
    }
}
