using System;
using System.Windows.Forms;
using TCM.Model;

namespace TCM.Toolkit
{
    public partial class FrmDetailLib : Form
    {
        private string[] _RuntimeIds
            = new string[] { 
                "crt", "clr", "jre",
                "py", "js", "mat",
                "idl", "ruby" };

        public int RuntimeId
        {
            get
            {
                for (int i = 0; i < _RuntimeIds.Length; i++)
                    if (_Library.Runtime == _RuntimeIds[i])
                        return i;
                return -1;
            }
        }

        private Library _Library = null;
        public Library Library
        {
            get { return _Library; }
            set
            {
                _Library = value;
                TbxId.Text = value.Id;
                TbxName.Text = value.Name;
                TbxVersion.Text = value.Version;
                TbxPublisher.Text = value.Publisher;
                TbxDescription.Text = value.Description;
                if (RuntimeId >= 0)
                    CobxRuntime.SelectedIndex = RuntimeId;
                else
                    CobxRuntime.SelectedIndex = 0;
            }
        }

        public FrmDetailLib()
        {
            InitializeComponent();
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            _Library.Name = TbxName.Text;
            _Library.Version = TbxVersion.Text;
            _Library.Publisher = TbxPublisher.Text;
            _Library.Description = TbxDescription.Text;
            _Library.Runtime = _RuntimeIds[CobxRuntime.SelectedIndex];
            DialogResult = DialogResult.OK;
        }
    }
}
