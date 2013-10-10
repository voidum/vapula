using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TCM
{
    public partial class Server
    {
        public bool Config(string file)
        {
            XDocument xdoc = XDocument.Load(file);
            XElement xeroot = xdoc.Element("root");
            //ports
            //static file filter
            //route
            //controller
            //view
            //template
            return false;
        }
    }
}
