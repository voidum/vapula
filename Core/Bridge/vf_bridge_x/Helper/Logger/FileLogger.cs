using Sartrey;
using System;
using System.IO;

namespace Vapula.Helper
{
    /// <summary>
    /// logger by file
    /// </summary>
    public class FileLogger : ILogger
    {
        private string _LogPath = null;
        public string LogPath
        {
            get
            {
                if (_LogPath == null)
                    _LogPath = Path.Combine(Base.RuntimeDir, "vapula.log");
                return _LogPath;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) 
                    _LogPath = null;
                else 
                    _LogPath = value;
            }
        }

        public void ClearLog()
        {
            if (File.Exists(LogPath)) 
                File.Delete(LogPath);
        }

        /// <summary>
        /// <para>accept 1 or 3 value(s)</para>
        /// <para>3 values as class/method/message</para>
        /// </summary>
        public void WriteLog(params object[] values)
        {
            if (values.Length == 1)
                File.AppendAllText(
                    LogPath,
                    (string)(values[0]) + Environment.NewLine);
            else if (values.Length == 3)
            {
                string content = string.Format("{0}{1}[{2}]{3}::{4}{1}{5}{1}",
                    new object[] {
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Environment.NewLine,
                        values[0], values[1], values[2]
                });
            }
        }
    }
}
