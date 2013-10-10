using System.Windows.Forms;
using System.Xml.Linq;
using TCM.Models;
using TCM.Toolkit;

namespace TCM.ComPublisher
{
    public class AppData
    {
        private static AppConfig _Config = null;
        public static AppConfig Config
        {
            get 
            {
                if (_Config == null) _Config = new AppConfig(); ;
                return _Config;
            }
        }

        private static Component _Component = null;
        public static Component Component
        {
            get { return _Component; }
            set
            {
                if (value == null && _Component != null) _Component.Clear();
                _Component = value;
            }
        }

        public static string PathCfgDst
        {
            get { return Config["DirCfg"] + Component.Id + ".tcm.xml"; }
        }
        public static string PathLibSrc;
        public static string PathLibDst 
        {
            get { return Config["DirBin"] + Component.Id + ".dll"; }
        }

        private static XDocument _XmlComLst = null;
        public static XDocument XmlComLst
        {
            get
            {
                if (_XmlComLst == null)
                {
                    try { _XmlComLst = XDocument.Load(Config["PathLst"]); }
                    catch 
                    {
                        _XmlComLst = new XDocument(
                            new XDeclaration("1.0", "utf-8", "yes"),
                            new XElement("root"));
                        XElement xeroot = _XmlComLst.Element("root");
                    }
                }
                return _XmlComLst;
            }
        }
        public static bool IfExistCom(string index)
        {
            XElement xeroot = XmlComLst.Element("root");
            foreach (XElement xe in xeroot.Elements("component"))
                if (xe.Attribute("id").Value == index) return true; ;
            return false;
        }
    }
}
