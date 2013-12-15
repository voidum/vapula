using System;
using Vapula.API;

namespace Vapula.Runtime
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class Context
    {
        private IntPtr _Handle = IntPtr.Zero;

        /// <summary>
        /// 构造上下文
        /// </summary>
        public Context(IntPtr handle)
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
        /// 获取运行状态
        /// </summary>
        public State State
        {
            get { return (State)Bridge.GetState(_Handle); }
        }

        /// <summary>
        /// 获取或设置进度值
        /// </summary>
        public float Progress 
        {
            get { return Bridge.GetProgress(_Handle); }
            set { Bridge.SetProgress(_Handle, value); }
        }

        /// <summary>
        /// 获取返回值
        /// </summary>
        public ReturnCode ReturnCode
        {
            get { return (ReturnCode)Bridge.GetReturnCode(_Handle); }
        }

        /// <summary>
        /// 获取控制码
        /// </summary>
        public CtrlCode CtrlCode
        {
            get { return (CtrlCode)Bridge.GetCtrlCode(_Handle); }
        }

        /// <summary>
        /// 应答控制码
        /// </summary>
        public void ReplyCtrlCode()
        {
            Bridge.ReplyCtrlCode(_Handle);
        }
    }
}