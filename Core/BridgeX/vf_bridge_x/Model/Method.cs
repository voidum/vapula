using System.Collections.Generic;
using System.Xml.Linq;

namespace Vapula.Model
{
    /// <summary>
    /// schema for Vapula method
    /// </summary>
    public class Method
    {
        #region Fields
        private string _Id;
        private bool _Protect;
        private string _ProcessSym;
        private string _RollbackSym;
        private Library _Library;
        private List<Field> _Fields
            = new List<Field>();
        private TagList _Tags
            = new TagList();
        #endregion

        #region Ctor
        public Method() { }
        #endregion

        #region Indexer
        /// <summary>
        /// get field by id
        /// </summary>
        public Field this[int id]
        {
            get
            {
                foreach (var field in Fields)
                    if (field.Id == id)
                        return field;
                return null;
            }
        }
        #endregion

        #region Serialization
        /// <summary>
        /// parse schema from XML
        /// </summary>
        public static Method Parse(XElement xml)
        {
            Method mt = new Method();
            mt._Id = xml.Element("id").Value;
            mt._Protect = (xml.Element("protect").Value == "true");
            mt._ProcessSym = xml.Element("entry").Element("process").Value;
            mt._RollbackSym = xml.Element("entry").Element("rollback").Value;
            var xe_tags = xml.Element("tags");
            mt._Tags = TagList.Parse(xe_tags);
            var xes = xml.Element("schema").Elements("fields");
            foreach (var xe in xes)
            {
                var param = Field.Parse(xe);
                param.Method = mt;
                mt.Fields.Add(param);
            }
            return mt;
        }

        /// <summary>
        /// output schema to XML
        /// </summary>
        public XElement ToXML()
        {
            XElement xml = new XElement("method",
                new XElement("schema"),
                new XElement("id", Id),
                new XElement("protect", _Protect ? "true" : "false"),
                new XElement("symbols",
                    new XElement("process", ProcessSym),
                    new XElement("rollback", RollbackSym)),
                _Tags.ToXML());
            foreach (var field in Fields)
            {
                var xe = field.ToXML();
                xml.Element("schema").Add(xe);
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
            foreach(var field in Fields)
                field.Clear();
            Fields.Clear();
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
        /// get or set process symbol
        /// </summary>
        public string ProcessSym
        {
            get { return _ProcessSym; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _ProcessSym = null;
                else _ProcessSym = value;
            }
        }

        /// <summary>
        /// get or set rollback symbol
        /// </summary>
        public string RollbackSym
        {
            get { return _RollbackSym; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _RollbackSym = null;
                else _RollbackSym = value;
            }
        }

        /// <summary>
        /// get or set protect
        /// </summary>
        public bool Protect 
        {
            get { return _Protect; }
            set { _Protect = value; }
        }

        /// <summary>
        /// get or set library
        /// </summary>
        public Library Library
        {
            get { return _Library; }
            set { _Library = value; }
        }

                /// <summary>
        /// get fields
        /// </summary>
        public List<Field> Fields
        {
            get { return _Fields; }
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
        #endregion
    }
}
