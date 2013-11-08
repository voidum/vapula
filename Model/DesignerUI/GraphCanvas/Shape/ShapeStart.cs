using System.Drawing;
using System.Drawing.Drawing2D;
using TCM.Helper;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 形状：起点
    /// </summary>
    class ShapeStart : Shape
    {
        #region 构造
        public ShapeStart(Point p)
            : base(p)
        {
            _Size = new Size(60, 40);

            //up right bottom left
            _Connectors.Add(new Connector(new Point(X, Top), this));
            _Connectors.Add(new Connector(new Point(Right, Y), this));
            _Connectors.Add(new Connector(new Point(X, Bottom), this));
            _Connectors.Add(new Connector(new Point(Left, Y), this));
        }

        public override void ConfigCache()
        {
            base.ConfigCache();
            Color c = Color.FromArgb(0xCC, 0xEE, 0xFF);
            _Cache.CacheBrush("back", 
                new LinearGradientBrush(
                    new Rectangle(0, 0, 100, 100),
                    c, GDIHelper.GetDeepColor(c), 30f));
        }
        #endregion

        #region 方法
        protected override void DrawText(Graphics g)
        {
            string id = (string)SyncTarget.Sync("get-id", null);
            float id_width = g.MeasureString(id, _Cache.GetFont("id")).Width;
            g.DrawString(id, _Cache.GetFont("id"), Brushes.Black,
                X - id_width / 2, Y - 8);
        }

        public override bool IsHit(Point p)
        {
            if (Bounds.Contains(p))
                return true;
            return false;
        }

        public override void OnPaint(Graphics g)
        {
            g.FillRectangle(_Cache.GetBrush("back"), Bounds);
            DrawText(g);
            if (IsHovered)
            {
                g.DrawRectangle(_Cache.GetPen("1"), Bounds);
                foreach (Connector cop in _Connectors) 
                    cop.OnPaint(g);
            }
            else if (IsSelected)
            {
                g.DrawRectangle(_Cache.GetPen("2"), Bounds);
                foreach (Connector cop in _Connectors) 
                    cop.OnPaint(g);
            }
            else
            {
                g.DrawRectangle(_Cache.GetPen("0"), Bounds);
            }
            base.OnPaint(g);
        }

        public override void Invalidate()
        {
            Rectangle r = Bounds;
            r.Offset(-5, -5);
            r.Inflate(10, 10);
            if(_Canvas != null) _Canvas.Invalidate(r);
        }

        public override void Resize(Size newsize)
        {
            //up right bottom left
            _Connectors[0].Location = new Point(X, Top);
            _Connectors[1].Location = new Point(Right, Y);
            _Connectors[2].Location = new Point(X, Bottom);
            _Connectors[3].Location = new Point(Left, Y);
            base.Resize(newsize);
        }
        #endregion
    }
}
