using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Vapula.Model;

namespace Vapula.Designer
{
    /// <summary>
    /// 用于图的画布
    /// </summary>
    [ToolboxItem(true)]
    public partial class CanvasGraph : Canvas
    {
        #region 字段
        private bool _IfShowGrid = true;
        private bool _IfShowStatus = true;

        private Size _GridSize = new Size(25, 25);
        
        private int _Complexity = 0;
        private int _MaxComplexity = 100;

        private List<Connection> _Connections 
            = new List<Connection>();
        private List<Connector> _Connectors 
            = new List<Connector>();
        private List<Shape> _Shapes 
            = new List<Shape>();

        private List<Entity> _SelectedEntities 
            = new List<Entity>();

        private Point _RefPoint;
        private bool _IsDraging = false;

        private bool _IsMultiSelect = false;

        private Graph _Graph = new Graph();
        #endregion

        #region 隐藏父属性
        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = value; }
        }

        [Browsable(false)]
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        [Browsable(false)]
        public override RightToLeft RightToLeft
        {
            get { return base.RightToLeft; }
            set { base.RightToLeft = value; }
        }

        [Browsable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }
        #endregion

        #region 属性
        [Browsable(false)]
        public List<Entity> SelectedEntities
        {
            get { return _SelectedEntities; }
        }

        /// <summary>
        /// 是否显示网格
        /// </summary>
        public bool IfShowGrid
        {
            get { return _IfShowGrid; }
            set { _IfShowGrid = value; Invalidate(); }
        }

        /// <summary>
        /// 是否显示状态
        /// </summary>
        public bool IfShowStatus
        {
            get { return _IfShowStatus; }
            set { _IfShowStatus = value; Invalidate(); }
        }

        /// <summary>
        /// 复杂度
        /// </summary>
        [Browsable(false)]
        public int Complexity
        {
            get 
            {
                if (_Complexity > _MaxComplexity)
                    _Complexity = _MaxComplexity;
                return _Complexity;
            }
        }

        /// <summary>
        /// 外框线
        /// </summary>
        [Browsable(false)]
        public Rectangle Outline
        {
            get { return new Rectangle(0, 0, Width - 1, Height - 1); }
        }

        /// <summary>
        /// 获取状态Font
        /// </summary>
        [Browsable(false)]
        public Font FontStatus
        {
            get { return _Cache.GetFont("1"); }
        }

        /// <summary>
        /// 获取控件外框线Pen
        /// </summary>
        [Browsable(false)]
        public Pen PenOutlineCtrl
        {
            get { return _Cache.GetPen("1"); }
        }

        /// <summary>
        /// 获取工作区外框线Pen
        /// </summary>
        [Browsable(false)]
        public Pen PenOutlineWork
        {
            get { return _Cache.GetPen("2"); }
        }

        [Browsable(false)]
        public Brush BrushBack
        {
            get { return _Cache.GetBrush("1"); }
        }
        #endregion

        #region 构造
        public CanvasGraph()
            : this(400, 300)
        {
        }

        public CanvasGraph(int width, int height)
        {
            AllowDrop = true;
            Location = new Point(0, 0);
            WorkSize = new Size(width, height);

            Invalidate();
        }

        public override void ConfigCache()
        {
            _Cache.CacheBrush("1", new SolidBrush(Color.FromArgb(245, 255, 251)));
            _Cache.CacheFont("1", new Font("微软雅黑", 9));
            _Cache.CachePen("1", new Pen(Color.Gray, 1.5f));
            _Cache.CachePen("2", new Pen(Color.FromArgb(220, 220, 220), 1.5f));
            base.ConfigCache();
        }
        #endregion

        #region 绘制
        private void DrawGrid(Graphics g)
        {
            //TODO: 绘制网格线
        }

        private void DrawStatus(Graphics g)
        {
            g.DrawString("复杂度：", FontStatus, Brushes.Black, new Point(_Padding, 20));
            g.DrawRectangle(PenOutlineCtrl, _Padding + 50, 20, 101, 15);
            g.FillRectangle(BrushBack, _Padding + 51, 21, 100, 14);
            float cplxv = Complexity * 100.0f / _MaxComplexity;
            g.FillRectangle(
                new SolidBrush(
                    Color.FromArgb((int)(155 + cplxv), (int)(238 - cplxv * 0.25), (int)(200 - cplxv * 2))), 
                    _Padding + 51, 21, cplxv, 14);
        }

        private void DrawBackground(Graphics g)
        {
            g.Clear(Color.FromArgb(245, 245, 245));
            g.DrawRectangle(PenOutlineWork, Outline);
            g.DrawRectangle(PenOutlineWork, new Rectangle(WorkRect.X - 1, WorkRect.Y - 1, WorkRect.Width + 2, WorkRect.Height + 2));
            g.FillRectangle(new SolidBrush(Color.White), WorkRect);
        }

        private void DrawEntity(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (var con in _Connections)
            {
                con.OnPaint(g);
                con.From.OnPaint(g);
                con.To.OnPaint(g);
            }
            foreach (var shp in _Shapes)
            {
                shp.OnPaint(g);
                foreach (var cop in shp.Connectors)
                    cop.OnPaint(g);
            }
        }
        #endregion

        #region 事件响应
        /// <summary>
        /// 绘制事件
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            DrawEntity(e.Graphics);
        }

        /// <summary>
        /// 绘制背景事件
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            DrawBackground(g);
            if (_IfShowGrid) DrawGrid(g);
            if (_IfShowStatus) DrawStatus(g);
        }
        
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            if (p.X < WorkRect.Left) p.X = WorkRect.Left;
            if (p.Y < WorkRect.Top) p.Y = WorkRect.Top;
            if (p.X > WorkRect.Right) p.X = WorkRect.Right;
            if (p.Y > WorkRect.Bottom) p.Y = WorkRect.Bottom;
            if (_IsDraging)
            {
                Point v = new Point(
                    p.X - _RefPoint.X,
                    p.Y - _RefPoint.Y);
                int vx = Math.Abs(v.X);
                int vy = Math.Abs(v.Y);
                if (vx > 0 || vy > 0)
                {
                    _RefPoint = p;
                    foreach (Entity ent in _SelectedEntities)
                        ent.MoveAs(v);
                }
            }
            foreach (Connection con in _Connections)
            {
                if (con.From.IsHovered = con.From.IsHit(p)) break;
                if (con.To.IsHovered = con.To.IsHit(p)) break;
                if (con.IsHovered = con.IsHit(p)) return;
            }
            foreach (Shape shp in _Shapes)
            {
                foreach (Connector cot in shp.Connectors)
                    cot.IsHovered = cot.IsHit(p);
                if (shp.IsHovered = shp.IsHit(p))
                    return;
            }
        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _RefPoint = e.Location;
            Point p = e.Location;
            if (p.X < WorkRect.Left ||
                p.Y < WorkRect.Top ||
                p.X > WorkRect.Right ||
                p.Y > WorkRect.Bottom)
                return;
            if (e.Button == MouseButtons.Left)
            {
                foreach (Connection con in _Connections)
                {
                    if (con.From.IsHit(p)) { SelectEntity(con.From); return; }
                    if (con.To.IsHit(p)) { SelectEntity(con.To); return; }
                }
                foreach (Shape shp in _Shapes)
                {
                    foreach (Connector cot in shp.Connectors)
                        if (cot.IsHit(p))
                        {
                            Connection con = AddConnection(p);
                            con.From.AttachConnector(cot);
                            return;
                        }
                    if (shp.IsHit(p)) { SelectEntity(shp); return; }
                }
                foreach (Connection con in _Connections)
                {
                    if (con.IsHit(p)) { SelectEntity(con); return; }
                }
                SelectEntity(null);
                Invalidate();
            }
        }

        /// <summary>
        /// 鼠标抬起事件
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_IsDraging)
            {
                Point p = e.Location;
                if (_SelectedEntities.Count != 1) return;
                Entity ent = _SelectedEntities[0];
                if (ent is Connector)
                {
                    Connector cot_sel = ent as Connector;
                    foreach (Shape shp in _Shapes)
                    {
                        Connector cot_target = 
                            shp.Connectors.Find(
                            (cot) => { return cot.IsHit(p); });
                        if (cot_target != null)
                        {
                            if (cot_target.Dragable) return; //线-线关联
                            cot_sel.DetachConnector();
                            cot_sel.AttachConnector(cot_target);
                            cot_sel.IsHovered = false;
                            _IsDraging = false;
                            return;
                        }
                    }
                    cot_sel.DetachConnector();
                }
                _IsDraging = false;
            }
        }
        #endregion
    }
}