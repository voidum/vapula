using Sartrey;
using System.Xml.Linq;

namespace Vapula.Model
{
    /// <summary>
    /// schema for Vapula field
    /// </summary>
    public class Field
    {
        #region Fields
        private int _Id;
        private AccessMode _Access;
        private Method _Method;
        private Table<string> _Tags
            = new Table<string>();
        private object _Attach
            = null;
        #endregion

        #region Ctor
        public Field() { }
        #endregion

        #region Serialization
        /// <summary>
        /// parse schema from XML
        /// </summary>
        public static Field Parse(XElement xml)
        {
            Field field = new Field();
            field.Id = int.Parse(xml.Attribute("id").Value);
            field.Access = (AccessMode)int.Parse(xml.Element("access").Value);
            var xes_tag = xml.Element("tags").Elements("tag");
            foreach (var xe in xes_tag)
                field._Tags[xe.Attribute("key").Value] = xe.Value;
            return field;
        }

        /// <summary>
        /// output schema to XML
        /// </summary>
        public XElement ToXML()
        {
            XElement xml = new XElement("field",
                new XAttribute("id", Id),
                new XElement("access", (int)Access),
                _Tags.ToXML("tags", "tag", "key"));
            return xml;
        }
        #endregion

        #region Collection
        /// <summary>
        /// clear schema
        /// </summary>
        public void Clear()
        {
            Tags.Clear();
        }
        #endregion

        #region Properties
        /// <summary>
        /// get or set id
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// get or set access mode
        /// </summary>
        public AccessMode Access
        {
            get { return _Access; }
            set { _Access = value; }
        }

        /// <summary>
        /// get or set method
        /// </summary>
        public Method Method
        {
            get { return _Method; }
            set { _Method = value; }
        }

        /// <summary>
        /// get or set tags
        /// </summary>
        public Table<string> Tags
        {
            get { return _Tags; }
        }

        public object Attach 
        {
            get { return _Attach; }
            set { _Attach = value; }
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
