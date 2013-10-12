using System.Drawing;
using System.Drawing.Drawing2D;
using TCM.Helper;

namespace TCM.Model.Designer
{
    class ShapeDecision : Shape
    {
        #region 构造
        public ShapeDecision()
        {
            _Size = new Size(100, 56);

            //up right bottom left
            _Connectors.Add(new Connector(new Point(X, Top), this));
            _Connectors.Add(new Connector(new Point(Right, Y), this));
            _Connectors.Add(new Connector(new Point(X, Bottom), this));
            _Connectors.Add(new Connector(new Point(Left, Y), this));
        }

        public override void ConfigCache()
        {
            base.ConfigCache();
            _Cache.CachePen("1", new Pen(Color.Blue, 1.5f));
            _Cache.CachePen("2", new Pen(Color.Red, 1.5f));
            _Cache.CachePen("3", new Pen(Color.Black, 1.5f));
            Color c = Color.FromArgb(0xCC, 0xFF, 0xEE);
            _Cache.CacheBrush("1",
                new LinearGradientBrush(
                    new Rectangle(0, 0, 100, 100), c, GDIHelper.GetDeepColor(c), 30f));
            _Cache.CacheBrush("2", new SolidBrush(Color.FromArgb(175, Color.White)));
            _Cache.CacheFont("1", new Font("微软雅黑", 9));
        }
        #endregion

        #region 方法
        public override bool IsHit(Point p)
        {
            Rectangle rp = new Rectangle(p, new Size(5, 5));
            if (Bounds.Contains(rp)) return true;
            return false;
        }

        public override void OnPaint(Graphics g)
        {
            Point[] polygon = new Point[] {
                _Connectors[0].Location, 
                _Connectors[1].Location, 
                _Connectors[2].Location, 
                _Connectors[3].Location
            };
            g.FillPolygon(_Cache.GetBrush("0"), polygon);
            if (IsHovered)
            {
                g.FillPolygon(_Cache.GetBrush("1"), polygon);
                g.DrawPolygon(_Cache.GetPen("0"), polygon);
                foreach (Connector cop in _Connectors) cop.OnPaint(g);
                float tw_fi = g.MeasureString(_Texts[0], _Cache.GetFont("0")).Width;
                g.DrawString(_Texts[0], _Cache.GetFont("0"), Brushes.Black, X + 50 - tw_fi / 2, Y + 20);
            }
            else if (IsSelected)
            {
                g.DrawPolygon(_Cache.GetPen("1"), polygon);
                foreach (Connector cop in _Connectors) 
                    cop.OnPaint(g);
            }
            else
            {
                g.DrawPolygon(_Cache.GetPen("2"), polygon);
            }
            base.OnPaint(g);
        }

        public override void Invalidate()
        {
            Rectangle r = Bounds;
            r.Offset(-5, -5);
            r.Inflate(10, 10);
            if (_Canvas != null) _Canvas.Invalidate(r);
        }

        public override void  Resize(Size newsize)
        {
            base.Resize(newsize);
            //up right bottom left
            _Connectors[0].Location = new Point(X, Top);
            _Connectors[1].Location = new Point(Right, Y);
            _Connectors[2].Location = new Point(X, Bottom);
            _Connectors[3].Location = new Point(Left, Y);
            Invalidate();

        }
        #endregion
    }
}
