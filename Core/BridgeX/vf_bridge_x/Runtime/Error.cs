using System;

namespace Vapula.Runtime
{
    public class Error
    {
        private IntPtr _Handle = IntPtr.Zero;

        public Error(IntPtr handle)
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
        /// get code for error
        /// </summary>
        public int Code 
        {
            get { return Bridge.WhatError(Handle); }
        }

        /// <summary>
        /// throw error
        /// </summary>
        public static void Throw(int code)
        {
            Bridge.ThrowError(code);
        }
    }
}
