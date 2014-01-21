using System;
using System.IO;

namespace Vapula.Helper
{
    /// <summary>
    /// 文件日志器
    /// </summary>
    public class FileLogger : ILogger
    {
        private string _LogPath = null;
        public string LogPath
        {
            get
            {
                if (_LogPath == null)
                    _LogPath = 
                        Base.RuntimeDir + "\\vapula.log";
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
        /// <para>输入1个参数或3个参数</para>
        /// <para>输入3个参数请按照 类、方法、消息 组合内容</para>
        /// </summary>
        public void WriteLog(LogType type, params object[] values)
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
                        type,
                        values[0],
                        values[1],
                        values[2]
                });
            }
        }
    }
}
