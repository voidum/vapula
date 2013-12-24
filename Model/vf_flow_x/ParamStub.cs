using System;
using System.ComponentModel;
using Vapula.Model;

namespace Vapula.Flow
{
    /// <summary>
    /// 参数坐标
    /// </summary>
    public class ParamPoint
    {
        private int _NodeId;
        private int _ParamId;

        public int NodeId
        {
            get { return _NodeId; }
            set 
            {
                if (_NodeId < 1) return;
                _NodeId = value;
            }
        }

        public int ParamId
        {
            get { return _ParamId; }
            set 
            {
                if (_ParamId < 1) return;
                _ParamId = value;
            }
        }

        public bool IsValid
        {
            get { return _NodeId > 0 && _ParamId > 0; }
        }

        public ParamPoint(int node, int param)
        {
            _NodeId = node;
            _ParamId = param;
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
        private ParamPoint _Self
            = new ParamPoint(-1, -1);
        private ParamPoint _Supply
            = new ParamPoint(-1, -1);
        private object _Value
            = null;
        private bool _IsExport
            = false;
        #endregion

        #region 属性
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public Parameter Prototype
        {
            get { return _Prototype; }
        }

        public ParamPoint Self
        {
            get { return _Self; }
        }

        public ParamPoint Supply
        {
            get { return _Supply; }
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
        #endregion
    }
}
