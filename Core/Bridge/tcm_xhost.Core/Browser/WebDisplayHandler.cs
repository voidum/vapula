using Xilium.CefGlue;

namespace TCM.xHost
{
    internal sealed class WebDisplayHandler : CefDisplayHandler
    {
        private readonly WebBrowser _Core;

        public WebDisplayHandler(WebBrowser core)
        {
            _Core = core;
        }

        protected override void OnTitleChange(CefBrowser browser, string title)
        {
            _Core.OnTitleChanged(title);
        }

        protected override void OnAddressChange(CefBrowser browser, CefFrame frame, string url)
        {
            if (frame.IsMain)
            {
                _Core.OnAddressChanged(url);
            }
        }

        protected override void OnStatusMessage(CefBrowser browser, string value)
        {
            _Core.OnTargetUrlChanged(value);
        }

        protected override bool OnTooltip(CefBrowser browser, string text)
        {
            //_Core.OnTooltip(text);
        	return false;
        }
    }
}
