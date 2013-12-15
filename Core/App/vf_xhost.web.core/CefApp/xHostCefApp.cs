using System;
using Xilium.CefGlue;

namespace Vapula.xHost.Web
{
    internal sealed class xHostCefApp : CefApp
    {
        private CefBrowserProcessHandler _browserProcessHandler = new BrowserProcessHandler();
        private CefRenderProcessHandler _renderProcessHandler = new RenderProcessHandler();

        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
            Console.WriteLine("OnBeforeCommandLineProcessing: {0} {1}", processType, commandLine);
        }

        protected override CefBrowserProcessHandler GetBrowserProcessHandler()
        {
            return _browserProcessHandler;
        }

        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return _renderProcessHandler;
        }
    }
}
