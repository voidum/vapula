using System;
using System.Windows.Forms;
using Vapula.Flow;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.Designer
{
    public partial class FrmStage : DockContent, IWindow
    {
        private WindowHub.State _State;

        private AppData App
        {
            get { return AppData.Instance; }
        }

        public FrmStage()
        {
            InitializeComponent();
        }

        public void UI_Clear()
        {
            if (InvokeRequired)
            {
                var action = new Action(UI_Clear);
                action.Invoke();
            }
            else 
            {
                LsvStages.Items.Clear();
            }
        }

        public void UI_AddItem(Stage stage)
        {
            if (InvokeRequired)
            {
                var action = new Action<Stage>(UI_AddItem);
                action.Invoke(stage);
            }
            else
            {
                string str_node = "";
                foreach (var node in stage.Nodes)
                    str_node += node.Id.ToString() + " ";
                var lvi = new ListViewItem(
                    new string[]{stage.Id.ToString(), str_node});
                LsvStages.Items.Add(lvi);
            }
        }

        public string Id
        {
            get { return "stage"; }
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

        private void FrmStage_FormClosing(object sender, FormClosingEventArgs e)
        {
            State = WindowHub.State.Hidden;
            e.Cancel = true;
        }
    }
}
