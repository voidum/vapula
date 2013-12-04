using System.Windows.Forms;

namespace TCM.xHost.Web
{
    public partial class FrmBrowser : Form, IView
    {
        private WebBrowser _Browser = null;
        private NavigatorBar _Navigator = null;

        public FrmBrowser()
        {
            InitializeComponent();

            _Browser = new WebBrowser();
            _Browser.Dock = DockStyle.Fill;
            _Browser.Parent = this;

            _Navigator = new NavigatorBar();
            _Navigator.Dock = DockStyle.Top;
            _Navigator.Parent = this;

            _Navigator.Browser = _Browser;
            _Browser.BringToFront();
        }
    }
}
