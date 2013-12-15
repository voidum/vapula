using System;

namespace Vapula.xHost.Web
{
    public sealed class AddressChangedEventArgs : EventArgs
    {
        private readonly string _Address;

        public AddressChangedEventArgs(string address)
        {
            _Address = address;
        }

        public string Address
        {
            get { return _Address; }
        }
    }
}
