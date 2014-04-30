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

        public void CallEntry()
        {
            string entry_sym = 
                _Library.GetProcessSym(Stack.MethodId);
            string clsid = "";
            string mtid = "";
            if(string.IsNullOrWhiteSpace(entry_sym))
            {
                clsid = "Program";
                mtid = "Run";
            }
            else
            {
                string[] strs = entry_sym.Split(new char[] { '.' });
                clsid = strs[0];
                mtid = strs[1];
            }
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
