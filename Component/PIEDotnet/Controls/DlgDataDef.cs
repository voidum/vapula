using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PIE.Controls
{
    public partial class DlgDataDef : Form
    {
        public XElement XmlOutput
        {
            get { return CtrlOutput.Xml; }
        }
        public XElement XmlDataVar
        {
            get { return CtrlDataVar.Xml; }
        }

        public DlgDataDef()
        {
            InitializeComponent();
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            string errmsg;
            if (!CtrlDataVar.IsValid(out errmsg) ||
                !CtrlOutput.IsValid(out errmsg))
            {
                MessageBox.Show(errmsg, "数据验证");
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
