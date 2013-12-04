using System;

namespace TCM.xHost.Web
{
    public sealed class LoadingStateChangedEventArgs : EventArgs
    {
        private readonly bool _IsLoading;
        private readonly bool _CanGoBack;
        private readonly bool _CanGoForward;

        public LoadingStateChangedEventArgs(bool loading, bool back, bool forward)
        {
            _IsLoading = loading;
            _CanGoBack = back;
            _CanGoForward = forward;
        }

        public bool Loading
        {
            get { return _IsLoading; }
        }

        public bool CanGoBack
        {
            get { return _CanGoBack; }
        }

        public bool CanGoForward
        {
            get { return _CanGoForward; }
        }
    }
}
