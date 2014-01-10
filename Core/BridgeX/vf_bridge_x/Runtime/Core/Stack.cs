using System;
using Vapula.API;

namespace Vapula.Runtime
{
    public class Stack
    {
        private IntPtr _Handle;

        public IntPtr Handle
        {
            get
            {
                if (_Handle == IntPtr.Zero)
                    _Handle = IntPtr.Zero;// Bridge.GetCurrentStack();
                return _Handle;
            }
        }

        public int FunctionId
        {
            get { return Bridge.GetFunctionId(Handle); }
        }

        public IntPtr Envelope
        {
            get 
            { 
                return Bridge.GetEnvelope(Handle);
            }
        }

        public IntPtr Context
        {
            get
            {
                return Bridge.GetContext(Handle);
            }
        }
    }
}
