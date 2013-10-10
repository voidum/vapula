using System;
using System.Windows.Forms;
using DecisionTreeUI.Models;

namespace DecisionTreeUI
{
    public partial class FrmCfgData : Form
    {
        private void InitLang()
        {
            Text = AppData.LangPack["FrmCfgData"];
            ColhVar.Text = AppData.LangPack["Var"];
            ColhSource.Text = AppData.LangPack["Source"];
            ColhBand.Text = AppData.LangPack["Band"];
            GrpOut.Text = AppData.LangPack["Output"];
            Lbl1.Text = AppData.LangPack["Input"];
            Lbl2.Text = AppData.LangPack["Directory"];
            Lbl3.Text = AppData.LangPack["File"];
            BtBrowse.Text = AppData.LangPack["BtBrowse"];
            BtOK.Text = AppData.LangPack["BtOK"];
            BtCancel.Text = AppData.LangPack["BtCancel"];
        }

        public FrmCfgData()
        {
            InitializeComponent();
            InitLang();
        }

        private void FrmCfgData_Load(object sender, EventArgs e)
        {
            LsvInput.Items.Clear();
            foreach (Mapping map in AppData.Mappings)
            {
                ListViewItem lvi = new ListViewItem(
                    new string[]
                    {
                        map.Name,
                        map.File,
                        map.BandIndex.ToString()
                    });
                
                lvi.SubItems[0].Name = "var";
                lvi.SubItems[1].Name = "data";
                lvi.SubItems[2].Name = "band";

                LsvInput.Items.Add(lvi);
            }
            TbxOutDir.Text = AppData.OutDir;
            TbxOutName.Text = AppData.OutName;
            CobxOutFmt.SelectedIndex = AppData.OutFmt;
        }

        private void BtBrowse_Click(object sender, EventArgs e)
        {
            if (dlgfolder.ShowDialog() != DialogResult.OK) return;
            TbxOutDir.Text = dlgfolder.SelectedPath;
        }

        private void LsvInput_MouseUp(object sender, MouseEventArgs e)
        {
            if (LsvInput.SelectedItems.Count < 1) return;
            ListViewItem lvi = LsvInput.SelectedItems[0];
            ListViewItem.ListViewSubItem lvsi = lvi.GetSubItemAt(e.X, e.Y);
            switch (lvsi.Name)
            {
                case "var": return;
                case "data":
                    if (dlgopen.ShowDialog() != DialogResult.OK) return;
                    lvsi.Text = dlgopen.FileName;
                    return;
                case "band":
                    PIE.Controls.DlgInputText dlgbandid = 
                        new PIE.Controls.DlgInputText(
                            AppData.LangPack["FrmCfgData_LsvInput_1"],
                            AppData.LangPack["Band"]);
                    if (dlgbandid.ShowDialog() != DialogResult.OK) return;
                    if (string.IsNullOrWhiteSpace(dlgbandid.Result)) return;
                    int tmpbandid;
                    if (!int.TryParse(dlgbandid.Result, out tmpbandid))
                    {
                        MessageBox.Show(
                            AppData.LangPack["FrmCfgData_LsvInput_2"],
                            AppData.LangPack["Caution"]);
                        return;
                    }
                    lvsi.Text = dlgbandid.Result;
                    return;
            }
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            AppData.OutName = TbxOutName.Text;
            AppData.OutDir = TbxOutDir.Text;
            AppData.OutFmt = CobxOutFmt.SelectedIndex;
            foreach (Mapping map in AppData.Mappings)
            {
                foreach (ListViewItem lvi in LsvInput.Items)
                {
                    if (map.Name == lvi.SubItems[0].Text)
                    {
                        map.File = lvi.SubItems[1].Text;
                        map.BandIndex = int.Parse(lvi.SubItems[2].Text);
                    }
                }
            }
            DialogResult = DialogResult.OK;
        }
    }
}
