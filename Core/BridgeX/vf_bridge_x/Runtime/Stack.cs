using System;

namespace Vapula.Runtime
{
    /// <summary>
    /// stack
    /// </summary>
    public class Stack : IDisposable
    {
        private Dataset _Dataset 
            = null;
        private Context _Context
            = null;
        private Error _Error
            = null;

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
                return 
                    Base.ToStringAnsi(
                    Bridge.GetMethodId(Handle)); 
            }
        }

        /// <summary>
        /// get dataset
        /// </summary>
        public Dataset Dataset
        {
            get 
            {
                if (_Dataset == null) 
                {
                    _Dataset = new Dataset(
                        Bridge.GetDataset(Handle));
                }
                return _Dataset; 
            }
        }

        /// <summary>
        /// get context
        /// </summary>
        public Context Context
        {
            get 
            {
                if (_Context == null)
                {
                    _Context = new Context(
                        Bridge.GetContext(Handle));
                }
                return _Context; 
            }
        }

        /// <summary>
        /// get if stack has protect
        /// </summary>
        public bool HasProtect
        {
            get { return Bridge.HasProtect(Handle); }
        }

        /// <summary>
        /// get error
        /// </summary>
        public Error Error
        {
            get 
            {
                if (_Error == null)
                {
                    _Error = new Error(
                        Bridge.GetError(Handle));
                }
                return _Error;
            }
        }

        public void Dispose()
        {
            _Context = null;
            _Dataset = null;
            _Error = null;
        }
    }
}
