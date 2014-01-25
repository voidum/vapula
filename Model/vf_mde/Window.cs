using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.MDE
{
    public class Window : DockContent
    {
        protected string _Id;
        protected DockState _DefaultDock;
        protected WindowHub.State _State;

        public Window()
        {
            FormClosing += new FormClosingEventHandler(Window_FormClosing);
        }

        public AppData App
        {
            get { return AppData.Instance; }
        }

        public string Id 
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public DockState DefaultDock
        {
            get { return _DefaultDock; }
            set { _DefaultDock = value; }
        }

        public WindowHub.State State 
        {
            get { return _State; }
            set
            {
                if (_State == value)
                    return;
                if (value == WindowHub.State.Visible)
                {
                    bool unknown = (DockState == DockState.Unknown || DockState == DockState.Hidden);
                    App.MainWindow.UI_ShowWindow(this, unknown ? DefaultDock : DockState);
                }
                else
                    Hide();
                _State = value;
            }
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
                return;
            State = WindowHub.State.Hidden;
            e.Cancel = true;
        }

        public virtual object Sync(string cmd, object attach) 
        {
            throw new NotImplementedException();
        }
    }
}
