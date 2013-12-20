using System;
using System.Windows.Forms;
using Vapula.Model;

namespace Vapula.Toolkit
{
    public partial class FrmLibrary : Form
    {
        private string[] _RuntimeIds
            = new string[] { 
                "crt", "clr", "jre",
                "py", "js", "mat",
                "idl", "ruby" };

        private string[] _RuntimeNames
            = new string[] {
                "C/C++", ".NET", "Java",
                "Python", "JavaScript", "Matlab",
                "IDL", "Ruby" };

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
                TbxEntry.Text = value.Entry;
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

        public FrmLibrary()
        {
            InitializeComponent();
            CobxRuntime.Items.Clear();
            foreach (string s in _RuntimeNames)
                CobxRuntime.Items.Add(s);
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            _Library.Entry = TbxEntry.Text;
            _Library.Name = TbxName.Text;
            _Library.Version = TbxVersion.Text;
            _Library.Publisher = TbxPublisher.Text;
            _Library.Description = TbxDescription.Text;
            _Library.Runtime = _RuntimeIds[CobxRuntime.SelectedIndex];
            DialogResult = DialogResult.OK;
        }
    }
}
