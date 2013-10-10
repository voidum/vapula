using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PIE.Controls
{
    public partial class DlgSpectralSubset : Form
    {
        private int _BandTotal;
        private int _MaxNeed;
        private int _MinNeed;

        public List<int> Subset
        {
            get
            {
                List<int> tmp = new List<int>();
                for (int i = 0; i < LsbBands.Items.Count; i++)
                    if (LsbBands.GetItemChecked(i))
                        tmp.Add(i + 1);
                return tmp;
            }
            set
            {
                foreach (int i in value)
                    LsbBands.SetItemChecked(i - 1, true);
            }
        }

        public DlgSpectralSubset(int bandtotal, int maxneed = 1, int minneed = 1)
        {
            InitializeComponent();
            if (bandtotal < 1) throw new Exception("波段必须多于零个。");
            if (maxneed > bandtotal ||
                maxneed < minneed ||
                minneed < 1) throw new Exception("波段数上下限不正确。");
            _BandTotal = bandtotal;
            _MaxNeed = maxneed;
            _MinNeed = minneed;
            LsbBands.Items.Clear();
            for (int i = 0; i < bandtotal; i++)
                LsbBands.Items.Add("波段" + (i + 1).ToString());
            FormLayout_UpdateStat(bandtotal, 0);
            TbxMax.Text = bandtotal.ToString();
        }

        private void FormLayout_UpdateStat()
        { FormLayout_UpdateStat(_BandTotal, LsbBands.CheckedItems.Count); }

        private void FormLayout_UpdateStat(int total,int count)
        {
            LblStat.Text =
                string.Format("共有{0}个波段，已选中{1}个波段。", total, count);
        }

        private void ChbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < LsbBands.Items.Count; i++)
                LsbBands.SetItemChecked(i, ChbxSelectAll.Checked);
            FormLayout_UpdateStat();
        }

        private void LsbBands_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormLayout_UpdateStat();
        }

        private void BtAddRange_Click(object sender, EventArgs e)
        {
            int min, max;
            if (!int.TryParse(TbxMin.Text, out min)) return;
            if (!int.TryParse(TbxMax.Text, out max)) return;
            if (min > max) return;
            if (min < 1) return;
            for (int i = min - 1; i < max; i++)
                LsbBands.SetItemChecked(i, true);
            FormLayout_UpdateStat();
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            if (LsbBands.CheckedItems.Count < _MinNeed ||
                LsbBands.CheckedItems.Count > _MaxNeed)
            {
                MessageBox.Show("已选波段的数量不符合计算要求。");
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
