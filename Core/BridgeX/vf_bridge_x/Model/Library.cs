using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Vapula.Model
{
    /// <summary>
    /// schema for Vapula library
    /// </summary>
    public class Library
    {
        #region Fields
        private string _Id;
        private string _Runtime;
        private List<Method> _Methods
            = new List<Method>();
        private TagList _Tags
            = new TagList();
        private object _Attach
            = null;
        #endregion

        #region Ctor
        public Library() { }
        #endregion

        #region Indexer
        /// <summary>
        /// get method by id
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

        #region Serialization
        /// <summary>
        /// load Vapula library schema
        /// </summary>
        /// <param name="path">path for schema file</param>
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
        /// parse schema from XML
        /// </summary>
        public static Library Parse(XElement xml)
        {
            Library lib = new Library();
            lib.Id = xml.Element("id").Value;
            lib.Runtime = xml.Element("runtime").Value;
            var xe_tags = xml.Element("tags");
            lib._Tags = TagList.Parse(xe_tags);
            var xes = xml.Element("methods").Elements("method");
            foreach (var xe in xes)
            {
                var mt = Method.Parse(xe);
                mt.Library = lib;
                lib.Methods.Add(mt);
            }
            return lib;
        }

        /// <summary>
        /// output schema to XML
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
                var xe = mt.ToXML();
                xml.Element("methods").Add(xe);
            }
            return xml;
        }
        #endregion

        #region Collection
        /// <summary>
        /// clear schema
        /// </summary>
        public void Clear()
        {
            foreach (var mt in Methods)
                mt.Clear();
            Methods.Clear();
            Tags.Clear();
        }
        #endregion

        #region Properties
        /// <summary>
        /// get or set id
        /// </summary>
        public string Id
        {
            get { return _Id; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Id = null;
                else _Id = value;
            }
        }

        /// <summary>
        /// get or set runtime
        /// </summary>
        public string Runtime
        {
            get { return _Runtime; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Runtime = null;
                else _Runtime = value;
            }
        }

        /// <summary>
        /// get methods
        /// </summary>
        public List<Method> Methods
        {
            get { return _Methods; }
        }

        /// <summary>
        /// get tags
        /// </summary>
        public TagList Tags
        {
            get { return _Tags; }
        }

        /// <summary>
        /// get or set name
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
        /// get or set publisher
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
        /// get or set description
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
        /// get or set version
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