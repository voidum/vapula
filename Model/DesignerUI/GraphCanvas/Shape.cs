using System;
using System.Collections.Generic;
using System.Drawing;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 图元：形状
    /// </summary>
    public class Shape : Entity
    {
        #region 字段
        protected Size _Size;
        protected List<Connector> _Connectors = new List<Connector>();
        protected List<string> _Texts = new List<string>();
        #endregion

        #region 属性
        /// <summary>
        /// 获取形状的所有连结点
        /// </summary>
        public List<Connector> Connectors
        {
            get { return _Connectors; }
        }

        /// <summary>
        /// 获取边界矩形
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(X - Width / 2, Y - Height / 2, Width, Height); }
        }

        /// <summary>
        /// 获取或设置左边缘的X坐标
        /// </summary>
        public int Left
        {
            get { return _Location.X - _Size.Width / 2; }
            set
            {
                if (value == Left) return;
                MoveAs(new Point(value - Left, 0));
            }
        }

        /// <summary>
        /// 获取或设置右边缘的X坐标
        /// </summary>
        public int Right
        {
            get { return _Location.X + _Size.Width / 2; }
            set
            {
                if (value == Right) return;
                MoveAs(new Point(value - Right, 0));
            }
        }

        /// <summary>
        /// 获取或设置上边缘的Y坐标
        /// </summary>
        public int Top
        {
            get { return _Location.Y - _Size.Height / 2; }
            set
            {
                if (value == Top) return;
                MoveAs(new Point(0, value - Top));
            }
        }

        /// <summary>
        /// 获取或设置下边缘的Y坐标
        /// </summary>
        public int Bottom
        {
            get { return _Location.Y + _Size.Height / 2; }
            set
            {
                if (value == Bottom) return;
                MoveAs(new Point(0, value - Bottom));
            }
        }

        /// <summary>
        /// 获取或设置宽度
        /// </summary>
        public int Width
        {
            get { return _Size.Width; }
            set
            {
                if (value == _Size.Width) return;
                Resize(new Size(value, _Size.Height));
            }
        }

        /// <summary>
        /// 获取或设置高度
        /// </summary>
        public int Height
        {
            get { return _Size.Height; }
            set
            {
                if (value == _Size.Height) return;
                Resize(new Size(_Size.Width, value));
            }
        }

        /// <summary>
        /// 获取或设置中心的X坐标
        /// </summary>
        public int X
        {
            get { return _Location.X; }
            set
            {
                if (value == _Location.X) return;
                MoveAs(new Point(value - X, 0)); ;
            }
        }

        /// <summary>
        /// 获取或设置中心的Y坐标
        /// </summary>
        public int Y
        {
            get { return _Location.Y; }
            set
            {
                if (value == _Location.Y)
                    return;
                MoveAs(new Point(0, value - Y)); ;
            }
        }

        /// <summary>
        /// 获取或设置形状中心点位置
        /// </summary>
        public override Point Location
        {
            get { return base.Location; }
            set
            {
                if (value == _Location)
                    return;
                _Location.X = value.X;
                _Location.Y = value.Y;
                MoveAs(new Point(
                    value.X - _Location.X, 
                    value.Y - _Location.Y));
            }
        }

        public override Canvas Canvas
        {
            get { return _Canvas; }
            set
            {
                base.Canvas = value;
                foreach (Connector cop in _Connectors)
                    cop.Canvas = value;
            }
        }
        #endregion

        #region 构造
        public Shape()
        {
            _Location = new Point(0, 0);
        }
        #endregion

        #region 方法
        /// <summary>
        /// <para>重设尺寸</para>
        /// </summary>
        public virtual void Resize(Size size)
        {
            _Size.Width = size.Width;
            _Size.Height = size.Height;
            Invalidate();
        }

        /// <summary>
        /// 获得命中的结点
        /// </summary>
        public Connector GetHitConnector(Point p)
        {
            if (_IsSelected) return null; //todo: diff hit logic
            foreach (Connector cop in _Connectors)
            {
                if (cop.IsHit(p))
                {
                    cop.IsHovered = true;
                    return cop;
                }
                else cop.IsHovered = false;
            }
            return null;
        }

        /// <summary>
        /// 分析目标结点的关联源是否与自身发生过关联
        /// </summary>
        public bool IfHasAttached(Connector c)
        {
            /*
            Connection con = c.Container as Connection;
            Connector coptmp = con.GetAnotherConnector(c).AttachedTo;
            ShapeBase shp = null;
            if (coptmp != null) shp = coptmp.Container as ShapeBase;
            foreach (Connector cop in _Connectors) 
            {
                foreach (Connector cop2 in cop.Attached)
                {
                    Connection con2 = cop2.Container as Connection;
                    if ((cop2 == con2.From) ^ (c == con.From)) continue;
                    coptmp = con2.GetAnotherConnector(cop2).AttachedTo;
                    if (coptmp == null) continue;
                    ShapeBase shp2 = coptmp.Container as ShapeBase;
                    if (shp2 == shp) return true;
                }
            }
             */
            return false;
        }
        #endregion

        #region 重写
        public override bool IsHit(Point p)
        {
            throw new Exception("不要调用ShapeBase的命中测试。");
        }

        public override void MoveAs(Point v)
        {
            _Location.X += v.X;
            _Location.Y += v.Y;
            foreach (Connector cop in _Connectors)
                cop.MoveAs(v);
            Invalidate();
        }

        public override void MoveTo(Point p)
        {
            Point v = new Point(X - p.X, Y - p.Y);
            _Location.X = p.X;
            _Location.Y = p.Y;
            foreach (Connector cop in _Connectors) 
                cop.MoveAs(v);
            Invalidate();
        }

        public override void Invalidate()
        {
            if (_Canvas != null)
                _Canvas.Invalidate();
        }

        public override void Invalidate(Rectangle rect)
        {
            throw new NotImplementedException();
        }

        public override void OnPaint(Graphics g)
        {
            foreach (Connector cop in _Connectors)
            {
                if (cop.IsHovered)
                    cop.OnPaint(g);
            }
        }

        public override void Dispose()
        {
            foreach (Connector cop in _Connectors)
                cop.Dispose();
            base.Dispose();
        }
        #endregion
    }
}