﻿using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.Designer
{
    public partial class FrmDebug : DockContent, IWindow
    {
        private WindowHub.State _State;

        private AppData App
        {
            get { return AppData.Instance; }
        }

        public FrmDebug()
        {
            InitializeComponent();
        }

        public void SelectObject(object obj)
        {
            property.SelectedObject = obj;
        }

        public string Id
        {
            get { return "debug"; }
        }

        public WindowHub.State State
        {
            get { return _State; }
            set
            {
                if (_State == value)
                    return;
                if (value == WindowHub.State.Visible)
                    App.MainWindow.UI_ShowWindow(this, DockState.DockRightAutoHide);
                else Hide();
                _State = value;
            }
        }

        private void FrmDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            State = WindowHub.State.Hidden;
            e.Cancel = true;
        }
    }
}
