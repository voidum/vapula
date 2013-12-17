using System.Diagnostics;
using Vapula.Runtime;
using System.IO;

namespace Vapula.Dispatcher
{
    public enum HostType
    {
        Unknown = 0,
        Host = 1,
        xHostCLR = 2,
        xHostWeb = 3
    }

    public class Host
    {
        public static HostType GetHostType(string type)
        {
            switch (type)
            {
                case "xhost-clr":
                    return HostType.xHostCLR;
                case "xhost-web":
                    return HostType.xHostWeb;
                case "host":
                    return HostType.Host;
                default:
                    return HostType.Unknown;
            }
        }

        private HostType _Type;
        private Process _Process;
        private Pipe _Pipe;

        public HostType Type
        {
            get { return _Type; }
        }

        public Process Process
        {
            get { return _Process; }
        }

        public Pipe Pipe
        {
            get { return _Pipe; }
        }

        public Host(HostType type)
        {
            _Type = type;
            _Pipe = new Pipe();
            _Pipe.Listen();
            _Process = Process.Start(
                Path.Combine(Base.AppDir, "vf_xhost.clr.exe"),
                _Pipe.Id);
        }

        public bool Load(Target target)
        {
            _Pipe.Write(target.Path);
            return true;
        }
    }
}
