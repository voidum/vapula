using System;
using System.Windows.Forms;
using Vapula.Model;

namespace Vapula.Toolkit
{
    public partial class FrmFunction : Form
    {
        private Method _Model = null;
        public Method Model
        {
            get { return _Model; }
            set
            {
                _Model = value;
                TbxId.Text = value.Id;
                TbxEntry.Text = value.Entry;
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
            _Model.Id = TbxId.Text;
            _Model.Entry = TbxEntry.Text;
            _Model.Name = TbxName.Text;
            _Model.Description = TbxDescription.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
