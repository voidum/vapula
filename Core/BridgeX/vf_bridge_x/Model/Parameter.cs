using System.Xml.Linq;

namespace Vapula.Model
{
    /// <summary>
    /// Vapula模型的参数描述
    /// </summary>
    public class Parameter
    {
        #region 字段
        private int _Id;
        private DataType _Type;
        private ParamMode _Mode;
        private Function _Function;
        private TagList _Tags
            = new TagList();
        #endregion

        #region 构造
        /// <summary>
        /// 构造参数描述
        /// </summary>
        public Parameter() { }
        #endregion

        #region 序列化
        /// <summary>
        /// 由XML解析参数描述
        /// </summary>
        public static Parameter Parse(XElement xml)
        {
            Parameter param = new Parameter();
            param.Id = int.Parse(xml.Attribute("id").Value);
            param.Type = (DataType)int.Parse(xml.Element("type").Value);
            param.Mode = (ParamMode)int.Parse(xml.Element("mode").Value);
            var xml_tags = xml.Element("tags");
            param._Tags = TagList.Parse(xml_tags);
            return param;
        }

        /// <summary>
        /// 将参数描述序列化成XML元素
        /// </summary>
        public XElement ToXML()
        {
            XElement xml = new XElement("param",
                new XAttribute("id", Id),
                new XElement("type", (int)Type),
                new XElement("mode", (int)Mode),
                _Tags.ToXML());
            return xml;
        }
        #endregion

        #region 集合
        /// <summary>
        /// 清理参数描述
        /// </summary>
        public void Clear()
        {
            Tags.Clear();
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
        /// 获取或设置参数的模式
        /// </summary>
        public ParamMode Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        /// <summary>
        /// 获取或设置参数所属的功能
        /// </summary>
        public Function Function
        {
            get { return _Function; }
            set { _Function = value; }
        }

        /// <summary>
        /// 获取参数的标签表
        /// </summary>
        public TagList Tags
        {
            get { return _Tags; }
        }

        /// <summary>
        /// 获取或设置参数的名称
        /// </summary>
        public string Name
        {
            get
            {
                var tag = _Tags["name"];
                if (tag == null)
                    return "";
                return (string)tag;
            }
            set
            {
                _Tags["name"] = 
                    string.IsNullOrWhiteSpace(value) ? 
                    null : value;
            }
        }

        /// <summary>
        /// 获取或设置参数的描述
        /// </summary>
        public string Description
        {
            get
            {
                var tag = _Tags["description"];
                if (tag == null)
                    return "";
                return (string)tag;
            }
            set
            {
                _Tags["description"] =
                    string.IsNullOrWhiteSpace(value) ?
                    null : value;
            }
        }
        #endregion
    }
}
