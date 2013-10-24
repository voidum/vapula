using System;
using System.ComponentModel;

namespace TCM.Model
{
    public class ParamStub
    {
        private Parameter _Prototype
            = null;
        private string _Value 
            = null;

        public Parameter Prototype
        {
            get { return _Prototype; }
            set { _Prototype = value; }
        }

        public string Value
        {
            get
            {
                if (_Value == null) return "";
                return _Value;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _Value = null;
                else _Value = value;
            }
        }

        public bool HasValue 
        {
            get 
            {
                if (_Prototype.Type == DataType.StringA ||
                    _Prototype.Type == DataType.StringW)
                    return true;
                else
                    return _Value != null; 
            }
        }

        public bool IsValid
        {
            get
            {
                try
                {
                    if (!HasValue) return true;
                    switch (_Prototype.Type)
                    {
                        case DataType.Bool:
                            return (Value == "true" || Value == "false");
                        case DataType.Int8:
                            char.Parse(Value); return true;
                        case DataType.Int16:
                            short.Parse(Value); return true;
                        case DataType.Int32:
                            int.Parse(Value); return true;
                        case DataType.Int64:
                            long.Parse(Value); return true;
                        case DataType.UInt8:
                            byte.Parse(Value); return true;
                        case DataType.UInt16:
                            ushort.Parse(Value); return true;
                        case DataType.UInt32:
                            uint.Parse(Value); return true;
                        case DataType.Pointer:
                        case DataType.UInt64:
                            ulong.Parse(Value); return true;
                        case DataType.Real32:
                            float.Parse(Value); return true;
                        case DataType.Real64:
                            double.Parse(Value); return true;
                        default:
                            return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
