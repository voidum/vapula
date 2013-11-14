using System.Collections.Generic;
using System.Xml.Linq;

namespace TCM.I18N
{
    public class Translator
    {
        private Dictionary<string, string> 
            _Dict = new Dictionary<string, string>();

        public string this[string key]
        {
            get
            {
                if (!_Dict.ContainsKey(key)) return null;
                return _Dict[key];
            }
            set 
            {
                if (!_Dict.ContainsKey(key)) return;
                _Dict[key] = value;
            }
        }

        public bool Load(string file)
        {
            try
            {
                XDocument xdoc = XDocument.Load(file);
                XElement xeroot = xdoc.Element("root");
                foreach (XElement xe in xeroot.Elements("item"))
                    _Dict.Add(xe.Attribute("key").Value, xe.Value);
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
