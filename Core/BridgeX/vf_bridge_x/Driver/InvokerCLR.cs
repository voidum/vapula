using System;
using System.Collections.Generic;
using System.Reflection;

namespace Vapula.Runtime
{
    public class InvokerCLR : Invoker
    {
        private static List<InvokerCLR> _Invokers
            = new List<InvokerCLR>();

        public static InvokerCLR GetInvoker(IntPtr handle)
        {
            foreach (InvokerCLR inv in _Invokers)
                if (inv._Handle == handle) return inv;
            return null;
        }

        private LibraryCLR _Library = null;

        public InvokerCLR(IntPtr handle, LibraryCLR lib)
            : base(handle)
        {
            _Library = lib;
            _Invokers.Add(this);
        }

        public override void Dispose()
        {
            _Invokers.Remove(this);
            base.Dispose();
        }

        public void OnProcess()
        {
            string entry = 
                _Library.GetProcessSym(Stack.MethodId);
            if (string.IsNullOrWhiteSpace(entry))
                CallEntry("Program", "Process");
            else
            {
                string[] strs = entry.Split(new char[] { '.' });
                CallEntry(strs[0], strs[1]);
            }
        }

        public void OnRollback()
        {
            string entry =
                _Library.GetRollbackSym(Stack.MethodId);
            if (string.IsNullOrWhiteSpace(entry))
                CallEntry("Program", "Rollback");
            else
            {
                string[] strs = entry.Split(new char[] { '.' });
                CallEntry(strs[0], strs[1]);
            }
        }

        private void CallEntry(string clsid, string mtid)
        {
            Assembly assembly = _Library.Assembly;
            Type type = assembly.GetType(_Library.Id + "." + clsid);
            try
            {
                object instance = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(mtid);
                method.Invoke(instance, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
