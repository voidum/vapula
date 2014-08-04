using Sartrey;
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
        private bool _HasProtect;
        private string _ProcessSym;
        private string _RollbackSym;
        private Library _Library;
        private List<Record> _Records
            = new List<Record>();
        private Table<string> _Tags
            = new Table<string>();
        #endregion

        #region Ctor
        public Method() { }
        #endregion

        #region Indexer
        /// <summary>
        /// get record by id
        /// </summary>
        public Record this[int id]
        {
            get
            {
                foreach (var record in Records)
                    if (record.Id == id)
                        return record;
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
            Method method = new Method();
            method._Id = xml.Element("id").Value;
            method._HasProtect = (xml.Element("protect").Value == "true");
            method._ProcessSym = xml.Element("entry").Element("process").Value;
            method._RollbackSym = xml.Element("entry").Element("rollback").Value;
            var xes_tag = xml.Element("tags").Elements("tag");
            foreach (var xe in xes_tag)
                method._Tags[xe.Attribute("key").Value] = xe.Value;
            var xes = xml.Element("dataset").Elements("record");
            foreach (var xe in xes)
            {
                var record = Record.Parse(xe);
                record.Method = method;
                method.Records.Add(record);
            }
            return method;
        }

        /// <summary>
        /// output schema to XML
        /// </summary>
        public XElement ToXML()
        {
            XElement xml = new XElement("method",
                new XElement("dataset"),
                new XElement("id", Id),
                new XElement("protect", _HasProtect ? "true" : "false"),
                new XElement("symbols",
                    new XElement("process", ProcessSym),
                    new XElement("rollback", RollbackSym)),
                _Tags.ToXML("tags|tag|key"));
            foreach (var record in Records)
            {
                var xe = record.ToXML();
                xml.Element("dataset").Add(xe);
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
            foreach(var record in Records)
                record.Clear();
            Records.Clear();
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
        public bool HasProtect 
        {
            get { return _HasProtect; }
            set { _HasProtect = value; }
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
        /// get records
        /// </summary>
        public List<Record> Records
        {
            get { return _Records; }
        }
        
        /// <summary>
        /// get tags
        /// </summary>
        internal Table<string> Tags
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
                return tag;
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
                return tag;
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
