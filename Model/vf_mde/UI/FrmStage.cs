using System;
using System.Windows.Forms;
using Vapula.Flow;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.MDE
{
    public partial class FrmStage : Window
    {
        public FrmStage()
        {
            Id = "stage";
            DefaultDock = DockState.DockRight;
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

        public override object Sync(string cmd, object attach)
        {
            throw new NotImplementedException();
        }
    }
}
