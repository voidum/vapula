using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace TCM.xHost
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisibleAttribute(true)]
    public partial class FrmBrowser : Form
    {
        public FrmBrowser()
        {
            InitializeComponent();
            if (browser.Version.Major < 9)
            {
                MessageBox.Show("IE version is too low", "TCM xHost");
                Application.Exit();
            }
        }

        private void FrmBrowser_Load(object sender, EventArgs e)
        {
            browser.Navigate("www.baidu.com");
        }
    }
}
