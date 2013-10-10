using System;
using System.Collections.Generic;
using System.Reflection;

namespace TCM.Runtime
{
    public class ExecutorCLR : Executor
    {
        private static List<ExecutorCLR> _Executors
            = new List<ExecutorCLR>();

        public static ExecutorCLR GetExecutor(IntPtr handle)
        {
            foreach (ExecutorCLR exec in _Executors)
                if (exec._Handle == handle) return exec;
            return null;
        }

        private LibraryCLR _Library = null;

        public ExecutorCLR(IntPtr handle, LibraryCLR lib)
            : base(handle)
        {
            _Library = lib;
            _Executors.Add(this);
        }

        public override void Dispose()
        {
            _Executors.Remove(this);
            base.Dispose();
        }

        public int CallEntry()
        {
            Assembly assembly = _Library.Assembly;
            Type type = assembly.GetType(_Library.Id + ".Program");
            object instance = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("Run");
            ReturnCode rc = (ReturnCode)method.Invoke(instance,
                new object[] { FunctionId, Envelope, Context });
            return (int)rc;
        }
    }
}
