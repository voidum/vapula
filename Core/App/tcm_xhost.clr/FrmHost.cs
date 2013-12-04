using System.Windows.Forms;

namespace TCM.xHost.CLR
{
    public partial class FrmHost : Form
    {
        public Control Control 
        {
            get 
            {
                if(Controls.Count > 0)
                    return Controls[0];
                return null;
            }
            set 
            {
                Controls.Clear();
                Controls.Add(value);
                Controls[0].Dock = DockStyle.Fill;
            }
        }

        public FrmHost()
        {
            InitializeComponent();
        }
    }
}
