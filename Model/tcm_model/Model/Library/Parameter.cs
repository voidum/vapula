using System.Xml.Linq;

namespace TCM.Model
{
    /// <summary>
    /// TCM模型的参数描述
    /// </summary>
    public partial class Parameter
    {
        #region 字段
        protected int _Id;
        protected DataType _Type;
        protected bool _IsIn;
        protected string _Catalog;
        protected string _Name;
        protected string _Description;
        protected object _Value;
        protected Function _Function = null;
        #endregion

        #region 构造
        /// <summary>
        /// 构造参数描述
        /// </summary>
        public Parameter() { }
        #endregion

        #region 序列化
        public static Parameter Parse(XElement xe)
        {

            Parameter param = new Parameter();
            if(xe.Attribute("id") == null) return null;
            param.Id = int.Parse(xe.Attribute("id").Value);
            if (xe.Attribute("type") == null) return null;
            param.Type = (DataType)int.Parse(xe.Attribute("type").Value);
            if(xe.Attribute("in") == null) return null;
            param.IsIn = (xe.Attribute("in").Value == "true");
            param.Name = xe.Element("name").Value;
            param.Catalog = xe.Element("catalog").Value;
            param.Description = xe.Element("description").Value;
            return param;
        }

        public XElement ToXML()
        {
            XElement xe = new XElement("param",
                new XAttribute("id", _Id),
                new XAttribute("type", (int)_Type),
                new XAttribute("in", _IsIn ? "true" : "false"),
                new XElement("description", new XCData(Description)),
                new XElement("name", new XCData(Name)),
                new XElement("catalog", new XCData(Catalog)));
            return xe;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置参数的标识
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// 获取或设置参数的类型
        /// </summary>
        public DataType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        /// <summary>
        /// 获取或设置参数是否是输入参数
        /// </summary>
        public bool IsIn
        {
            get { return _IsIn; }
            set { _IsIn = value; }
        }

        /// <summary>
        /// 获取或设置参数的分类
        /// </summary>
        public string Catalog
        {
            get
            {
                if (_Catalog == null) return "";
                return _Catalog;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _Catalog = null;
                else _Catalog = value;
            }
        }

        /// <summary>
        /// 获取或设置参数的名称
        /// </summary>
        public string Name
        {
            get
            {
                if (_Name == null) return "";
                return _Name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _Name = null;
                else _Name = value;
            }
        }

        /// <summary>
        /// 获取或设置参数的描述
        /// </summary>
        public string Description
        {
            get
            {
                if (_Description == null) return "";
                return _Description;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _Description = null;
                else _Description = value;
            }
        }

        /// <summary>
        /// 获取或设置参数的值
        /// </summary>
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        /// 获取或设置参数所属的功能
        /// </summary>
        public Function Function
        {
            get { return _Function; }
            set { _Function = value; }
        }
        #endregion
    }
}
