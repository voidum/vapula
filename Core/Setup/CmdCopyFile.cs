using System.IO;

namespace TCM.Setup
{
    public class CmdCopyFile : Command
    {
        public override bool Execute()
        {
            string src = (string)_Params[0];
            string dst = (string)_Params[1];
            if (!File.Exists(src))
            {
                Log(string.Format("文件{0}不存在", src));
                return false;
            }
            else
            {
                File.Copy(src, dst, true);
                Log(string.Format("复制文件{0}", src));
                return true;
            }
        }
    }
}
