using System.Windows.Forms;
using TCM.xHost;
using WebBrowser = TCM.xHost.WebBrowser;

namespace TCM
{
    public partial class FrmBrowser : Form, IView
    {
        private WebBrowser _Browser = null;
        private xHostToolStrip _Navigator = null;

        public FrmBrowser()
        {
            InitializeComponent();

            _Browser = new WebBrowser();
            _Browser.Dock = DockStyle.Fill;
            _Browser.Parent = this;

            _Navigator = new xHostToolStrip();
            _Navigator.Dock = DockStyle.Top;
            _Navigator.Parent = this;

            _Navigator.Browser = _Browser;
            _Browser.BringToFront();
        }
    }
}
