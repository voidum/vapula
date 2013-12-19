using System;
using System.Windows.Forms;
using Vapula.Model;

namespace Vapula.Toolkit
{
    public partial class FrmFunction : Form
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

        public FrmFunction()
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
