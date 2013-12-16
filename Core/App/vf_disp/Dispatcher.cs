using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vapula.Dispatcher
{
    class Dispatcher
    {
        private List<xHost> _xHosts 
            = new List<xHost>();

        public bool Execute(Command command) 
        {
            return false;
        }
    }
}
