using System.Windows.Forms;
using DecisionTreeUI.Models;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DecisionTreeUI
{
    public partial class FrmJudge : Form
    {
        private void InitLang()
        {
            Text = AppData.LangPack["FrmJudge"];
            Lbl1.Text = AppData.LangPack["Name"];
            Lbl2.Text = AppData.LangPack["Expression"];
            BtOK.Text = AppData.LangPack["BtOK"];
            BtCancel.Text = AppData.LangPack["BtCancel"];
        }

        private bool CheckVar(string var)
        {
            Regex regex = new Regex("[a-zA-Z0-9]+");
            MatchCollection mc = regex.Matches(var);
            if (mc.Count == 0) return false;
            List<string> errvars = new List<string>();
            foreach (Match m in mc)
            {
                double tmp;
                if (double.TryParse(m.Value, out tmp)) continue;
                bool ret = false;
                foreach (Mapping map in AppData.Mappings)
                {
                    if (map.Name == m.Value)
                    {
                        ret = true;
                        break;
                    }
                }
                if (!ret)
                {
                    errvars.Add(m.Value);
                    //return false;
                }
            }
            string msg = AppData.LangPack["FrmJudge_CheckVar_1"];
            foreach (string str in errvars)
                msg += str + ",";
            msg = msg.Substring(0,msg.Length-1);
            if(errvars.Count > 0) MessageBox.Show(msg);
            return true;
        }

        private NodeJudge _Model;
        public NodeJudge Model
        {
            get { return _Model; }
            set
            {
                if (value == null) return;
                _Model = value;
                TbxName.Text = _Model.Name;
                TbxCode.Text = _Model.Condition;
            }
        }

        public string JudgeName
        {
            get { return _Model.Name; }
        }

        public string Condition
        {
            get { return _Model.Condition; }
        }

        public FrmJudge()
        {
            InitializeComponent();
            InitLang();
        }

        private void FrmJudge_Load(object sender, System.EventArgs e)
        {
            TbxName.Text = _Model.Name;
            TbxCode.Text = Condition;
        }

        private void BtOK_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbxName.Text))
                _Model.Name = TbxName.Text;
            if (!CheckVar(TbxCode.Text))
            {
                //MessageBox.Show(AppData.LangPack["FrmJudge_BtOK_1"]);
                return;
            }
            _Model.Condition = TbxCode.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
