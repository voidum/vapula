using System;
using Vapula.API;

namespace Vapula.Runtime
{
    /// <summary>
    /// invoker
    /// </summary>
    public class Invoker : IDisposable
    {
        #region Fields
        protected IntPtr _Handle;
        protected Stack _Stack = null;
        protected readonly object _SyncRoot = new object();
        #endregion

        #region Ctor
        public Invoker(IntPtr handle)
        {
            _Handle = handle;
        }
        #endregion

        #region Properties
        /// <summary>
        /// get stack for current invoker
        /// </summary>
        public Stack Stack
        {
            get
            {
                if (_Stack == null)
                {
                    lock (_SyncRoot) 
                    {
                        IntPtr ptr = Bridge.GetStack(_Handle);
                        _Stack = new Stack(ptr);
                    }
                }
                return _Stack;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// start invoker
        /// </summary>
        public virtual bool Start()
        {
            return Bridge.StartInvoker(_Handle);
        }

        /// <summary>
        /// stop invoker
        /// </summary>
        public virtual void Stop(uint wait)
        {
            Bridge.StopInvoker(_Handle, wait);
        }

        /// <summary>
        /// pause invoker
        /// </summary>
        public virtual void Pause(uint wait)
        {
            Bridge.PauseInvoker(_Handle, wait);
        }

        /// <summary>
        /// resume invoker
        /// </summary>
        public virtual void Resume() 
        {
            Bridge.ResumeInvoker(_Handle);
        }

        /// <summary>
        /// restart invoker
        /// </summary>
        public virtual void Restart(uint wait) 
        {
            Bridge.RestartInvoker(_Handle, wait);
        }

        /// <summary>
        /// dispose invoker
        /// </summary>
        public virtual void Dispose()
        {
            Stack.Clear();
            _Stack = null;
            _Handle = IntPtr.Zero;
            Bridge.DeleteRaw(_Handle);
        }
        #endregion
    }
}
