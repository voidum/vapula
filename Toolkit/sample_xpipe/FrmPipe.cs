using System;
using System.Windows.Forms;

namespace sample_xpipe
{
    public partial class FrmPipe : Form
    {
        public FrmPipe()
        {
            InitializeComponent();
        }

        private bool _AsServer;
        private string _PipeId;

        public bool AsServer
        {
            get { return _AsServer; }
        }

        public string PipeId
        {
            get { return _PipeId; }
        }

        private void ChbxAsServer_CheckedChanged(object sender, EventArgs e)
        {
            TbxPipeId.Enabled = !ChbxAsServer.Checked;
        }

        private void BtStart_Click(object sender, EventArgs e)
        {
            _AsServer = ChbxAsServer.Checked;
            _PipeId = TbxPipeId.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
