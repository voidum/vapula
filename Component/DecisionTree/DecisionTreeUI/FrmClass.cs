using System;
using System.Windows.Forms;
using DecisionTreeUI.Models;
using System.Drawing;

namespace DecisionTreeUI
{
    public partial class FrmClass : Form
    {
        private void InitLang() 
        {
            Text = AppData.LangPack["FrmClass"];
            Lbl1.Text = AppData.LangPack["Index"];
            Lbl2.Text = AppData.LangPack["Name"];
            Lbl3.Text = AppData.LangPack["Color"];
            BtOK.Text = AppData.LangPack["BtOK"];
            BtCancel.Text = AppData.LangPack["BtCancel"];
        }

        private NodeClass _Model;
        public NodeClass Model
        {
            get { return _Model; }
            set 
            {
                if (value == null) return;
                _Model = value;
            }
        }

        public Color ClassColor
        {
            get { return _Model.ClassColor; }
        }

        public string ClassName
        {
            get { return _Model.Name; }
        }

        public FrmClass()
        {
            InitializeComponent();
            InitLang();
        }

        private void LblColor_Click(object sender, EventArgs e)
        {
            dlgcolor.Color = LblColor.BackColor;
            if (dlgcolor.ShowDialog() == DialogResult.OK)
            {
                LblColor.BackColor = dlgcolor.Color;
            }
        }

        private void FrmClass_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FrmClass_Load(object sender, EventArgs e)
        {
            LblId.Text = _Model.Id.ToString();
            TbxName.Text = _Model.Name;
            LblColor.BackColor = _Model.ClassColor;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbxName.Text)) 
                _Model.Name = TbxName.Text;
            _Model.ClassColor = LblColor.BackColor;
            DialogResult = DialogResult.OK;
        }
    }
}
