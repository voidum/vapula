using System.Windows.Forms;

namespace Vapula.Designer
{
    public partial class FrmParamValue : Form
    {
        private DataType _Type;
        public DataType Type
        {
            get { return _Type; }
        }

        public string Value 
        {
            get 
            {
                if (string.IsNullOrWhiteSpace(TbxValue.Text))
                    return null;
                return TbxValue.Text; 
            }
            set 
            { 
                TbxValue.Text = value; 
            }
        }

        public FrmParamValue(DataType type)
        {
            InitializeComponent();
        }

        private void BtCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtOK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
