using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace TCM.Helper
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public class AppConfig
    {
        private string _ConfigPath;

        private Dictionary<string, string> _Configs 
            = new Dictionary<string, string>();

        /// <summary>
        /// 加载配置
        /// </summary>
        public bool Load(string file)
        {
            _ConfigPath = file;
            if (!File.Exists(file)) return false;
            try
            {
                XDocument xmldoc = XDocument.Load(file);
                XElement xeroot = xmldoc.Element("root");
                foreach (XElement xe in xeroot.Elements("item"))
                    _Configs.Add(xe.Attribute("key").Value, xe.Value);
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// 保存配置到默认位置
        /// </summary>
        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(_ConfigPath)) return false;
            return Save(_ConfigPath);
        }

        /// <summary>
        /// 保存配置到指定位置
        /// </summary>
        public bool Save(string file)
        {
            try
            {
                XDocument xmldoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("root"));
                XElement xeroot = xmldoc.Element("root");
                foreach (KeyValuePair<string, string> kvp in _Configs)
                {
                    xeroot.Add(new XElement("item",
                        new XAttribute("key", kvp.Key),
                        new XCData(kvp.Value)));
                }
                xmldoc.Save(file);
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// 获取或设置指定键对应的配置值
        /// </summary>
        public string this[string key]
        {
            get
            {
                foreach (KeyValuePair<string, string> kvp in _Configs)
                    if (kvp.Key == key) return kvp.Value;
                return null;
            }
            set
            {
                if (_Configs.ContainsKey(key)) _Configs[key] = value;
                else _Configs.Add(key, value);
            }
        }
    }
}
