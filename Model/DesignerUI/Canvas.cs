using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 画布基类
    /// </summary>
    [ToolboxItem(false)]
    public class Canvas : Panel
    {
        #region 字段
        /// <summary>
        /// 工作区尺寸
        /// </summary>
        protected Size _WorkSize;

        /// <summary>
        /// 工作区边距
        /// </summary>
        protected int _Padding;

        /// <summary>
        /// 最小宽度
        /// </summary>
        protected int _MinWidth;

        /// <summary>
        /// 最小高度
        /// </summary>
        protected int _MinHeight;

        /// <summary>
        /// GDI+对象缓存
        /// </summary>
        protected GDIPlusCache _Cache = new GDIPlusCache();

        /// <summary>
        /// 图元集合
        /// </summary>
        protected List<Entity> _Entities = new List<Entity>();
        #endregion

        #region 属性
        /// <summary>
        /// 工作区
        /// </summary>
        public Rectangle WorkRect
        {
            get { return new Rectangle(_Padding, _Padding, _WorkSize.Width, WorkSize.Height); }
        }

        /// <summary>
        /// 工作区尺寸
        /// </summary>
        public Size WorkSize
        {
            get { return _WorkSize; }
            set
            {
                _WorkSize.Width = value.Width > _MinWidth ? value.Width : _MinWidth;
                _WorkSize.Height = value.Height > _MinHeight ? value.Height : _MinHeight;
                int padding = _Padding * 2;
                Size = new Size(_WorkSize.Width + padding, _WorkSize.Height + padding);
            }
        }
        #endregion

        #region 构造
        /// <summary>
        /// 构造画布基类
        /// </summary>
        public Canvas(int padding = 50, int minw = 600, int minh = 400)
        {
            _Padding = padding;
            _MinWidth = minw;
            _MinHeight = minh;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            ConfigCache();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取新的标识
        /// </summary>
        public int GetNewId()
        {
            List<int> ids = new List<int>();
            foreach (Entity e in _Entities)
                ids.Add(e.Id);
            ids.Sort();
            for (int i = 0; i < ids.Count; i++)
                if (ids[i] != i) return i;
            return ids.Count;
        }

        /// <summary>
        /// 配置缓存
        /// </summary>
        public virtual void ConfigCache() 
        {
        }

        /// <summary>
        /// 释放画布对象
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            _Cache.Clear();
            base.Dispose(disposing);
        }
        #endregion
    }
}
