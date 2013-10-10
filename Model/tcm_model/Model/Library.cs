using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using TCM.Helper;

namespace TCM.Model
{
    /// <summary>
    /// TCM模型的库描述
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
                foreach (Function func in _Functions)
                    if (func.Id == id) return func;
                return null;
            }
        }
        #endregion

        #region 序列化
        public static Library Load(string path)
        {
            XmlReaderSettings xrs = new XmlReaderSettings();
            xrs.ValidationType = ValidationType.Schema;
            xrs.Schemas.Add(null, Base.AppDir + "library.xsd");
            XmlReader xr = null;
            XDocument xml = null;
            try
            {
                xr = XmlReader.Create(path, xrs);
                xml = XDocument.Load(xr);
                while (xr.Read()) { }
            }
            catch (Exception ex)
            {
                Base.Logger.WriteLog(LogType.Error, ex.Message);
            }
            finally
            {
                xr.Close();
            }
            if (xml != null)
            {
                XElement xeroot = xml.Element("library");
                Library library = Parse(xeroot);
                return library;
            }
            return null;
        }

        /// <summary>
        /// 由XML解析组件对象
        /// </summary>
        public static Library Parse(XElement xe)
        {
            Library lib = new Library();
            lib.Id = xe.Element("id").Value;
            lib.Runtime = xe.Element("runtime").Value;
            lib.Version = xe.Element("version").Value;
            lib.Name = xe.Element("name").Value;
            lib.Publisher = xe.Element("publisher").Value;
            lib.Description = xe.Element("description").Value;
            foreach (XElement xefunc in xe.Element("functions").Elements("function"))
            {
                Function func = Function.Parse(xefunc);
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
            XElement xe = new XElement("library",
                new XElement("name", new XCData(Name)),
                new XElement("version", new XCData(Version)),
                new XElement("description", new XCData(Description)),
                new XElement("publisher", new XCData(Publisher)),
                new XElement("functions"),
                new XAttribute("id", _Id));
            foreach (Function func in _Functions)
            {
                XElement xefunc = func.ToXML();
                xe.Element("functions").Add(xefunc);
            }
            return xe;
        }
        #endregion

        #region 集合
        public void Clear()
        {
            foreach (Function func in _Functions)
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
                if (_Id == null) return "";
                return _Id;
            }
            set 
            {
                if (string.IsNullOrWhiteSpace(value)) _Id = null;
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
                if (_Runtime == null) return "";
                return _Runtime;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _Runtime = null;
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
        /// 获取或设置组件发布方
        /// </summary>
        public string Publisher 
        {
            get
            {
                if (_Publisher == null) return "";
                return _Publisher;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _Publisher = null;
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
        /// 获取或设置组件版本
        /// </summary>
        public string Version
        {
            get
            {
                if (_Version == null) return "";
                return _Version;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) _Version = null;
                else _Version = value;
            }
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
