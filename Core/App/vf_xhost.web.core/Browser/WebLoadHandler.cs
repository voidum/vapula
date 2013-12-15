using Xilium.CefGlue;

namespace Vapula.xHost.Web
{
    internal sealed class WebLoadHandler : CefLoadHandler
    {
        private readonly WebBrowser _Core;

        public WebLoadHandler(WebBrowser core)
        {
            _Core = core;
        }

        protected override void OnLoadingStateChange(CefBrowser browser, bool isLoading, bool canGoBack, bool canGoForward)
        {
            _Core.OnLoadingStateChanged(isLoading, canGoBack, canGoForward);
        }
    }
}
