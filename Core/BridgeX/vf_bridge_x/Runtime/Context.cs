using System;
using TCM.API;

namespace TCM.Runtime
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class Context : IDisposable
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
        /// 释放上下文对象
        /// </summary>
        public void Dispose()
        {
            if (_Handle != IntPtr.Zero)
                Bridge.DeleteObject(_Handle);
        }

        /// <summary>
        /// 获取上下文句柄
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
        /// 设置运行状态(操作要求授信)
        /// </summary>
        public void SetState(State state, IntPtr token)
        {
            Bridge.SetState(_Handle, (int)state, token);
        }

        /// <summary>
        /// 设置返回值(操作要求授信)
        /// </summary>
        public void SetReturnCode(ReturnCode return_code, IntPtr token)
        {
            Bridge.SetReturnCode(_Handle, (int)return_code, token);
        }

        /// <summary>
        /// 设置控制码(操作要求授信)
        /// </summary>
        public void SetCtrlCode(CtrlCode ctrl_code, IntPtr token)
        {
            Bridge.SetCtrlCode(_Handle, (int)ctrl_code, token);
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
