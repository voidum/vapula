using System;

namespace Vapula.xHost.Web
{
    public sealed class TargetUrlChangedEventArgs : EventArgs
    {
        private readonly string _TargetUrl;

        public TargetUrlChangedEventArgs(string url)
        {
            _TargetUrl = url;
        }

        public string TargetUrl
        {
            get { return _TargetUrl; }
        }
    }
}
