using System;
using System.Collections.Generic;
using System.Reflection;

namespace TCM.Runtime
{
    public class LibraryCLR : Library
    {
        private static List<LibraryCLR> _Libraries 
            = new List<LibraryCLR>();

        public static LibraryCLR GetLibrary(IntPtr handle)
        {
            foreach (LibraryCLR lib in _Libraries) 
                if (lib._Handle == handle) return lib;
            return null;
        }

        private Assembly _Assembly;
        public Assembly Assembly
        {
            get { return _Assembly; }
        }

        public LibraryCLR(IntPtr handle)
            : base(handle)
        {
            _Libraries.Add(this);
        }

        public override void Dispose()
        {
            _Libraries.Remove(this);
            base.Dispose();
        }

        public bool MountEx(string path)
        {
            _Assembly = Assembly.LoadFrom(path);
            return true;
        }

        public bool UnmountEx() 
        {
            return true;
        }
    }
}
