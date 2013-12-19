using System.Collections.Generic;
using System.Xml.Linq;

namespace Vapula.Model
{
    /// <summary>
    /// Vapula模型的功能描述
    /// </summary>
    public class Function
    {
        #region 字段
        private int _Id;
        private Library _Library;
        private List<Tag> _Tags 
            = new List<Tag>();
        private List<Parameter> _Parameters 
            = new List<Parameter>();
        #endregion

        #region 构造
        public Function() { }
        #endregion

        #region 索引器
        /// <summary>
        /// 根据指定标识获取参数
        /// </summary>
        public Parameter this[int id]
        {
            get
            {
                foreach (var param in Parameters)
                    if (param.Id == id)
                        return param;
                return null;
            }
        }

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
        /// 由XML解析功能描述
        /// </summary>
        public static Function Parse(XElement xml)
        {
            Function func = new Function();
            func.Id = int.Parse(xml.Attribute("id").Value);
            var xmls_tag = xml.Element("tags").Elements("tag");
            foreach (var xml_tag in xmls_tag)
                func.Tags.Add(Tag.Parse(xml_tag));
            var xml_params = xml.Element("params").Elements("param");
            foreach (var xml_param in xml_params)
            {
                var param = Parameter.Parse(xml_param);
                param.Function = func;
                func.Parameters.Add(param);
            }
            return func;
        }

        /// <summary>
        /// 将功能描述序列化为XML元素
        /// </summary>
        public XElement ToXML()
        {
            XElement xml = new XElement("function",
                new XElement("tags"),
                new XElement("params"),
                new XAttribute("id", Id));
            foreach (var tag in _Tags)
            {
                var xml_tag = tag.ToXML();
                xml.Element("tags").Add(xml_tag);
            }
            foreach (var param in Parameters)
            {
                var xml_param = param.ToXML();
                xml.Element("params").Add(xml_param);
            }
            return xml;
        }
        #endregion

        #region 集合
        /// <summary>
        /// 清理功能描述
        /// </summary>
        public void Clear()
        {
            foreach(var param in Parameters)
                param.Clear();
            Parameters.Clear();
            Tags.Clear();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取功能的标识
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// 获取功能所在的组件
        /// </summary>
        public Library Library
        {
            get { return _Library; }
            set { _Library = value; }
        }

        /// <summary>
        /// 获取功能的标签表
        /// </summary>
        public List<Tag> Tags
        {
            get { return _Tags; }
        }

        /// <summary>
        /// 获取功能的参数集合
        /// </summary>
        public List<Parameter> Parameters
        {
            get { return _Parameters; }
        }

        /// <summary>
        /// 获取或设置功能的名称
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
        /// 获取或设置功能的描述
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
    }
}
