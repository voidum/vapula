using System;
using System.Collections.Generic;

namespace Vapula
{
    /// <summary>
    /// 通过键值对存储的附加数据字典
    /// </summary>
    public class Tag : IDisposable
    {
        private Dictionary<string, object> _Data 
            = new Dictionary<string, object>();

        /// <summary>
        /// 获取或设置附加数据
        /// </summary>
        public object this[string key]
        {
            get
            {
                if (!_Data.ContainsKey(key))
                    return null;
                else
                    return _Data[key];
            }
            set
            {
                if (_Data.ContainsKey(key))
                    _Data[key] = value;
                else
                    _Data.Add(key, value);
            }
        }

        public void Dispose()
        {
            _Data.Clear();
        }
    }
}
