using System;
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
        private TagList _Tags
            = new TagList();
        private List<Method> _Methods
            = new List<Method>();

        private string _Path;
        private TagList _Attach
            = null;
        #endregion

        #region 构造
        public Library() { }
        #endregion

        #region 索引器
        /// <summary>
        /// 根据指定标识获取功能
        /// </summary>
        public Method this[string id]
        {
            get
            {
                foreach (var mt in Methods)
                    if (mt.Id == id)
                        return mt;
                return null;
            }
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
                System.IO.Path.Combine(Base.RuntimeDir, "library.xsd"));
            try
            {
                XmlReader xr = XmlReader.Create(path, xrs);
                XDocument xml = XDocument.Load(xr);
                while (xr.Read()) { }
                xr.Close();
                if (xml != null)
                {
                    XElement xeroot = xml.Element("library");
                    Library library = Parse(xeroot);
                    library.Path = path;
                    return library;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            var xml_tags = xml.Element("tags");
            lib._Tags = TagList.Parse(xml_tags);
            var xmls_mt = xml.Element("methods").Elements("method");
            foreach (var xml_mt in xmls_mt)
            {
                var mt = Method.Parse(xml_mt);
                mt.Library = lib;
                lib.Methods.Add(mt);
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
                new XElement("methods"),
                _Tags.ToXML());
            foreach (var mt in _Methods)
            {
                var xml_mt = mt.ToXML();
                xml.Element("methods").Add(xml_mt);
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
            foreach (var mt in Methods)
                mt.Clear();
            Methods.Clear();
            Tags.Clear();
            if (_Attach != null)
                _Attach.Clear();
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
        public TagList Tags
        {
            get { return _Tags; }
        }

        /// <summary>
        /// 获取库的功能集合
        /// </summary>
        public List<Method> Methods
        {
            get { return _Methods; }
        }

        /// <summary>
        /// 获取库的路径
        /// </summary>
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

        /// <summary>
        /// 获取或设置库的附加数据
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
        /// 获取或设置库的名称
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
        /// 获取或设置库的发布方
        /// </summary>
        public string Publisher
        {
            get
            {
                var tag = _Tags["publisher"];
                if (tag == null)
                    return "";
                return (string)tag;
            }
            set
            {
                _Tags["publisher"] =
                    string.IsNullOrWhiteSpace(value) ?
                    null : value;
            }
        }

        /// <summary>
        /// 获取或设置库的描述
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

        /// <summary>
        /// 获取或设置库的版本
        /// </summary>
        public string Version
        {
            get
            {
                var tag = _Tags["version"];
                if (tag == null)
                    return "";
                return (string)tag;
            }
            set
            {
                _Tags["version"] =
                    string.IsNullOrWhiteSpace(value) ?
                    null : value;
            }
        }
        #endregion
    }
}