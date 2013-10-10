﻿using System;
using TCM.API;

namespace TCM.Runtime
{
    /// <summary>
    /// 执行器基类
    /// </summary>
    public class Executor : IDisposable
    {
        #region 字段
        protected IntPtr _Handle;
        protected Context _Context = null;
        protected Envelope _Envelope = null;
        #endregion

        #region 构造
        public Executor(IntPtr handle)
        {
            _Handle = handle;
        }
        #endregion

        #region 属性
        public int FunctionId
        {
            get
            {
                return Bridge.GetFunctionId(_Handle);
            }
        }

        /// <summary>
        /// 获取或设置执行器的上下文
        /// </summary>
        public Context Context
        {
            get
            {
                if (_Context == null)
                {
                    IntPtr ptr = Bridge.GetContext(_Handle);
                    _Context = new Context(ptr);
                }
                return _Context;
            }
        }

        /// <summary>
        /// 获取或设置信封
        /// </summary>
        public Envelope Envelope
        {
            get
            {
                if (_Envelope == null)
                {
                    IntPtr ptr = Bridge.GetEnvelope(_Handle);
                    _Envelope = new Envelope(ptr);
                }
                return _Envelope;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 启动
        /// </summary>
        public virtual bool Start()
        {
            return Bridge.StartExecutor(_Handle);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public virtual void Stop(uint wait)
        {
            Bridge.StopExecutor(_Handle, wait);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public virtual void Pause(uint wait)
        {
            Bridge.PauseExecutor(_Handle, wait);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public virtual void Resume() 
        {
            Bridge.ResumeExecutor(_Handle);
        }

        /// <summary>
        /// 重启
        /// </summary>
        public virtual void Restart(uint wait) 
        {
            Bridge.RestartExecutor(_Handle, wait);
        }

        /// <summary>
        /// 销毁执行器
        /// </summary>
        public virtual void Dispose()
        {
            if(_Envelope != null) _Envelope.Dispose();
            if(_Context != null) _Context.Dispose();
        }
        #endregion
    }
}
