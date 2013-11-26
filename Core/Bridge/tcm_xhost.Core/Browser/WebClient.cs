using System;
using Xilium.CefGlue;
using System.Windows.Forms;

namespace TCM.xHost
{
    internal sealed class WebClient : CefClient
    {
        private readonly WebBrowser _Core;
        private readonly WebLifeSpanHandler _LifeSpanHandler;
        private readonly WebDisplayHandler _DisplayHandler;
        private readonly WebLoadHandler _LoadHandler;

        public WebClient(WebBrowser core)
        {
            _Core = core;
            _LifeSpanHandler = new WebLifeSpanHandler(_Core);
            _DisplayHandler = new WebDisplayHandler(_Core);
            _LoadHandler = new WebLoadHandler(_Core);
        }

        protected override CefLifeSpanHandler GetLifeSpanHandler()
        {
            return _LifeSpanHandler;
        }

        protected override CefDisplayHandler GetDisplayHandler()
        {
            return _DisplayHandler;
        }

        protected override CefLoadHandler GetLoadHandler()
        {
            return _LoadHandler;
        }

        protected override bool OnProcessMessageReceived(
            CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        {
            string msg = string.Format(
                "Client::OnProcessMessageReceived: SourceProcess={0}", 
                sourceProcess);
            MessageBox.Show(msg);
            msg = string.Format(
                "Message Name={0} IsValid={1} IsReadOnly={2}", 
                message.Name, message.IsValid, message.IsReadOnly);
            MessageBox.Show(msg);
            var arguments = message.Arguments;
            for (var i = 0; i < arguments.Count; i++)
            {
                var type = arguments.GetValueType(i);
                object value;
                switch (type)
                {
                    case CefValueType.Null: value = null; break;
                    case CefValueType.String: value = arguments.GetString(i); break;
                    case CefValueType.Int: value = arguments.GetInt(i); break;
                    case CefValueType.Double: value = arguments.GetDouble(i); break;
                    case CefValueType.Bool: value = arguments.GetBool(i); break;
                    default: value = null; break;
                }

                Console.WriteLine("  [{0}] ({1}) = {2}", i, type, value);
            }

            if (message.Name == "myMessage2" || 
                message.Name == "myMessage3")
                return true;
            return false;
        }
    }
}