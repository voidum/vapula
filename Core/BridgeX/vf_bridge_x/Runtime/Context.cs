using System;

namespace Vapula.Runtime
{
    /// <summary>
    /// context
    /// </summary>
    public class Context
    {
        private IntPtr _Handle = IntPtr.Zero;

        public Context(IntPtr handle)
        {
            _Handle = handle;
        }

        /// <summary>
        /// get handle
        /// </summary>
        public IntPtr Handle
        {
            get { return _Handle; }
        }

        /// <summary>
        /// get current state
        /// </summary>
        public State CurrentState
        {
            get { return (State)Bridge.GetCurrentState(_Handle); }
        }

        /// <summary>
        /// get last state
        /// </summary>
        public State LastState
        {
            get { return (State)Bridge.GetLastState(_Handle); }
        }

        /// <summary>
        /// get or set progress
        /// </summary>
        public float Progress 
        {
            get { return Bridge.GetProgress(_Handle); }
            set { Bridge.SetProgress(_Handle, value); }
        }

        /// <summary>
        /// get or set return code
        /// </summary>
        public ReturnCode ReturnCode
        {
            get { return (ReturnCode)Bridge.GetReturnCode(_Handle); }
            set { Bridge.SetReturnCode(_Handle, (byte)value); }
        }

        /// <summary>
        /// get control code
        /// </summary>
        public ControlCode ControlCode
        {
            get { return (ControlCode)Bridge.GetControlCode(_Handle); }
        }

        /// <summary>
        /// switch pause/resume
        /// </summary>
        public void SwitchHold()
        {
            Bridge.SwitchHold(_Handle);
        }

        /// <summary>
        /// switch back/front busy
        /// </summary>
        public void SwitchBusy()
        {
            Bridge.SwitchBusy(_Handle);
        }
    }
}