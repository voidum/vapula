using System;
using System.Windows.Forms;
using TCM.Model;

namespace TCM.ComPubMini
{
    public partial class FrmDetailFunc : Form
    {
        private Function _Function = null;
        public Function Function
        {
            get { return _Function; }
        }

        public FrmDetailFunc(Function func)
        {
            InitializeComponent();
            _Function = func;
            TbxId.Text = func.Id.ToString();
            TbxName.Text = func.Name;
            TbxDescription.Text = func.Description;
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            _Function.Name = TbxName.Text;
            _Function.Description = TbxDescription.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
