using System.Windows.Forms;

namespace Vapula.xHost.Web
{
    class xHostAppImpl : xHostApp
    {
        protected override void Initialize()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        protected override void Shutdown()
        {
        }

        protected override void RunMessageLoop()
        {
            Application.Run();
        }

        protected override void QuitMessageLoop()
        {
            Application.Exit();
        }

        protected override IView CreateView()
        {
            FrmBrowser form = new FrmBrowser();
            form.Show();
            return form;
        }
    }
}
