using System.Drawing;
using System.Drawing.Drawing2D;
using TCM.Helper;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 形状：处理
    /// </summary>
    class ShapeProcess : Shape
    {
        #region 字段
        private Image _Icon = null;
        #endregion

        #region 构造
        public ShapeProcess(Point p)
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
            _Cache.CachePen("0", new Pen(Color.Black, 1.5f));
            _Cache.CachePen("1", new Pen(Color.Blue, 1.6f));
            _Cache.CachePen("2", new Pen(Color.Red, 1.6f));
            Color c = Color.FromArgb(0xFF, 0xEE, 0xCC);
            _Cache.CacheBrush("0", 
                new LinearGradientBrush(
                    new Rectangle(0, 0, 100, 100),
                    c, GDIHelper.GetDeepColor(c), 30f));
            _Cache.CacheBrush("1",
                new SolidBrush(Color.FromArgb(175, Color.White)));
            _Cache.CacheFont("0",
                new Font("微软雅黑", 9));
        }
        #endregion

        #region 方法
        public override bool IsHit(Point p)
        {
            if (Bounds.Contains(p))
                return true;
            return false;
        }

        private void DrawText(Graphics g)
        {
            string id = (string)SyncTarget.Sync("get-id", null);
            g.DrawString(id, _Cache.GetFont("0"), Brushes.Black, Left + 5, Top + 4);

            string text = (string)SyncTarget.Sync("get-text", null);
            //text = text.Length > 4 ? text.Substring(0, 2) + "..." : text;

            float text_width = g.MeasureString(text, _Cache.GetFont("0")).Width;
            g.DrawString(text, _Cache.GetFont("0"), Brushes.Black, X - text_width / 2, Bottom - 18);
        }

        public override void OnPaint(Graphics g)
        {
            g.FillRectangle(_Cache.GetBrush("0"), Bounds);
            _Icon = SyncTarget.Sync("get-icon", null) as Image;
            if (_Icon != null)
                g.DrawImage(_Icon, new Rectangle(X + 21, Y + 4, 48, 48));
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
