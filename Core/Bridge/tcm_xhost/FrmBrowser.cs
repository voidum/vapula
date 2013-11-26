using System;
using System.Windows.Forms;
using TCM.xHost;
using WebBrowser = TCM.xHost.WebBrowser;

namespace TCM
{
    public partial class FrmBrowser : Form, IView
    {
        private WebBrowser _Browser = null;

        public FrmBrowser()
        {
            InitializeComponent();
            toolbar_SizeChanged(this, null);

            _Browser = new WebBrowser();
            _Browser.Parent = this;
            _Browser.Dock = DockStyle.Fill;
            _Browser.BringToFront();
        }

        private void toolbar_SizeChanged(object sender, EventArgs e)
        {
            TbxURL.Width = toolbar.Width 
                - BtBack.Width 
                - BtForward.Width 
                - BtOption.Width 
                - BtRefresh.Width - 4;
        }

        private void TbxURL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                _Browser.CefBrowser.GetMainFrame().LoadUrl(TbxURL.Text);
                e.Handled = true;
            }
        }
    }
}
