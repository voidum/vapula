using System.Xml.Linq;

namespace Vapula.xHost.CLR
{
    public class UIContext
    {
        private string _Dir;
        private string _Path;
        private string _Clsid;

        public string Dir 
        {
            get { return _Dir; } 
        }

        public string Path
        {
            get { return _Path; }
        }

        public string Clsid
        {
            get { return _Clsid; }
        }

        public static UIContext Parse(string data)
        {
            XElement xml = XElement.Parse(data);
            UIContext ctx = new UIContext();
            ctx._Dir = xml.Element("dir").Value;
            ctx._Path = xml.Element("path").Value;
            ctx._Clsid = xml.Element("clsid").Value;
            return ctx;
        }
    }
}
