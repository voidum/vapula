using System;
using System.ComponentModel;

namespace TCM.Model
{
    public class ParamProxy
    {
        private Parameter _Parameter 
            = null;
        private object _Value 
            = null;

        public Parameter Parameter
        {
            get { return _Parameter; }
            set { _Parameter = value; }
        }

        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public bool HasValue 
        {
            get { return _Value != null; }
        }
    }
}
