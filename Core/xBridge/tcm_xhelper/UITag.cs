using System.Collections.Generic;

namespace TCM
{
    public class UITag
    {
        private int _Index = -1;
        private Dictionary<string, object> _Tags 
            = new Dictionary<string, object>();

        /// <summary>
        /// 构造UI标签
        /// </summary>
        public UITag() { }

        /// <summary>
        /// 获取或设置附加对象
        /// </summary>
        public object this[string key]
        {
            get
            {
                if (_Tags.ContainsKey(key)) return _Tags[key];
                else return null;
            }
            set
            {
                if (_Tags.ContainsKey(key)) _Tags[key] = value;
                else _Tags.Add(key, value);
            }
        }

        /// <summary>
        /// 获取或设置UI索引
        /// </summary>
        public int Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
                if (value < 0) _Tags.Clear();
            }
        }

        /// <summary>
        /// 获取附加对象数量
        /// </summary>
        public int Count
        {
            get { return _Tags.Count; }
        }
    }
}
