using System.Diagnostics;
using Vapula.Runtime;

namespace Vapula.Dispatcher
{
    public enum xHostType
    {
        CLR,
        Web
    }

    public class xHost
    {
        private xHostType _Type;
        private Process _Process;
        private Pipe _Pipe;
    }
}
