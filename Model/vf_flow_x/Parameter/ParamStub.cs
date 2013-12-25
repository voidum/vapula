using Vapula.Model;

namespace Vapula.Flow
{
    /// <summary>
    /// 参数存根
    /// </summary>
    public class ParamStub
    {
        #region 字段
        private ParamPoint _Self
            = new ParamPoint(-1, -1);
        private ParamPoint _Supply
            = new ParamPoint(-1, -1);
        private bool _IsExport
            = false;
        private bool _IsOptional
            = false;

        private Node _Parent
            = null;
        private Parameter _Prototype
            = null;
        private object _Value
            = null;
        #endregion

        #region 属性
        public ParamPoint Self
        {
            get { return _Self; }
        }

        public ParamPoint Supply
        {
            get { return _Supply; }
            set
            {
                _Supply.NodeId = value.NodeId;
                _Supply.ParamId = value.ParamId;
            }
        }

        /// <summary>
        /// 获取或设置参数是否导出
        /// </summary>
        public bool IsExport
        {
            get { return _IsExport; }
            set 
            {
                _IsExport = value;
                if (!value)
                    _IsOptional = false;
            }
        }

        public bool IsOptional 
        {
            get { return _IsOptional; }
            set { _IsOptional = value; }
        }

        /// <summary>
        /// 获取参数是否有值
        /// </summary>
        public bool HasValue 
        {
            get { return _Value != null; }
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        /// 获取或设置所在节点
        /// </summary>
        public Node Parent
        {
            get { return _Parent; }
            set 
            {
                _Parent = value;
                _Self.NodeId = 
                    (value == null ? -1 : value.Id);
            }
        }

        /// <summary>
        /// 获取或设置参数的原型
        /// </summary>
        public Parameter Prototype
        {
            get { return _Prototype; }
            set
            {
                _Prototype = value;
                _Self.ParamId =
                    (value == null ? -1 : value.Id);
            }
        }
        #endregion
    }
}
