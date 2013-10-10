using System;
using System.Windows.Forms;

namespace PIE.Controls
{
    public partial class DlgInputText : Form
    {
        public string Result 
        {
            get { return TbxValue.Text; }
        }

        public DlgInputText(string description = null, string caption = null, string defvalue = null)
        {
            InitializeComponent();
            if (caption != null) Text = caption;
            if (description != null) LblText.Text = description;
            if (defvalue != null) TbxValue.Text = defvalue;
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
