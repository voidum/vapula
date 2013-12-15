using Xilium.CefGlue;

namespace Vapula.xHost
{
    internal sealed class xHostSchemeHandlerFactory : CefSchemeHandlerFactory
    {
        protected override CefResourceHandler Create(
            CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
        {
            return new DumpRequestResourceHandler();
        }
    }
}
