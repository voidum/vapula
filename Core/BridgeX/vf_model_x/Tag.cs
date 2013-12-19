using System.Xml.Linq;

namespace Vapula.Model
{
    /// <summary>
    /// Vapula附加数据
    /// </summary>
    public class Tag
    {
        private string _Key;
        private string _Value;

        public Tag()
        {
        }

        public Tag(string key, string value)
        {
            _Key = key;
            _Value = value;
        }

        public static Tag Parse(XElement xml)
        {
            Tag tag = new Tag();
            tag._Key = xml.Attribute("key").Value;
            tag._Value = xml.Value;
            return tag;
        }

        public XElement ToXML()
        {
            XElement xml = new XElement("tag",
                new XAttribute("key", _Key), 
                _Value);
            return xml;
        }

        public string Key
        {
            get
            {
                if (_Key == null)
                    return "";
                return _Key;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Key = null;
                else _Key = value;
            }
        }

        public string Value
        {
            get
            {
                if (_Value == null)
                    return "";
                return _Value;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _Value = null;
                else _Value = value;
            }
        }
    }
}
