using System;
using System.Windows.Forms;
using Vapula.Designer.UI;
using Vapula.Model;
using xDockPanel;

namespace Vapula.Designer
{
    public partial class FrmDocument : DockContent
    {
        private AppData App = AppData.Instance;

        UctGraph _UctGraph = new UctGraph();
        UctData _UctData = new UctData();

        public Graph Graph 
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
    }
}
