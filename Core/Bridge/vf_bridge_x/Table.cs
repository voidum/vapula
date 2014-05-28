using System.Collections.Generic;
using System.Xml.Linq;

namespace Vapula
{
    /// <summary>
    /// easy and thread-safe {string:object} collection
    /// </summary>
    public class Table<T>
    {
        private Dictionary<string, T> _Data
            = new Dictionary<string, T>();
        private readonly object _SyncRoot
            = new object();

        public Dictionary<string, T> Data
        {
            get { return _Data; }
        }

        public T this[string key]
        {
            get 
            {
                T result = default(T);
                lock(_SyncRoot) {
                    if (_Data.ContainsKey(key))
                        result = _Data[key];
                }
                return result;
            }
            set
            {
                lock(_SyncRoot) {
                    if (_Data.ContainsKey(key))
                        _Data[key] = value;
                    else
                        _Data.Add(key, value);
                }
            }
        }

        public Table()
        {
        }
        
        public void Clear()
        {
            _Data.Clear();
        }

        public XElement ToXML(string table, string row, string key)
        {
            XElement xml = new XElement(table);
            foreach (var kvp in _Data)
                xml.Add(new XElement(row, 
                    new XAttribute(key, kvp.Key),
                    kvp.Value));
            return xml;
        }
    }
}
