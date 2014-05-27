using System;

namespace Vapula.Runtime
{
    /// <summary>
    /// full-duplex data pipe
    /// </summary>
    public class Pipe : IDisposable
    {
        private IntPtr _Handle = IntPtr.Zero;
        private string _Id = null;

        public Pipe()
        {
            _Handle = Bridge.CreatePipe();
        }

        public void Dispose()
        {
            if (_Handle != IntPtr.Zero)
                Bridge.DeleteRaw(_Handle);
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
                Bridge.ToStringAnsi(
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
                Bridge.ToStringAnsi(
                Bridge.ReadPipe(_Handle));
        }
    }
}
