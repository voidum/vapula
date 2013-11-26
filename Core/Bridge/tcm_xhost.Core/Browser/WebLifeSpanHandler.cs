using Xilium.CefGlue;

namespace TCM.xHost
{
    internal sealed class WebLifeSpanHandler : CefLifeSpanHandler
    {
        private readonly WebBrowser _Core;

        public WebLifeSpanHandler(WebBrowser core)
        {
            _Core = core;
        }

        protected override void OnAfterCreated(CefBrowser browser)
        {
            base.OnAfterCreated(browser);
            _Core.OnCreated(browser);
        }

        protected override void OnBeforeClose(CefBrowser browser)
        {
            base.OnBeforeClose(browser);
        }

        protected override bool OnBeforePopup(CefBrowser browser, CefFrame frame, string targetUrl, string targetFrameName, CefPopupFeatures popupFeatures, CefWindowInfo windowInfo, ref CefClient client, CefBrowserSettings settings, ref bool noJavascriptAccess)
        {
            return base.OnBeforePopup(browser, frame, targetUrl, targetFrameName, popupFeatures, windowInfo, ref client, settings, ref noJavascriptAccess);
        }

        protected override bool DoClose(CefBrowser browser)
        {
            return false;
        }
    }
}
