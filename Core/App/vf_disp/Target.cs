using System.Xml.Linq;

namespace Vapula.Dispatcher
{
    public class Target
    {
        private string _Path;
        private HostType _Type;

        public string Path
        {
            get { return _Path; }
        }

        public HostType Type
        {
            get { return _Type; }
        }

        public static Target Parse(string data)
        {
            XElement xml = XElement.Parse(data);
            Target tar = new Target();
            tar._Type = 
                Host.GetHostType(xml.Element("type").Value);
            tar._Path = xml.Element("path").Value;
            return tar;
        }
    }
}
