using System;
using Vapula.API;

namespace Vapula.Runtime
{
    /// <summary>
    /// 调用器基类
    /// </summary>
    public class Invoker : IDisposable
    {
        #region 字段
        protected IntPtr _Handle;
        protected Stack _Stack = null;
        #endregion

        #region 构造
        public Invoker(IntPtr handle)
        {
            _Handle = handle;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置当前私有栈
        /// </summary>
        public Stack Stack
        {
            get
            {
                if (_Stack == null)
                {
                    IntPtr ptr = Bridge.GetStack(_Handle);
                    _Stack = new Stack(ptr);
                }
                return _Stack;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 启动
        /// </summary>
        public virtual bool Start()
        {
            return Bridge.StartInvoker(_Handle);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public virtual void Stop(uint wait)
        {
            Bridge.StopInvoker(_Handle, wait);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public virtual void Pause(uint wait)
        {
            Bridge.PauseInvoker(_Handle, wait);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public virtual void Resume() 
        {
            Bridge.ResumeInvoker(_Handle);
        }

        /// <summary>
        /// 重启
        /// </summary>
        public virtual void Restart(uint wait) 
        {
            Bridge.RestartInvoker(_Handle, wait);
        }

        /// <summary>
        /// 销毁调用器
        /// </summary>
        public virtual void Dispose()
        {
            if(_Handle != IntPtr.Zero)
                Bridge.DeleteObject(_Handle);
        }
        #endregion
    }
}
