using System;
using System.Windows.Forms;

namespace PIE.Controls
{
    public partial class DlgSpatialSubset : Form
    {
        private int _MaxXSize;
        private int _MaxYSize;

        public int[] Subset
        {
            get 
            {
                int[] tmp = new int[] 
                {
                    int.Parse(TbxSL.Text),
                    int.Parse(TbxSR.Text),
                    int.Parse(TbxLT.Text),
                    int.Parse(TbxLB.Text)
                };
                return tmp;
            }
            set
            {
                TbxSL.Text = value[0].ToString();
                TbxSR.Text = value[1].ToString();
                TbxLT.Text = value[2].ToString();
                TbxLB.Text = value[3].ToString();
            }
        }

        public DlgSpatialSubset(int max_x,int max_y)
        {
            InitializeComponent();
            if (max_x < 1 || max_y < 1) throw new Exception("最大空间范围不合理。");
            _MaxXSize = max_x;
            _MaxYSize = max_y;
            TbxSR.Text = TbxNS.Text = max_x.ToString();
            TbxLB.Text = TbxNL.Text = max_y.ToString();
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            int SL, SR, LT, LB;
            if (!int.TryParse(TbxSL.Text, out SL) ||
                !int.TryParse(TbxSR.Text, out SR) ||
                !int.TryParse(TbxLT.Text, out LT) ||
                !int.TryParse(TbxLB.Text, out LB))
            {
                MessageBox.Show("请输入有效的空间子集边界数值。");
                return;
            }
            if (SL < 1 || SR > _MaxXSize ||
                LT < 1 || LB > _MaxYSize ||
                SL > SR || LT > LB)
            {
                MessageBox.Show("请输入有效的空间子集边界数值。");
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ChbxLockNS_CheckedChanged(object sender, EventArgs e)
        {
            if (ChbxLockNS.Checked)
            {
                TbxNS.Enabled = false;
                TbxNS.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                TbxNS.Enabled = true;
                TbxNS.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void ChbxLockNL_CheckedChanged(object sender, EventArgs e)
        {
            if (ChbxLockNL.Checked)
            {
                TbxNL.Enabled = false;
                TbxNL.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                TbxNL.Enabled = true;
                TbxNL.BorderStyle = BorderStyle.Fixed3D;
            }
        }
    }
}
