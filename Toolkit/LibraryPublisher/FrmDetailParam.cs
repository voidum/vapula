using System;
using System.Windows.Forms;
using TCM.Model;

namespace TCM.Toolkit
{
    public partial class FrmDetailParam : Form
    {
        private int[] _TypeValues = 
            new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 20, 21, 22 };

        public int GetIndexByType(DataType type) 
        {
            int tv = (int)type;
            for (int i =0;i<_TypeValues.Length;i++) 
                if (_TypeValues[i] == tv) return i;
            return -1;
        }

        private Parameter _Parameter = null;
        public Parameter Parameter
        {
            get { return _Parameter; }
            set
            {
                _Parameter = value;
                TbxId.Text = value.Id.ToString();
                TbxName.Text = value.Name;
                TbxCatalog.Text = value.Catalog;
                TbxDescription.Text = value.Description;
                ChbxIn.Checked = value.IsIn;
                CobxType.SelectedIndex = GetIndexByType(_Parameter.Type);
            }
        }

        public FrmDetailParam()
        {
            InitializeComponent();
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            _Parameter.Name = TbxName.Text;
            _Parameter.Catalog = TbxCatalog.Text;
            _Parameter.Description = TbxDescription.Text;
            _Parameter.IsIn = ChbxIn.Checked;
            _Parameter.Type = (DataType)_TypeValues[CobxType.SelectedIndex];
            DialogResult = DialogResult.OK;
        }
    }
}
