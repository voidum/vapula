using System;
using Vapula.API;

namespace Vapula.Runtime
{
    /// <summary>
    /// 私有栈
    /// </summary>
    public class Stack
    {
        public static Stack Instance
        {
            get { return new Stack(Bridge.GetCurrentStack()); }
        }

        private IntPtr _Handle;

        /// <summary>
        /// 构造私有栈
        /// </summary>
        public Stack(IntPtr handle)
        {
            _Handle = handle;
        }

        /// <summary>
        /// 获取内核句柄
        /// </summary>
        public IntPtr Handle
        {
            get { return _Handle; }
        }

        /// <summary>
        /// 获取功能标识
        /// </summary>
        public string MethodId
        {
            get 
            { 
                return Bridge.MarshalString(
                    Bridge.GetMethodId(Handle), false); 
            }
        }

        /// <summary>
        /// 获取信封
        /// </summary>
        public Envelope Envelope
        {
            get { return new Envelope(Bridge.GetEnvelope(Handle)); }
        }

        /// <summary>
        /// 获取上下文
        /// </summary>
        public Context Context
        {
            get { return new Context(Bridge.GetContext(Handle)); }
        }
    }
}
