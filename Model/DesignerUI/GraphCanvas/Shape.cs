using System;
using System.Collections.Generic;
using System.Drawing;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 形状基类
    /// </summary>
    public class Shape : Entity
    {
        #region 字段
        protected Size _Size;
        protected List<Connector> _Connectors = new List<Connector>();
        protected List<string> _Texts = new List<string>();
        private Size _HalfSize;
        #endregion

        #region 属性
        public List<Connector> Connectors
        {
            get { return _Connectors; }
        }

        public List<string> Texts
        {
            get { return _Texts; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(X - Width / 2, Y - Height / 2, Width, Height); }
        }

        public int Left
        {
            get { return _Location.X - _HalfSize.Width; }
            set
            {
                if (value == Left) return;
                MoveAs(new Point(value - Left, 0));
            }
        }

        public int Right
        {
            get { return _Location.X + _HalfSize.Width; }
            set
            {
                if (value == Right) return;
                MoveAs(new Point(value - Right, 0));
            }
        }

        public int Top
        {
            get { return _Location.Y - _HalfSize.Height; }
            set
            {
                if (value == Top) return;
                MoveAs(new Point(0, value - Top));
            }
        }

        public int Bottom
        {
            get { return _Location.Y + _HalfSize.Height; }
            set
            {
                if (value == Bottom) return;
                MoveAs(new Point(0, value - Bottom));
            }
        }

        public int Width
        {
            get { return _Size.Width; }
            set
            {
                if (value == _Size.Width) return;
                Resize(new Size(value, _Size.Height));
            }
        }

        public int Height
        {
            get { return _Size.Height; }
            set
            {
                if (value == _Size.Height) return;
                Resize(new Size(_Size.Width, value));
            }
        }

        public int X
        {
            get { return _Location.X; }
            set
            {
                if (value == _Location.X) return;
                MoveAs(new Point(value - X, 0)); ;
            }
        }

        public int Y
        {
            get { return _Location.Y; }
            set
            {
                if (value == _Location.Y) return;
                MoveAs(new Point(value - Y, 0)); ;
            }
        }

        public override Canvas Canvas
        {
            get { return _Canvas; }
            set
            {
                if (value == _Canvas) return;
                _Canvas = value;
                _Id = _Canvas.GetNewId();
                foreach (Connector cop in _Connectors) cop.Canvas = value;
            }
        }
        #endregion

        #region 构造
        public Shape()
        {
            _Location = new Point(0, 0);
            _Texts.Add("编号：" + _Id.ToString());
        }
        #endregion

        #region 方法

        /// <summary>
        /// <para>重设尺寸，仅实现基本逻辑，不刷新</para>
        /// <para>需要进一步实现其关联对象的变化及刷新</para>
        /// </summary>
        public virtual void Resize(Size newsize)
        {
            _Size.Width = newsize.Width;
            _Size.Height = newsize.Height;
            _HalfSize.Width = newsize.Width / 2;
            _HalfSize.Height = newsize.Height / 2;
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
            foreach (Connector cop in _Connectors) cop.MoveAs(v);
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
            if (_Canvas != null) _Canvas.Invalidate();
        }

        public override void Invalidate(Rectangle rect)
        {
            throw new NotImplementedException();
        }

        public override void OnPaint(Graphics g)
        {
            foreach (Connector cop in _Connectors)
            {
                if (cop.IsHovered) cop.OnPaint(g);
            }
        }
        #endregion
    }
}