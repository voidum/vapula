using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 图元：连接线
    /// </summary>
    public class Connection : Entity
    {
        #region 字段
        protected Connector _From = null;
        protected Connector _To = null;
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置来源连结点
        /// </summary>
        public Connector From
        {
            get { return _From; }
        }

        /// <summary>
        /// 获取或设置目标连结点
        /// </summary>
        public Connector To
        {
            get { return _To; }
            set { _To = value; }
        }

        /// <summary>
        /// 获取连接线的区域宽度
        /// </summary>
        public int Width
        {
            get { return Math.Abs(_From.Location.X - _To.Location.X); }
        }

        /// <summary>
        /// 获取连接线的区域高度
        /// </summary>
        public int Height
        {
            get { return Math.Abs(_From.Location.Y - _To.Location.Y); }
        }

        /// <summary>
        /// 获取连接线中点X坐标
        /// </summary>
        public int X
        {
            get { return (_From.Location.X < _To.Location.X ? _From.Location.X : _To.Location.X) + Width / 2; }
        }

        /// <summary>
        /// 获取连接线中点Y坐标
        /// </summary>
        public int Y
        {
            get { return (_From.Location.Y < _To.Location.Y ? _From.Location.Y : _To.Location.Y) + Height / 2; }
        }

        /// <summary>
        /// 获取连接线的长度指标
        /// </summary>
        public float Length
        {
            get
            {
                float result = Width * Width + Height * Height;
                return result;
            }
        }

        /// <summary>
        /// 获取或设置画布
        /// </summary>
        public override Canvas Canvas
        {
            get { return _Canvas; }
            set
            {
                if (value == _Canvas) return;
                _Canvas = value;
                _Id = _Canvas.GetNewId();
                _From.Canvas = value;
                _To.Canvas = value;
            }
        }
        #endregion

        #region 构造
        public Connection(Point from, Point to, int id)
        {
            _From = new Connector(from, this);
            _To = new Connector(from, this);
            _Id = id;
        }

        public override void ConfigCache()
        {
            Pen pen = new Pen(Color.Black, 2f);
            pen.CustomEndCap = new AdjustableArrowCap(6, 6, true);
            _Cache.CachePen("1", pen);
            _Cache.CachePen("2", new Pen(Color.Red, 2f));
            _Cache.CachePen("3", new Pen(Color.FromArgb(50, Color.Red), 5f));
        }
        #endregion

        #region 重写
        public override void Paint(Graphics g)
        {
            if (_IsHovered || IsSelected)
            {
                Pen pen1 = _Cache.GetPen("1");
                Pen pen2 = _Cache.GetPen("2");
                g.DrawLine(pen2, From.Location, To.Location);
                g.DrawLine(pen1, From.Location, To.Location);
            }
            else
            {
                Pen pen1 = _Cache.GetPen("1");
                g.DrawLine(pen1, From.Location, To.Location);
            }
        }

        public override bool IsHit(Point p)
        {
            Point p1, p2;
            RectangleF r1, r2;
            float o, u;
            p1 = _From.Location;
            p2 = _To.Location;
            //使p1恒为左端点
            if (p1.X > p2.X) { Point tmp = p2; p2 = p1; p1 = tmp; }

            r1 = new RectangleF(p1.X, p1.Y, 0, 0);
            r2 = new RectangleF(p2.X, p2.Y, 0, 0);
            r1.Inflate(3, 3);
            r2.Inflate(3, 3);
            if (RectangleF.Union(r1, r2).Contains(p))
            {
                if (p1.Y < p2.Y) //South-West
                {
                    o = r1.Left + ((r2.Left - r1.Left) * (p.Y - r1.Bottom) / (r2.Bottom - r1.Bottom));
                    u = r1.Right + ((r2.Right - r1.Right) * (p.Y - r1.Top) / (r2.Top - r1.Top));
                    return ((p.X > o) && (p.X < u));
                }
                else if(p1.Y > p2.Y) //South-East
                {
                    o = r1.Left + (((r2.Left - r1.Left) * (p.Y - r1.Top)) / (r2.Top - r1.Top));
                    u = r1.Right + (((r2.Right - r1.Right) * (p.Y - r1.Bottom)) / (r2.Bottom - r1.Bottom));
                    return ((p.X > o) && (p.X < u));
                }
                return true;
            }
            return false;
        }

        public override void Invalidate()
        {
            Rectangle f = new Rectangle(_From.Location, new Size(10, 10));
            Rectangle t = new Rectangle(_To.Location, new Size(10, 10));
            _Canvas.Invalidate(Rectangle.Union(f, t));
        }

        public override void MoveAs(Point v)
        {
            throw new NotImplementedException();
        }

        public override void MouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
