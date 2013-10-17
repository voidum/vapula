using System;
using System.Windows.Forms;
using TCM.Model;

namespace TCM.Toolkit
{
    public partial class FrmDetailFunc : Form
    {
        private Function _Function = null;
        public Function Function
        {
            get { return _Function; }
            set
            {
                _Function = value;
                TbxId.Text = value.Id.ToString();
                TbxName.Text = value.Name;
                TbxDescription.Text = value.Description;
            }
        }

        public FrmDetailFunc()
        {
            InitializeComponent();
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
