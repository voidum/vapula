using System;
using Vapula.API;

namespace Vapula.Runtime
{
    /// <summary>
    /// stack
    /// </summary>
    public class Stack
    {
        /// <summary>
        /// get stack for current thread
        /// </summary>
        public static Stack Instance
        {
            get  
            {
                IntPtr ptr = Bridge.GetCurrentStack();
                Stack stack = new Stack(ptr);
                return stack; 
            }
        }

        private IntPtr _Handle;

        public Stack(IntPtr handle)
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
        /// get method id
        /// </summary>
        public string MethodId
        {
            get 
            { 
                return Bridge.ToString(
                    Bridge.GetMethodId(Handle), false); 
            }
        }

        /// <summary>
        /// get dataset
        /// </summary>
        public Dataset Dataset
        {
            get { return new Dataset(Bridge.GetEnvelope(Handle)); }
        }

        /// <summary>
        /// get context
        /// </summary>
        public Context Context
        {
            get { return new Context(Bridge.GetContext(Handle)); }
        }

        /// <summary>
        /// get if stack is protected
        /// </summary>
        public bool IsProtected 
        {
            get { return Bridge.IsProtected(Handle); }
        }

        /// <summary>
        /// get error
        /// </summary>
        public Error Error
        {
            get { return new Error(Bridge.GetError(Handle)); }
        }

        /// <summary>
        /// clear data in CLR
        /// </summary>
        public void Clear() 
        {
        }
    }
}
