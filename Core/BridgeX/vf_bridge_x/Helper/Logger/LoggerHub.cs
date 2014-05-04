using System.Collections.Generic;

namespace Vapula.Helper
{
    public class LoggerHub
    {
        private static LoggerHub _Instance 
            = null;
        private static readonly object _SyncCtor 
            = new object();

        public static LoggerHub Instance
        {
            get 
            {
                if (_Instance == null)
                {
                    lock (_SyncCtor) 
                    {
                        _Instance = new LoggerHub();
                    }
                }
                return _Instance;
            }
        }

        private Dictionary<string, ILogger> _Loggers 
            = new Dictionary<string, ILogger>();

        public Dictionary<string, ILogger> Loggers
        {
            get { return _Loggers; }
        }

        public LoggerHub() 
        {
        }

        public ILogger this[string key]
        {
            get
            {
                if (_Loggers.ContainsKey(key))
                    return _Loggers[key];
                return null;
            }
            set
            {
                if (_Loggers.ContainsKey(key))
                {
                    _Loggers[key] = value;
                    if (value == null)
                        _Loggers.Remove(key);
                }
                else
                    _Loggers.Add(key, value);
            }
        }
    }
}
