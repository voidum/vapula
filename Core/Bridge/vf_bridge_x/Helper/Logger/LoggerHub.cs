using System.Collections.Generic;

namespace Vapula.Helper
{
    public class LoggerHub
    {
        private static LoggerHub _Instance 
            = null;
        private static readonly object _CtorLock
            = new object();

        public static LoggerHub Instance
        {
            get 
            {
                if (_Instance == null) {
                    lock (_CtorLock) {
                        if(_Instance == null)
                            _Instance = new LoggerHub();
                    }
                }
                return _Instance;
            }
        }

        private Table<ILogger> _Loggers
            = new Table<ILogger>();

        public Table<ILogger> Loggers
        {
            get { return _Loggers; }
        }

        public LoggerHub() 
        {
        }

        public ILogger this[string key]
        {
            get { return _Loggers[key]; }
            set { _Loggers[key] = value; }
        }
    }
}
