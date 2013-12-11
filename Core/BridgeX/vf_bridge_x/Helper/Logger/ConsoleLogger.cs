using System;

namespace TCM.Helper
{
    /// <summary>
    /// 控制台日志器
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void WriteLog(LogType type, params object[] values)
        {
            string log = string.Join(" ; ", values);
            Console.WriteLine(type.ToString() + ":" + log);
        }

        public void ClearLog() 
        {
        }
    }
}
