using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace TCM.xHost
{
    internal sealed class DemoAppSchemeHandlerFactory : CefSchemeHandlerFactory
    {
        protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
        {
            return new DumpRequestResourceHandler();
        }
    }
}
