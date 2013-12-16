using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vapula.Dispatcher
{
    public abstract class Command
    {
        protected HostType _Host;

        public abstract bool Execute();
    }
}
