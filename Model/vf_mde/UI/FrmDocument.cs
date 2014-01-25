using System;
using System.Windows.Forms;
using Vapula.Flow;
using WeifenLuo.WinFormsUI.Docking;

namespace Vapula.MDE
{
    public partial class FrmDocument : Window
    {
        UctGraph _UctGraph = new UctGraph();
        UctData _UctData = new UctData();

        public Graph Model 
        {
            get { return _UctGraph.Graph; }
        }

        public FrmDocument() 
        {
            Id = "document";
            DefaultDock = DockState.Document;
            InitializeComponent();
            _UctGraph.Dock = DockStyle.Fill;
            _UctData.Dock = DockStyle.Fill;
            TabGraph.Controls.Add(_UctGraph);
            TabData.Controls.Add(_UctData);
        }

        private void MnuDebugCanvas_Click(object sender, EventArgs e)
        {
            var dlg = App.WindowHub["debug"] as FrmDebug;
            dlg.SelectObject(_UctGraph.Canvas);
        }

        private void MnuDebugGraph_Click(object sender, EventArgs e)
        {
            var dlg = App.WindowHub["debug"] as FrmDebug;
            dlg.SelectObject(_UctGraph.Graph);
        }

        private void CtrlTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = CtrlTab.SelectedIndex;
            if (index == 1)
                _UctData.UI_UpdateData(_UctGraph.Graph);
        }

        public override object Sync(string cmd, object attach)
        {
            throw new NotImplementedException();
        }
    }
}
