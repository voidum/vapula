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
        private string _Name;
        private string _Publisher;
        private string _Description;
        private string _Version;
        private string _Path;
        private Tag _Tag = new Tag();
        private List<Function> _Functions = new List<Function>();
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
                foreach (var func in _Functions)
                    if (func.Id == id)
                        return func;
                return null;
            }
        }
        #endregion

        #region 序列化
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
                library.Path = path;
                return library;
            }
            return null;
        }

        /// <summary>
        /// 由XML解析组件对象
        /// </summary>
        public static Library Parse(XElement xml)
        {
            Library lib = new Library();
            lib.Id = xml.Element("id").Value;
            lib.Runtime = xml.Element("runtime").Value;
            lib.Version = xml.Element("version").Value;
            lib.Name = xml.Element("name").Value;
            lib.Publisher = xml.Element("publisher").Value;
            lib.Description = xml.Element("description").Value;
            var xml_funcs = xml.Element("functions").Elements("function");
            foreach (var xml_func in xml_funcs)
            {
                Function func = Function.Parse(xml_func);
                func.Library = lib;
                lib._Functions.Add(func);
            }
            return lib;
        }

        /// <summary>
        /// 将组件序列化成XML元素
        /// </summary>
        public XElement ToXML()
        {
            XElement xml = new XElement("library",
                new XElement("runtime", Runtime),
                new XElement("id", Id),
                new XElement("name", Name),
                new XElement("description", Description),
                new XElement("version", Version),
                new XElement("publisher", Publisher),
                new XElement("functions"));
            foreach (var func in _Functions)
            {
                var xml_func = func.ToXML();
                xml.Element("functions").Add(xml_func);
            }
            return xml;
        }
        #endregion

        #region 集合
        public void Clear()
        {
            foreach (var func in _Functions)
                func.Clear();
            _Functions.Clear();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置组件标识
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
        /// 获取或设置组件运行时
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
        /// 获取或设置组件名称
        /// </summary>
        public string Name 
        {
            get
            {
                if (_Name == null)
                    return "";
                return _Name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Name = null;
                else _Name = value;
            }
        }

        /// <summary>
        /// 获取或设置组件发布方
        /// </summary>
        public string Publisher 
        {
            get
            {
                if (_Publisher == null)
                    return "";
                return _Publisher;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Publisher = null;
                else _Publisher = value;
            }
        }

        /// <summary>
        /// 获取或设置组件描述
        /// </summary>
        public string Description 
        {
            get
            {
                if (_Description == null)
                    return "";
                return _Description;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Description = null;
                else _Description = value;
            }
        }

        /// <summary>
        /// 获取或设置组件版本
        /// </summary>
        public string Version
        {
            get
            {
                if (_Version == null)
                    return "";
                return _Version;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Version = null;
                else _Version = value;
            }
        }

        /// <summary>
        /// 获取库的附加数据表
        /// </summary>
        public Tag Tag
        {
            get { return _Tag; }
        }

        /// <summary>
        /// 获取或设置库的路径
        /// </summary>
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

        /// <summary>
        /// 获取所有功能
        /// </summary>
        public List<Function> Functions
        {
            get { return _Functions; }
        }
        #endregion
    }
}