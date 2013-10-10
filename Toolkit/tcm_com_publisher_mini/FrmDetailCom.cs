using System;
using System.Windows.Forms;
using TCM.Model;

namespace TCM.ComPubMini
{
    public partial class FrmDetailCom : Form
    {
        private Component _Component = null;
        public Component Component 
        {
            get { return _Component; }
        }

        public FrmDetailCom(Component com)
        {
            InitializeComponent();
            _Component = com;
            TbxId.Text = com.Id;
            TbxName.Text = com.Name;
            TbxVersion.Text = com.Version;
            TbxPublisher.Text = com.Publisher;
            TbxDescription.Text = com.Description;
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            _Component.Name = TbxName.Text;
            _Component.Version = TbxVersion.Text;
            _Component.Publisher = TbxPublisher.Text;
            _Component.Description = TbxDescription.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
