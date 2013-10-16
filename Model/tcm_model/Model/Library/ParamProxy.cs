using System.ComponentModel;
using System;

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
            get { return _Value == null; }
        }
    }

    public class ParamDescriptor : PropertyDescriptor
    {
        private ParamProxy _ParamProxy 
            = null;
  
        public ParamDescriptor(ParamProxy param, Attribute[] attrs)  
            : base(param.Parameter.Name, attrs)  
        {  
            _ParamProxy = param;  
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get { return _ParamProxy.GetType(); }
        }

        public override object GetValue(object component)
        {
            return _ParamProxy.Value;
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get { return Base.GetNullableCLRType(_ParamProxy.Parameter.Type); }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            _ParamProxy.Value = value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
}
