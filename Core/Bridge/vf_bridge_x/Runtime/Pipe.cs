using Sartrey;
using System;

namespace Vapula.Runtime
{
    /// <summary>
    /// full-duplex data pipe
    /// </summary>
    public class Pipe : DisposableObject
    {
        private IntPtr _Handle 
            = IntPtr.Zero;
        private string _Id 
            = null;

        public Pipe()
        {
            _Handle = Bridge.CreatePipe();
        }

        /// <summary>
        /// get handle
        /// </summary>
        public IntPtr Handle
        {
            get { return _Handle; }
        }

        /// <summary>
        /// get pipe id
        /// </summary>
        public string Id
        {
            get { return _Id; }
        }

        /// <summary>
        /// get if pipe is closed
        /// </summary>
        public bool IsClose
        {
            get { return Bridge.PipeIsClose(_Handle); }
        }

        /// <summary>
        /// get if pipe has new data
        /// </summary>
        public bool HasNewData
        {
            get { return Bridge.PipeHasNewData(_Handle); }
        }

        /// <summary>
        /// close pipe
        /// </summary>
        public void Close()
        {
            Bridge.ClosePipe(_Handle);
        }

        /// <summary>
        /// listen pipe
        /// </summary>
        public void Listen()
        {
            _Id = 
                Base.ToStringAnsi(
                Bridge.ListenPipe(_Handle));
        }

        /// <summary>
        /// connect to pipe
        /// </summary>
        public bool Connect(string id)
        {
            return Bridge.ConnectPipe(_Handle, id);
        }

        /// <summary>
        /// write data to pipe
        /// </summary>
        public void Write(string data) 
        {
            Bridge.WritePipe(_Handle, data);
        }

        /// <summary>
        /// read data from pipe
        /// </summary>
        public string Read() 
        {
            return 
                Base.ToStringAnsi(
                Bridge.ReadPipe(_Handle));
        }

        protected override void DisposeManaged()
        {
        }

        protected override void DisposeNative()
        {
            if (_Handle != IntPtr.Zero)
            {
                Close();
                Bridge.DeleteData(_Handle);
                _Handle = IntPtr.Zero;
            }
        }
    }
}
