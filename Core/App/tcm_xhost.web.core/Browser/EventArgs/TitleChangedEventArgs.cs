using System;

namespace TCM.xHost.Web
{
    public sealed class TitleChangedEventArgs : EventArgs
    {
        private readonly string _Title;

        public TitleChangedEventArgs(string title)
        {
            _Title = title;
        }

        public string Title
        {
            get { return _Title; }
        }
    }
}
