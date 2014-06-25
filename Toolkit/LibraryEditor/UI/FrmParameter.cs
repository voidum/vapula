using System;
using System.Windows.Forms;
using Vapula.Model;

namespace Vapula.Toolkit
{
    public partial class FrmParameter : Form
    {
        private int[] _ParamModeValues
            = new int[] { 0, 1, 2 };
        private string[] _ParamModeNames
            = new string[] { "仅输入", "仅输出", "输入和输出" };

        public int GetParamModeIndex(ParamMode mode)
        {
            int v = (int)mode;
            for (int i = 0; i < _ParamModeValues.Length; i++)
                if (_ParamModeValues[i] == v)
                    return i;
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
                TbxDescription.Text = value.Description;
                CobxParamMode.SelectedIndex = GetParamModeIndex(_Parameter.Mode);
            }
        }

        public FrmParameter()
        {
            InitializeComponent();
            CobxParamMode.Items.Clear();
            foreach (string s in _ParamModeNames)
                CobxParamMode.Items.Add(s);
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            _Parameter.Name = TbxName.Text;
            _Parameter.Description = TbxDescription.Text;
            _Parameter.Mode = (ParamMode)_ParamModeValues[CobxParamMode.SelectedIndex];
            DialogResult = DialogResult.OK;
        }
    }
}