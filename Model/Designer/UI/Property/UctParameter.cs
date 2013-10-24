using System;
using System.Windows.Forms;

namespace TCM.Model.Designer
{
    public partial class UctParameter : UserControl
    {
        private ParamStub _Param = null;

        public UctParameter(ParamStub stub)
        {
            InitializeComponent();
            _Param = stub;
            if (stub.Prototype.Type == DataType.Bool)
            {
                CheckBox chbx = new CheckBox();
                chbx.Text = stub.Prototype.Name;
                chbx.Dock = DockStyle.Top;
                Controls.Add(chbx);
                if (stub.HasValue)
                    chbx.Checked = (stub.Value == "true");
                if (!stub.Prototype.IsIn)
                    chbx.Enabled = false;
            }
            else
            {
                Label lbl = new Label();
                lbl.Text = stub.Prototype.Name;
                lbl.Dock = DockStyle.Top;
                TextBox tbx = new TextBox();
                tbx.Dock = DockStyle.Top;
                Controls.Add(tbx);
                Controls.Add(lbl);
                if (stub.HasValue)
                    tbx.Text = stub.Value.ToString();
                if (!stub.Prototype.IsIn)
                {
                    tbx.Enabled = false;
                    tbx.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            tooltip.ToolTipTitle = stub.Prototype.Name;
        }

        private void UctParameter_Leave(object sender, EventArgs e)
        {
            Control ctrl = Controls[0];
            switch(_Param.Prototype.Type)
            {
                case DataType.Bool:
                    _Param.Value = (ctrl as CheckBox).Checked ? "true" : "false";
                    break;
                default:
                    _Param.Value = ctrl.Text;
                    break;
            }
            if (!_Param.IsValid)
            {
                MessageBox.Show("不正确的输入。", "提示");
                _Param.Value = "";
                ctrl.Text = "";
            }
        }
    }
}
