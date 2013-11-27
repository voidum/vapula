using System.IO;

namespace TCM.Setup
{
    public class CmdNewDir : Command
    {
        public override bool Execute()
        {
            string dir = (string)_Params[0];
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                Log("创建目录" + dir);
            }
            return true;
        }
    }
}
