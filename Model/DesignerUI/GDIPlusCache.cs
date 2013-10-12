using System.Collections.Generic;
using System.Drawing;
using System;

namespace TCM.Model.Designer
{
    /// <summary>
    /// GDI+对象缓存集合
    /// </summary>
    public class GDIPlusCache
    {
        private Dictionary<string, object> _Data
            = new Dictionary<string, object>();

        /// <summary>
        /// 获取缓存的对象数量
        /// </summary>
        public int Count
        {
            get { return _Data.Count; }
        }

        /// <summary>
        /// 缓存Pen对象
        /// </summary>
        public bool CachePen(string key, Pen pen)
        {
            string rkey = "Pen_" + key;
            if (_Data.ContainsKey(rkey)) 
                return false;
            _Data.Add(rkey, pen);
            return true;
        }

        /// <summary>
        /// 缓存Brush对象
        /// </summary>
        public bool CacheBrush(string key, Brush brush)
        {
            string rkey = "Brush_" + key;
            if (_Data.ContainsKey(rkey))
                return false;
            _Data.Add(rkey, brush);
            return true;
        }

        /// <summary>
        /// 缓存Font对象
        /// </summary>
        public bool CacheFont(string key, Font font)
        {
            string rkey = "Font_" + key;
            if (_Data.ContainsKey(rkey))
                return false;
            _Data.Add(rkey, font);
            return true;
        }

        /// <summary>
        /// 获取Pen
        /// </summary>
        public Pen GetPen(string key)
        {
            string rkey = "Pen_" + key;
            if (!_Data.ContainsKey(rkey))
                return null;
            Pen pen = _Data[rkey] as Pen;
            return pen;
        }

        /// <summary>
        /// 获取Brush
        /// </summary>
        public Brush GetBrush(string key)
        {
            string rkey = "Brush_" + key;
            if (!_Data.ContainsKey(rkey))
                return null;
            Brush brush = _Data[rkey] as Brush;
            return brush;
        }

        /// <summary>
        /// 获取Font
        /// </summary>
        public Font GetFont(string key)
        {
            string rkey = "Font_" + key;
            if (!_Data.ContainsKey(rkey))
                return null;
            Font font = _Data[rkey] as Font;
            return font;
        }

        /// <summary>
        /// 清理缓存
        /// </summary>
        public void Clear()
        {
            foreach (var e in _Data)
            {
                if (e.Value is Pen)
                    (e.Value as Pen).Dispose();
                if (e.Value is Brush)
                    (e.Value as Brush).Dispose();
                if (e.Value is Font)
                    (e.Value as Font).Dispose();
            }
        }
    }
}
