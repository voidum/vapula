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
        /// 获取当前状态
        /// </summary>
        public State CurrentState
        {
            get { return (State)Bridge.GetCurrentState(_Handle); }
        }

        /// <summary>
        /// 获取上一个状态
        /// </summary>
        public State LastState
        {
            get { return (State)Bridge.GetLastState(_Handle); }
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
            set { Bridge.SetReturnCode(_Handle, (byte)value); }
        }

        /// <summary>
        /// 获取控制码
        /// </summary>
        public CtrlCode CtrlCode
        {
            get { return (CtrlCode)Bridge.GetCtrlCode(_Handle); }
        }

        /// <summary>
        /// 切换暂停/恢复
        /// </summary>
        public void SwitchHold()
        {
            Bridge.SwitchHold(_Handle);
        }

        /// <summary>
        /// 切换占用状态的后端/前端标记
        /// </summary>
        public void SwitchBusy()
        {
            Bridge.SwitchBusy(_Handle);
        }
    }
}