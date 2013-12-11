using System;
using TCM.API;

namespace TCM.Runtime
{
    /// <summary>
    /// 全双工信道
    /// </summary>
    public class Pipe : IDisposable
    {
        private IntPtr _Handle = IntPtr.Zero;
        private string _Id = null;

        /// <summary>
        /// 获取内核句柄
        /// </summary>
        public IntPtr Handle
        {
            get { return _Handle; }
        }

        /// <summary>
        /// 获取信道是否关闭
        /// </summary>
        public bool IsClose
        {
            get { return Bridge.PipeIsClose(_Handle); }
        }

        /// <summary>
        /// 检测信道是否收到新消息
        /// </summary>
        public bool HasNewData
        {
            get { return Bridge.PipeHasNewData(_Handle); }
        }

        /// <summary>
        /// 构造信道
        /// </summary>
        public Pipe()
        {
            _Handle = Bridge.CreatePipe();
        }

        /// <summary>
        /// 释放信道对象
        /// </summary>
        public void Dispose()
        {
            if (_Handle != IntPtr.Zero)
                Bridge.DeleteObject(_Handle);
        }

        /// <summary>
        /// 关闭信道
        /// </summary>
        public void Close()
        {
            Bridge.ClosePipe(_Handle);
        }

        /// <summary>
        /// 启动并监听信道
        /// </summary>
        public void Listen()
        {
            _Id = Bridge.MarshalString(
                Bridge.ListenPipe(_Handle), false);
        }

        /// <summary>
        /// 连接到指定信道
        /// </summary>
        public bool Connect(string pid)
        {
            return Bridge.ConnectPipe(_Handle, pid);
        }

        /// <summary>
        /// 向信道写入数据
        /// </summary>
        public void Write(string data) 
        {
            Bridge.WritePipe(_Handle, data);
        }

        /// <summary>
        /// 从信道读出数据
        /// </summary>
        public string Read() 
        {
            return Bridge.MarshalString(
                Bridge.ReadPipe(_Handle));
        }
    }
}
