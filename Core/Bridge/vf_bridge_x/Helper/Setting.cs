using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Vapula.Helper
{
    public class Setting
    {
        private string _Path;

        private Table<string> _Data
            = new Table<string>();

        /// <summary>
        /// load setting
        /// </summary>
        public bool Load(string file)
        {
            _Path = file;
            if (!File.Exists(file)) return false;
            try
            {
                XDocument xdoc = XDocument.Load(file);
                XElement xe_root = xdoc.Element("setting");
                var xes_item = xe_root.Elements("item");
                foreach (var xe in xes_item)
                    _Data[xe.Attribute("key").Value] = xe.Value;
            }
            catch 
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// save setting
        /// </summary>
        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(_Path))
                return false;
            return Save(_Path);
        }

        /// <summary>
        /// save setting
        /// </summary>
        public bool Save(string file)
        {
            try
            {
                XElement xml = 
                    _Data.ToXML("setting", "item", "key");
                xml.Save(file);
            }
            catch 
            {
                return false; 
            }
            return true;
        }

        /// <summary>
        /// get value by key
        /// </summary>
        public string this[string key]
        {
            get { return _Data[key]; }
            set { _Data[key] = value; }
        }
    }
}
