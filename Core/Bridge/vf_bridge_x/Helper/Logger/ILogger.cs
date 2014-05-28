namespace Vapula.Helper
{
    /// <summary>
    /// log type
    /// </summary>
    public enum LogType
    {
        Critical,
        Error,
        Warning,
        Event,
        Debug
    }

    /// <summary>
    /// support log
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// write log
        /// </summary>
        void WriteLog(LogType type, params object[] values);
    
        /// <summary>
        /// clear log
        /// </summary>
        void ClearLog();
    }
}
