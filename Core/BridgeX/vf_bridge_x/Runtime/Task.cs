using System;

namespace Vapula.Runtime
{
    /// <summary>
    /// task
    /// </summary>
    public class Task : IDisposable
    {
        #region Fields
        protected IntPtr _Handle;
        protected Stack _Stack = null;
        protected object _SyncRoot = new object();
        #endregion

        #region Ctor
        public Task(IntPtr handle)
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
                        if(_Stack == null)
                        {
                            IntPtr ptr = Bridge.GetTaskStack(_Handle);
                            _Stack = new Stack(ptr);
                        }
                    }
                }
                return _Stack;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// start task
        /// </summary>
        public void Start()
        {
            Bridge.StartTask(_Handle);
        }

        /// <summary>
        /// stop task
        /// </summary>
        public void Stop(uint wait)
        {
            Bridge.StopTask(_Handle, wait);
        }

        /// <summary>
        /// pause task
        /// </summary>
        public void Pause(uint wait)
        {
            Bridge.PauseTask(_Handle, wait);
        }

        /// <summary>
        /// resume task
        /// </summary>
        public void Resume() 
        {
            Bridge.ResumeTask(_Handle);
        }

        /// <summary>
        /// dispose invoker
        /// </summary>
        public void Dispose()
        {
            Stack.Dispose();
            _Stack = null;
            _SyncRoot = null;
            _Handle = IntPtr.Zero;
            Bridge.DeleteRaw(_Handle);
        }
        #endregion
    }
}
