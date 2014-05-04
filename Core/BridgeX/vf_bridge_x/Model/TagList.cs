using System.Collections.Concurrent;
using System.Xml.Linq;

namespace Vapula.Model
{
    /// <summary>
    /// easy and thread-safe {string:object} collection
    /// </summary>
    public class TagList
    {
        private ConcurrentDictionary<string, object> _Tags
            = new ConcurrentDictionary<string, object>();

        public ConcurrentDictionary<string, object> Tags
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
                    _Tags.TryAdd(key, value);
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
                    kvp.Value == null ? "" : kvp.Value.ToString());
                xml.Add(xe);
            }
            return xml;
        }
    }
}
