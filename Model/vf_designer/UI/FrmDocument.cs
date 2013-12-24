using System;
using System.Windows.Forms;
using Vapula.Flow;
using xDockPanel;

namespace Vapula.Designer
{
    public partial class FrmDocument : DockContent
    {
        private AppData App = AppData.Instance;

        UctGraph _UctGraph = new UctGraph();
        UctData _UctData = new UctData();

        public Graph Model 
        {
            get { return _UctGraph.Graph; }
        }

        public FrmDocument()
        {
            InitializeComponent();
            _UctGraph.Dock = DockStyle.Fill;
            _UctData.Dock = DockStyle.Fill;
            TabGraph.Controls.Add(_UctGraph);
            TabData.Controls.Add(_UctData);
        }

        private void MnuDebugCanvas_Click(object sender, EventArgs e)
        {
            App.FormDebug.SelectObject(_UctGraph.Canvas);
        }

        private void MnuDebugGraph_Click(object sender, EventArgs e)
        {
            App.FormDebug.SelectObject(_UctGraph.Graph);
        }

        private void CtrlTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = CtrlTab.SelectedIndex;
            if (index == 1)
                _UctData.UI_UpdateData(_UctGraph.Graph);
        }
    }
}
