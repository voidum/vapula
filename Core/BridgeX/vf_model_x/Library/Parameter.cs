using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel;

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
        private List<Tag> _Tags
            = new List<Tag>();
        #endregion

        #region 构造
        /// <summary>
        /// 构造参数描述
        /// </summary>
        public Parameter() { }
        #endregion

        #region 索引器
        /// <summary>
        /// 根据指定键获取标签
        /// </summary>
        public Tag GetTag(string key)
        {
            foreach (var tag in _Tags)
                if (tag.Key == key)
                    return tag;
            return null;
        }
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
            var xmls_tag = xml.Element("tags").Elements("tag");
            foreach (var xml_tag in xmls_tag)
                param.Tags.Add(Tag.Parse(xml_tag));
            return param;
        }

        /// <summary>
        /// 将参数描述序列化成XML元素
        /// </summary>
        public XElement ToXML()
        {
            XElement xml = new XElement("param",
                new XElement("id", Id),
                new XElement("type", (int)Type),
                new XElement("mode", (int)Mode),
                new XElement("tags"));
            foreach (var tag in _Tags)
            {
                var xml_tag = tag.ToXML();
                xml.Element("tags").Add(xml_tag);
            }
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
        public List<Tag> Tags
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
                Tag tag = GetTag("name");
                if (tag == null)
                    return "";
                return tag.Value;
            }
            set
            {
                string v =
                    string.IsNullOrWhiteSpace(value) ?
                    "" : value;
                Tag tag = GetTag("name");
                if (tag == null)
                    _Tags.Add(new Tag("name", v));
                else
                    tag.Value = v;
            }
        }

        /// <summary>
        /// 获取或设置参数的类别
        /// </summary>
        public string Category
        {
            get
            {
                Tag tag = GetTag("category");
                if (tag == null)
                    return "";
                return tag.Value;
            }
            set
            {
                string v =
                    string.IsNullOrWhiteSpace(value) ?
                    "" : value;
                Tag tag = GetTag("category");
                if (tag == null)
                    _Tags.Add(new Tag("category", v));
                else
                    tag.Value = v;
            }
        }

        /// <summary>
        /// 获取或设置参数的描述
        /// </summary>
        public string Description
        {
            get
            {
                Tag tag = GetTag("description");
                if (tag == null)
                    return "";
                return tag.Value;
            }
            set
            {
                string v =
                    string.IsNullOrWhiteSpace(value) ?
                    "" : value;
                Tag tag = GetTag("description");
                if (tag == null)
                    _Tags.Add(new Tag("description", v));
                else
                    tag.Value = v;
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 创建参数存根
        /// </summary>
        public ParamStub CreateStub()
        {
            ParamStub stub = new ParamStub();
            stub.Prototype = this;
            return stub;
        }
        #endregion
    }
}
