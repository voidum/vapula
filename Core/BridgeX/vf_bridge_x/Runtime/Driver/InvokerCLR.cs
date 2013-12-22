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

        public int CallEntry()
        {
            string clsid = "";
            string mtid = "";
            if(string.IsNullOrWhiteSpace(_Library.EntryDpt))
            {
                clsid = "Program";
                mtid = "Run";
            }
            else
            {
                string[] ents = _Library.EntryDpt.Split(new char[] { '.' });
                clsid = ents[0];
                mtid = ents[1];
            }
            Assembly assembly = _Library.Assembly;
            Type type = assembly.GetType(_Library.Id + "." + clsid);
            if (type == null) 
                return (int)ReturnCode.Error;
            try
            {
                object instance = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(mtid);
                ReturnCode ret = (ReturnCode)method.Invoke(instance,
                    new object[] { FunctionId, Envelope, Context });
                return (int)ret;
            }
            catch (Exception ex)
            {
                return (int)ReturnCode.Error;
            }
        }
    }
}
