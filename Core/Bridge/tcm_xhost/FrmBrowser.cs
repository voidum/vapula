using System;
using System.Windows.Forms;
using TCM.xHost;

namespace TCM
{
    public partial class FrmBrowser : Form, IView
    {
        public FrmBrowser()
        {
            InitializeComponent();
            toolbar_SizeChanged(this, null);
        
            var browserCtl = new WebBrowserCtrl();
            browserCtl.Parent = this;
            browserCtl.Dock = DockStyle.Fill;
            browserCtl.BringToFront();

            var browser = browserCtl.WebBrowser;
            browser.StartUrl = "www.baidu.com";
            Visible = true;
        }

        private void toolbar_SizeChanged(object sender, EventArgs e)
        {
            TbxURL.Width = toolbar.Width 
                - BtBack.Width 
                - BtForward.Width 
                - BtOption.Width 
                - BtRefresh.Width - 4;
        }
    }
}
