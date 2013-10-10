using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using PIE.Models;

namespace PIE.Controls
{
    public partial class UCDataVar : UserControl , IUserCtrlValid
    {
        public List<DataVar> DataVars
        {
            get
            {
                List<DataVar> lst = new List<DataVar>();
                foreach (ListViewItem lvi in LsvDataVar.Items)
                    lst.Add(lvi.Tag as DataVar);
                return lst;
            }
        }

        public XElement Xml
        {
            get
            {
                XElement xe = new XElement("datavars");
                foreach (ListViewItem lvi in LsvDataVar.Items) 
                {
                    DataVar dv = lvi.Tag as DataVar;
                    xe.Add(dv.ToXml());
                }
                return xe;
            }
        }

        public bool IsValid(out string msg)
        {
            if (LsvDataVar.Items.Count > 0)
            {
                foreach (ListViewItem lvi in LsvDataVar.Items)
                {
                    if (string.IsNullOrWhiteSpace(lvi.SubItems[2].Text))
                    {
                        msg = "请为变量配置数据映射。";
                        return false;
                    }
                }
                if (LsvDataVar.Items.Count > 1)
                {
                    DataVar dv1 = LsvDataVar.Items[0].Tag as DataVar;
                    foreach (ListViewItem lvi in LsvDataVar.Items)
                    {
                        DataVar dv2 = lvi.Tag as DataVar;
                        if (dv1.SpectralSubset.Count != dv2.SpectralSubset.Count) //要求波段数一致
                        {
                            msg = "映射的数据，波段数不一致。";
                            return false;
                        }
                        if (dv1.SpatWidth != dv2.SpatWidth || dv1.SpatHeight != dv2.SpatHeight)
                        {

                        }
                    }
                }
            }
            msg = null;
            return true;
        }

        private void FormLayout_UpdateModel(ListViewItem lvi)
        {
            DataVar var = lvi.Tag as DataVar;
            lvi.SubItems[1].Text = var.File;
            lvi.SubItems[2].Text =
                string.Format("按波段读={0};波段子集=[{1}];空间子集=[{2}]",
                var.AsBand,
                string.Join(",", var.SpectralSubset),
                string.Join(",", var.SpatialSubset));

        }

        public UCDataVar()
        {
            InitializeComponent();
        }

        private void BtAddVar_Click(object sender, EventArgs e)
        {
            DlgInputText dlgvn = new DlgInputText("请输入变量名称：");
            if (dlgvn.ShowDialog() != DialogResult.OK) return;
            Regex regex = new Regex("[^a-zA-Z0-9]");
            string varname = regex.Replace(dlgvn.Result, "");
            regex = new Regex("^[0-9]+");
            varname = regex.Replace(varname, "");
            if (string.IsNullOrWhiteSpace(varname))
            {
                MessageBox.Show("变量名称不合规范，无法自动修正。", "注意");
                return;
            }
            foreach (ListViewItem lvi in LsvDataVar.Items)
            {
                if (lvi.Text == varname)
                {
                    MessageBox.Show("已有变量" + varname + "。", "注意");
                    return;
                }
            }
            DataVar dv = new DataVar();
            dv.Name = varname;
            ListViewItem tmplvi = new ListViewItem(new string[] { varname, "", "" });
            tmplvi.Tag = dv;
            LsvDataVar.Items.Add(tmplvi);
        }

        private void BtRemoveVar_Click(object sender, EventArgs e)
        {
            if (LsvDataVar.SelectedItems.Count < 1) return;
            ListViewItem lvi = LsvDataVar.SelectedItems[0];
            lvi.Tag = null;
            lvi.Remove();
        }

        private void LsvDataVar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LsvDataVar.Items.Count < 1) return;
            ListViewItem lvi = LsvDataVar.GetItemAt(e.X, e.Y);
            DataVar tmpdv = lvi.Tag as DataVar;
            DlgDataBand dlgmdv = new DlgDataBand(tmpdv);
            if (dlgmdv.ShowDialog() != DialogResult.OK) return;
            FormLayout_UpdateModel(lvi);
        }

        private void BtLinkData_Click(object sender, EventArgs e)
        {
            if (LsvDataVar.SelectedItems.Count < 1) return;
            ListViewItem lvi = LsvDataVar.SelectedItems[0];
            DataVar tmpdv = lvi.Tag as DataVar;
            DlgDataBand dlg = new DlgDataBand(tmpdv);
            if (dlg.ShowDialog() != DialogResult.OK) return;
            FormLayout_UpdateModel(lvi);
        }
    }
}
