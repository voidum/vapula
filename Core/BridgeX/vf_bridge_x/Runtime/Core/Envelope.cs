using System;
using System.Runtime.InteropServices;
using Vapula.API;

namespace Vapula.Runtime
{
    /// <summary>
    /// 信封
    /// </summary>
    public class Envelope
    {
        private IntPtr _Handle = IntPtr.Zero;

        /// <summary>
        /// 获取信封句柄
        /// </summary>
        public IntPtr Handle
        {
            get { return _Handle; }
        }

        /// <summary>
        /// 构造信封对象
        /// </summary>
        public Envelope(IntPtr handle)
        {
            _Handle = handle;
        }

        /// <summary>
        /// 写入参数
        /// </summary>
        /// <param name="id">参数标识</param>
        /// <param name="value">参数值</param>
        public void Write(int id, string value)
        {
            Bridge.WriteEnvelope(_Handle, id, value);
        }

        /// <summary>
        /// 读出参数
        /// </summary>
        /// <param name="id">参数标识</param>
        public string Read(int id)
        {
            IntPtr ptr = Bridge.ReadEnvelope(_Handle, id);
            string result =
                ptr != IntPtr.Zero ? 
                Marshal.PtrToStringUni(ptr) : null;
            return result;
        }

        /// <summary>
        /// 投递参数
        /// </summary>
        /// <param name="src">目标信封</param>
        /// <param name="from">源参数标识</param>
        /// <param name="to">目标参数标识</param>
        public void Deliver(Envelope dst, int from, int to)
        {
            Bridge.DeliverEnvelope(_Handle, dst.Handle, from, to);
        }

    }
}
