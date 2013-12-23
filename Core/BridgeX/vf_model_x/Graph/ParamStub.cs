using System;
using System.ComponentModel;

namespace Vapula.Model
{
    /// <summary>
    /// 参数坐标
    /// </summary>
    public class ParamPoint
    {
        private int _Node;
        private int _Parameter;

        public int Node
        {
            get { return _Node; }
            set 
            {
                if (_Node < 1) return;
                _Node = value;
            }
        }

        public int Parameter
        {
            get { return _Parameter; }
            set 
            {
                if (_Parameter < 1) return;
                _Parameter = value;
            }
        }

        public ParamPoint(int node, int param)
        {
            _Node = node;
            _Parameter = param;
        }
    }

    /// <summary>
    /// 参数存根
    /// </summary>
    public class ParamStub
    {
        #region 字段
        private Parameter _Prototype
            = null;
        private string _Value 
            = null;
        private ParamPoint _From
            = null;
        private bool _IsExport
            = false;
        #endregion

        #region 属性
        public Parameter Prototype
        {
            get { return _Prototype; }
            set { _Prototype = value; }
        }

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public bool IsExport
        {
            get { return _IsExport; }
            set { _IsExport = value; }
        }

        public bool HasValue 
        {
            get { return _Value != null; }
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
        #endregion
    }
}
