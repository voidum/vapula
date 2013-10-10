using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PIE.Controls
{
    /// <summary>
    /// TODO:增加输出到内存的选项
    /// </summary>
    public partial class UCOutput : UserControl , IUserCtrlValid
    {
        public string OutDir
        {
            get { return TbxDirectory.Text; }
        }

        public string FileName
        {
            get { return TbxFile.Text; }
        }

        public int FileType
        {
            get { return CobxFormat.SelectedIndex; }
        }

        public XElement Xml
        {
            get
            {
                XElement xe = new XElement("output",
                    new XElement("file",
                        new XCData(TbxDirectory.Text)),
                    new XElement("format", CobxFormat.SelectedIndex));
                return xe;
            }
        }

        public UCOutput()
        {
            InitializeComponent();
            CobxFormat.SelectedIndex = 0;
        }

        private void UCConfigOutput_Resize(object sender, EventArgs e)
        {
            TbxDirectory.Width = Width - 160;
            BtBrowse.Left = TbxDirectory.Right + 6;
            TbxFile.Width = Width - 200;
            CobxFormat.Left = TbxFile.Right + 5;
        }

        private void BtBrowse_Click(object sender, EventArgs e)
        {
            if (dlgfolder.ShowDialog() == DialogResult.OK)
            {
                TbxDirectory.Text = dlgfolder.SelectedPath;
            }
        }

        public bool IsValid(out string msg)
        {
            if (CobxFormat.SelectedIndex < 0)
            {
                msg = "没有选择输出格式。";
                return false;
            }
            if (string.IsNullOrWhiteSpace(TbxDirectory.Text))
            {
                msg = "没有选择输出目录。";
                return false;
            }
            if (string.IsNullOrWhiteSpace(TbxFile.Text))
            {
                msg = "没有设置输出文件名。";
                return false;
            }
            //invalid filename char
            char[] chararr = new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
            foreach (char c in chararr)
            {
                if (TbxFile.Text.Contains(c))
                {
                    msg = "输出文件名存在非法字符。";
                    return false;
                }
            }
            msg = null;
            return true;
        }
    }
}
