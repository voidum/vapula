using System.Windows.Forms;

namespace TCM.xHost
{
    class xHostAppImpl : xHostApp
    {
        protected override void PlatformInitialize()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        protected override void PlatformShutdown()
        {
        }

        protected override void PlatformRunMessageLoop()
        {
            Application.Run();
        }

        protected override void PlatformQuitMessageLoop()
        {
            Application.Exit();
        }

        protected override IView CreateView()
        {
            FrmBrowser form = new FrmBrowser();
            return form;
        }
    }
}
