using System.Drawing;
using System.Windows.Forms;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 图元
    /// </summary>
    public abstract class Entity
    {
        #region 字段
        protected int _Id = -1;
        protected Point _Location = new Point(0, 0);

        protected bool _IsHovered = false;
        protected bool _IsSelected = false;

        protected int _Style = 0;

        protected GDIPlusCache _Cache = new GDIPlusCache();
        protected Canvas _Canvas;
        #endregion

        #region 属性
        /// <summary>
        /// 获取图元标识
        /// </summary>
        public int Id
        {
            get { return _Id; }
        }

        /// <summary>
        /// 获取或设置对象是否被悬停
        /// </summary>
        public virtual bool IsHovered
        {
            get { return _IsHovered; }
            set 
            {
                if (value != _IsHovered)
                {
                    _IsHovered = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置对象是否被选中
        /// </summary>
        public virtual bool IsSelected
        {
            get { return _IsSelected; }
            set 
            {
                if (value != _IsSelected)
                {
                    _IsSelected = value; 
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置图元位置
        /// </summary>
        public virtual Point Location
        {
            get { return _Location; }
            set
            {
                if (value != _Location) 
                {
                    _Location.X = value.X;
                    _Location.Y = value.Y;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置图元风格
        /// </summary>
        public virtual int Style
        {
            get { return _Style; }
            set 
            {
                if (value != _Style) 
                {
                    _Style = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置图元所在画布
        /// </summary>
        public virtual Canvas Canvas
        {
            get { return _Canvas; }
            set 
            {
                if (value != _Canvas) 
                {
                    _Canvas = value;
                }
            }
        }
        #endregion

        #region 构造
        public Entity()
        {
            ConfigCache();
        }

        /// <summary>
        /// 配置缓存
        /// </summary>
        public virtual void ConfigCache() { }
        #endregion

        #region 方法
        /// <summary>
        /// 在指定Graphics对象上绘制图元
        /// </summary>
        public abstract void Paint(Graphics g);

        /// <summary>
        /// 测试指定位置是否命中
        /// </summary>
        public abstract bool IsHit(Point p);

        /// <summary>
        /// 按照指定矢量移动图元
        /// </summary>
        public abstract void MoveAs(Point v);

        /// <summary>
        /// 通知重绘
        /// </summary>
        public abstract void Invalidate();

        /// <summary>
        /// 触发图元的鼠标按下
        /// </summary>
        public abstract void MouseDown(MouseEventArgs e);

        /// <summary>
        /// 触发图元的鼠标抬起
        /// </summary>
        public abstract void MouseUp(MouseEventArgs e);

        /// <summary>
        /// 触发图元的鼠标移动
        /// </summary>
        public abstract void MouseMove(MouseEventArgs e);
        #endregion
    }
}
