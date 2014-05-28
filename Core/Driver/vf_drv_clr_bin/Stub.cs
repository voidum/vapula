using System;
using System.Collections.Generic;
using System.Reflection;

namespace Vapula.Driver
{
    public class Stub
    {
        private static List<Stub> _Stubs
            = new List<Stub>();

        public static Stub GetStub(IntPtr handle)
        {
            foreach (Stub stub in _Stubs) 
                if (stub._Handle == handle) 
                    return stub;
            return null;
        }

        private IntPtr _Handle;
        private Assembly _Assembly;
        private string _Id;

        public Assembly Assembly
        {
            get { return _Assembly; }
        }

        public Stub(IntPtr handle)
        {
            _Handle = handle;
            _Stubs.Add(this);
        }

        public void Dispose()
        {
            _Stubs.Remove(this);
        }

        public bool Mount(string path, string id)
        {
            try {
                _Assembly = Assembly.LoadFrom(path);
                _Id = id;
            } catch {
                return false;
            }
            return true;
        }

        public bool Unmount() 
        {
            Dispose();
            return true;
        }

        public void OnProcess(string entry)
        {
            if (string.IsNullOrWhiteSpace(entry))
                CallEntry("Program", "Process");
            else
            {
                string[] strs = entry.Split(new char[] { '.' });
                CallEntry(strs[0], strs[1]);
            }
        }

        public void OnRollback(string entry)
        {
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
            Assembly assembly = _Assembly;
            Type type = assembly.GetType(_Id + "." + clsid);
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
