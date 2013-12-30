using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.Designer
{
    public partial class FrmWorkspace : DockContent, IWindow
    {
        private WindowHub.State _State;

        private AppData App
        {
            get { return AppData.Instance; }
        }

        public FrmWorkspace()
        {
            InitializeComponent();
        }

        public string Id
        {
            get { return "workspace"; }
        }

        public WindowHub.State State
        {
            get { return _State; }
            set
            {
                if (_State == value)
                    return;
                if (value == WindowHub.State.Visible)
                    App.MainWindow.UI_ShowWindow(this, DockState.DockRight);
                else Hide();
                _State = value;
            }
        }

        private void FrmWorkspace_FormClosing(object sender, FormClosingEventArgs e)
        {
            State = WindowHub.State.Hidden;
            e.Cancel = true;
        }
    }
}
