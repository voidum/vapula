using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 画布基类
    /// </summary>
    [ToolboxItem(false)]
    public class Canvas : Panel, ISyncable
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
        /// ISyncable要求的同步目标
        /// </summary>
        protected ISyncable _SyncTarget = null;
        #endregion

        #region 事件
        /// <summary>
        /// 选择对象变更
        /// </summary>
        public event Action SelectedChanged;

        protected virtual void OnSelectedChanged()
        {
            if(SelectedChanged != null)
                BeginInvoke(SelectedChanged);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 工作区
        /// </summary>
        [Browsable(false)]
        public Rectangle WorkRect
        {
            get { return new Rectangle(_Padding, _Padding, _WorkSize.Width, WorkSize.Height); }
        }

        /// <summary>
        /// 工作区尺寸
        /// </summary>
        [Category("布局")]
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

        /// <summary>
        /// 同步目标
        /// </summary>
        public ISyncable SyncTarget
        {
            get { return _SyncTarget; }
            set { _SyncTarget = value; }
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
            ConfigEntityTable();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 配置缓存
        /// </summary>
        public virtual void ConfigCache() 
        {
        }

        /// <summary>
        /// 配置图元表
        /// </summary>
        public virtual void ConfigEntityTable()
        {
        }

        /// <summary>
        /// 获取图元新标识
        /// </summary>
        public virtual int GetNewId(Entity entity)
        {
            return -1;
        }

        /// <summary>
        /// 根据协定将提供的数据同步到画布
        /// </summary>
        public virtual void Sync(string cmd, object attach) 
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
