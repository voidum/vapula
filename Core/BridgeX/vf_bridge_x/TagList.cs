using System.Collections.Generic;
using System.Xml.Linq;

namespace Vapula.Model
{
    /// <summary>
    /// Vapula附加数据
    /// </summary>
    public class TagList
    {
        private Dictionary<string, object> _Tags 
            = new Dictionary<string,object>();

        public Dictionary<string, object> Tags
        {
            get { return _Tags; }
        }

        public object this[string key]
        {
            get 
            {
                if (_Tags.ContainsKey(key))
                    return _Tags[key];
                return null;
            }
            set
            {
                if (_Tags.ContainsKey(key))
                    _Tags[key] = value;
                else
                    _Tags.Add(key, value);
            }
        }

        public TagList()
        {
        }

        public static TagList Parse(XElement xml)
        {
            TagList tags = new TagList();
            foreach(var xe in xml.Elements("tag"))
                tags[xe.Attribute("key").Value] 
                    = xe.Value;
            return tags;
        }

        public void Clear()
        {
            _Tags.Clear();
        }

        public XElement ToXML()
        {
            XElement xml = new XElement("tags");
            foreach(var kvp in _Tags)
            {
                var xe = new XElement("tag",
                    new XAttribute("key", kvp.Key),
                    kvp.Value.ToString());
            }
            return xml;
        }
    }
}
