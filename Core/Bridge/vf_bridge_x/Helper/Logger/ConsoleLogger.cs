using Sartrey;
using System;

namespace Vapula.Helper
{
    /// <summary>
    /// logger by console
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void WriteLog(params object[] values)
        {
            string log = string.Join(" ; ", values);
            Console.WriteLine("Vapula:" + log);
        }

        public void ClearLog() 
        {
            Console.Clear();
        }
    }
}
