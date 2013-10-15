namespace TCM.Helper
{
    /// <summary>
    /// 日志类型
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
    /// 支持日志的接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        void WriteLog(LogType type, params object[] values);
    
        /// <summary>
        /// 清理日志
        /// </summary>
        void ClearLog();
    }


}
