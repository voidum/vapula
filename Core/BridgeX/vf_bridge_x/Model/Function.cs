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
        private string _Id;
        private string _Entry;
        private Library _Library;
        private TagList _Tags
            = new TagList();
        private List<Parameter> _Parameters 
            = new List<Parameter>();
        private TagList _Attach 
            = null;
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
        #endregion

        #region 序列化
        /// <summary>
        /// 由XML解析功能描述
        /// </summary>
        public static Function Parse(XElement xml)
        {
            Function func = new Function();
            func._Id = xml.Element("id").Value;
            func._Entry = xml.Element("entry").Value;
            var xml_tags = xml.Element("tags");
            func._Tags = TagList.Parse(xml_tags);
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
                new XElement("params"),
                new XElement("id", Id),
                new XElement("entry", Entry),
                _Tags.ToXML());
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
        /// 获取或设置功能的标识
        /// </summary>
        public string Id
        {
            get
            {
                if (_Id == null)
                    return "";
                return _Id;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Id = null;
                else _Id = value;
            }
        }

        /// <summary>
        /// 获取或设置功能的入口
        /// </summary>
        public string Entry
        {
            get
            {
                if (_Entry == null)
                    return "";
                return _Entry;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Entry = null;
                else _Entry = value;
            }
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
        public TagList Tags
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
        /// 获取功能的附加数据
        /// </summary>
        public TagList Attach 
        {
            get
            {
                if (_Attach == null)
                    _Attach = new TagList();
                return _Attach;
            }
        }

        /// <summary>
        /// 获取或设置功能的名称
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
        /// 获取或设置功能的描述
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
