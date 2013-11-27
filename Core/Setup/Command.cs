using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TCM.Setup
{
    public enum CommandType
    {
        NewDir = 0,
        CopyFile = 1
    }

    public abstract class Command
    {
        protected CommandType _Type;
        protected List<object> _Params 
            = new List<object>();

        public static Action<string> Log = null;

        public static Command Parse(XElement xml)
        {
            CommandType type = 
                (CommandType)(int.Parse(
                    xml.Attribute("type").Value));
            Command cmd = null;
            switch (type)
            {
                case CommandType.NewDir:
                    cmd = new CmdNewDir();
                    cmd._Params.Add(xml.Element("dir").Value);
                    break;
                case CommandType.CopyFile:
                    cmd = new CmdCopyFile();
                    cmd._Params.Add(xml.Element("src").Value);
                    cmd._Params.Add(xml.Element("dst").Value);
                    break;
                default:
                    break;
            }
            return cmd;
        }

        public abstract bool Execute();
    }
}
