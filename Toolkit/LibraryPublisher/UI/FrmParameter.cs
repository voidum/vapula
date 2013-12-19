using System;
using System.Windows.Forms;
using Vapula.Model;

namespace Vapula.Toolkit
{
    public partial class FrmParameter : Form
    {
        private int[] _DataTypeValues
            = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 20, 21, 22 };
        private string[] _DataTypeNames
            = new string[] { 
                "指针", "8位整数", "16位整数", "32位整数" ,"64位整数",
                "8位无符号整数", "16位无符号整数", "32位无符号整数", "64位无符号整数",
                "32位浮点数", "64位浮点数", "布尔变量", "字符串"};

        private int[] _ParamModeValues
            = new int[] { 0, 1, 2 };
        private string[] _ParamModeNames
            = new string[] { "仅输入", "仅输出", "输入和输出" };

        public int GetDataTypeIndex(DataType type)
        {
            int v = (int)type;
            for (int i = 0; i < _DataTypeValues.Length; i++)
                if (_DataTypeValues[i] == v)
                    return i;
            return -1;
        }

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
                TbxCatalog.Text = value.Category;
                TbxDescription.Text = value.Description;
                CobxParamMode.SelectedIndex = GetParamModeIndex(_Parameter.Mode);
                CobxDataType.SelectedIndex = GetDataTypeIndex(_Parameter.Type);
            }
        }

        public FrmParameter()
        {
            InitializeComponent();
            CobxDataType.Items.Clear();
            CobxParamMode.Items.Clear();
            foreach (string s in _DataTypeNames)
                CobxDataType.Items.Add(s);
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
            _Parameter.Category = TbxCatalog.Text;
            _Parameter.Description = TbxDescription.Text;
            _Parameter.Mode = (ParamMode)_ParamModeValues[CobxParamMode.SelectedIndex];
            _Parameter.Type = (DataType)_DataTypeValues[CobxDataType.SelectedIndex];
            DialogResult = DialogResult.OK;
        }
    }
}