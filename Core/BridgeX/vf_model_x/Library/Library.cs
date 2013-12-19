using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Vapula.Model
{
    /// <summary>
    /// Vapula模型的库描述
    /// </summary>
    public class Library
    {
        #region 字段
        private string _Id;
        private string _Runtime;
        private List<Tag> _Tags
            = new List<Tag>();
        private List<Function> _Functions 
            = new List<Function>();
        private object _Attach;
        #endregion

        #region 构造
        public Library() { }
        #endregion

        #region 索引器
        /// <summary>
        /// 根据指定标识获取功能
        /// </summary>
        public Function this[int id]
        {
            get
            {
                foreach (var func in Functions)
                    if (func.Id == id)
                        return func;
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
        /// 加载Vapula组件的库描述
        /// </summary>
        /// <param name="path">库描述文件的全路径</param>
        public static Library Load(string path)
        {
            var xrs = new XmlReaderSettings();
            xrs.ValidationType = ValidationType.Schema;
            xrs.Schemas.Add(null,
                System.IO.Path.Combine(Base.AppDir, "library.xsd"));
            XmlReader xr = XmlReader.Create(path, xrs);
            XDocument xml = XDocument.Load(xr);
            while (xr.Read()) { }
            xr.Close();
            if (xml != null)
            {
                XElement xeroot = xml.Element("library");
                Library library = Parse(xeroot);
                return library;
            }
            return null;
        }

        /// <summary>
        /// 由XML解析库描述
        /// </summary>
        public static Library Parse(XElement xml)
        {
            Library lib = new Library();
            lib.Id = xml.Element("id").Value;
            lib.Runtime = xml.Element("runtime").Value;
            var xmls_tag = xml.Element("tags").Elements("tag");
            foreach (var xml_tag in xmls_tag)
                lib.Tags.Add(Tag.Parse(xml_tag));
            var xmls_func = xml.Element("functions").Elements("function");
            foreach (var xml_func in xmls_func)
            {
                var func = Function.Parse(xml_func);
                func.Library = lib;
                lib.Functions.Add(func);
            }
            return lib;
        }

        /// <summary>
        /// 将库描述序列化成XML元素
        /// </summary>
        public XElement ToXML()
        {
            XElement xml = new XElement("library",
                new XElement("id", Id),
                new XElement("runtime", Runtime),
                new XElement("tags"),
                new XElement("functions"));
            foreach (var tag in _Tags)
            {
                var xml_tag = tag.ToXML();
                xml.Element("tags").Add(xml_tag);
            }
            foreach (var func in _Functions)
            {
                var xml_func = func.ToXML();
                xml.Element("functions").Add(xml_func);
            }
            return xml;
        }
        #endregion

        #region 集合
        /// <summary>
        /// 清理库描述
        /// </summary>
        public void Clear()
        {
            foreach (var func in Functions)
                func.Clear();
            Functions.Clear();
            Tags.Clear();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置库的标识
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
        /// 获取或设置库的运行时
        /// </summary>
        public string Runtime
        {
            get
            {
                if (_Runtime == null)
                    return "";
                return _Runtime;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Runtime = null;
                else _Runtime = value;
            }
        }

        /// <summary>
        /// 获取库的标签表
        /// </summary>
        public List<Tag> Tags
        {
            get { return _Tags; }
        }

        /// <summary>
        /// 获取库的功能集合
        /// </summary>
        public List<Function> Functions
        {
            get { return _Functions; }
        }

        /// <summary>
        /// 获取或设置库的附加数据
        /// </summary>
        public object Attach
        {
            get { return _Attach; }
            set { _Attach = value; }
        }

        /// <summary>
        /// 获取或设置库的名称
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
        /// 获取或设置库的发布方
        /// </summary>
        public string Publisher 
        {
            get
            {
                Tag tag = GetTag("publisher");
                if (tag == null)
                    return "";
                return tag.Value;
            }
            set
            {
                string v =
                    string.IsNullOrWhiteSpace(value) ?
                    "" : value;
                Tag tag = GetTag("publisher");
                if (tag == null)
                    _Tags.Add(new Tag("publisher", v));
                else
                    tag.Value = v;
            }
        }

        /// <summary>
        /// 获取或设置库的描述
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

        /// <summary>
        /// 获取或设置库的版本
        /// </summary>
        public string Version
        {
            get
            {
                Tag tag = GetTag("version");
                if (tag == null)
                    return "";
                return tag.Value;
            }
            set
            {
                string v =
                    string.IsNullOrWhiteSpace(value) ?
                    "" : value;
                Tag tag = GetTag("version");
                if (tag == null)
                    _Tags.Add(new Tag("version", v));
                else
                    tag.Value = v;
            }
        }
        #endregion
    }
}