using System;

namespace TCM.xHost.Web
{
    public sealed class StatusMessageEventArgs : EventArgs
    {
        private readonly string _Value;

        public StatusMessageEventArgs(string value)
        {
            _Value = value;
        }

        public string Value 
        {
            get { return _Value; } 
        }
    }
}
