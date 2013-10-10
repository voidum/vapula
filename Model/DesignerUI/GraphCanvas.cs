using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 图数据结构的画布
    /// </summary>
    public partial class GraphCanvas : CanvasBase
    {
        #region 事件
        /// <summary>
        /// “选中对象变更”事件的委托
        /// </summary>
        public delegate void SelectedObjectChanged(EntityBase entity);

        /// <summary>
        /// 通知容器当前画布的选中对象已经变更
        /// </summary>
        public event SelectedObjectChanged OnSelectedObjectChanged;
        #endregion

        #region 字段
        private bool _IfShowGrid = true;
        private bool _IfShowStatus = true;
        private bool _IfAlwaysShowText = true;
        private Size _GridSize = new Size(10, 10);
        private Random _RandomMaker = new Random();

        private List<ShapeBase> _Shapes = new List<ShapeBase>();
        private List<Connection> _Connections = new List<Connection>();

        private EntityBase _HoveredEntity;
        private EntityBase _SelectedEntity;
        private bool _Tracking = false;
        private Point _RefPoint;

        private ContextMenu _Menu;
        #endregion

        #region 属性
        /// <summary>
        /// 外框线
        /// </summary>
        public Rectangle Outline
        {
            get { return new Rectangle(0, 0, Width - 1, Height - 1); }
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

        public bool IfAlwaysShowText
        {
            get { return _IfAlwaysShowText; }
            set { _IfAlwaysShowText = value; Invalidate(); }
        }

        public Font FontStatus
        {
            get { return _Fonts[0]; }
        }

        public Pen PenCtrlOutline
        {
            get { return _Pens[0]; }
        }

        public Pen PenCanvasOutline
        {
            get { return _Pens[1]; }
        }

        public Brush BrushCtrlBack
        {
            get { return _Brushes[0]; }
        }
        #endregion

        #region 构造
        public CanvasGraph(int width, int height)
        {
            AllowDrop = true;
            Location = new Point(0, 0);
            WorkRectSize = new Size(width, height);

            _Brushes.Add(new SolidBrush(Color.FromArgb(245, 255, 251)));

            _Fonts.Add(new Font("微软雅黑", 9));

            _Pens.Add(new Pen(Color.Gray, 1.5f));
            _Pens.Add(new Pen(Color.FromArgb(220, 220, 220), 1.5f));

            BuildMenu();
            Invalidate();
        }

        /// <summary>
        /// 创建快捷菜单
        /// </summary>
        private void BuildMenu()
        {
            _Menu = new ContextMenu();

            MenuItem mnuNewConnection = new MenuItem("添加关联", new EventHandler(OnNewConnection));
            _Menu.MenuItems.Add(mnuNewConnection);
            MenuItem mnuDash1 = new MenuItem("-");
            _Menu.MenuItems.Add(mnuDash1);
            MenuItem mnuDelete = new MenuItem("删除选中对象", new EventHandler(OnDeleteEntity));
            _Menu.MenuItems.Add(mnuDelete);
            MenuItem mnuDeleteAllEntities = new MenuItem("全部删除", new EventHandler(OnDeleteAllEntities));
            _Menu.MenuItems.Add(mnuDeleteAllEntities);
            ContextMenu = _Menu;
        }
        #endregion

        #region 绘制
        private void DrawGrid(Graphics g)
        {
            //TODO: 绘制网格线
        }

        private void DrawStatus(Graphics g)
        {
            g.DrawString("复杂度：", FontStatus, Brushes.Black, new Point(_Padding, 15));
            g.DrawRectangle(PenCtrlOutline, _Padding + 50, 16, 101, 15);
            g.FillRectangle(BrushCtrlBack, _Padding + 51, 17, 100, 14);
            float cplxv = _Complexity * 100.0f / _MaxComplexity;
            g.FillRectangle(new SolidBrush(Color.FromArgb((int)(154 + cplxv), 238, (int)(144 - cplxv))), _Padding + 51, 17, cplxv, 14);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(245, 245, 245));
            g.DrawRectangle(PenCanvasOutline, Outline);
            g.DrawRectangle(PenCanvasOutline, new Rectangle(WorkRect.X - 1, WorkRect.Y - 1, WorkRect.Width + 2, WorkRect.Height + 2));
            g.FillRectangle(new SolidBrush(Color.White), WorkRect);
            if (_IfShowGrid) DrawGrid(g);
            if (_IfShowStatus) DrawStatus(g);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (Connection con in _Connections)
            {
                con.Paint(g);
                con.From.Paint(g);
                con.To.Paint(g);
            }
            foreach (ShapeBase spb in _Shapes)
            {
                spb.Paint(g);
            }
        }
        #endregion

        #region 菜单
        /// <summary>
        /// 删除画布上的选中对象
        /// </summary>
        private void OnDeleteEntity(object sender, EventArgs e)
        {
            if (_SelectedEntity != null)
            {
                if (typeof(ShapeBase).IsInstanceOfType(_SelectedEntity))
                {
                    ShapeBase shp = _SelectedEntity as ShapeBase;
                    foreach (Connector cop in shp.Connectors)
                        foreach (Connector cop2 in cop.Attached)
                            cop2.AttachedTo = null;
                    _Shapes.Remove(shp);
                    _Model.RemoveNode(_SelectedEntity.Model as NodeBase);
                    Invalidate();
                }
                else if (typeof(Connection).IsInstanceOfType(_SelectedEntity))
                {
                    Connection con = _SelectedEntity as Connection;
                    Connector cop = con.From.AttachedTo;
                    if (cop != null) cop.Attached.Remove(con.From);
                    cop = con.To.AttachedTo;
                    if (cop != null) cop.Attached.Remove(con.To);
                    _Connections.Remove(con);
                    _Model.RemoveLink(_SelectedEntity.Model as Link);
                    Invalidate();
                }
            }
        }

        private void OnDeleteAllEntities(object sender, EventArgs e)
        {
            _Model.RemoveAll();
            _Connections.Clear();
            _Shapes.Clear();
            Invalidate();
        }

        /// <summary>
        /// 添加连接线
        /// </summary>
        private void OnNewConnection(object sender, EventArgs e)
        {
            AddConnection(_RefPoint);
        }
        #endregion

        #region 事件响应
        protected void UpdateHovered(EntityBase entity)
        {
            if (_HoveredEntity != null)
            {
                _HoveredEntity.IsHovered = false;
                _HoveredEntity.Invalidate();
            }
            entity.IsHovered = true;
            _HoveredEntity = entity;
            _HoveredEntity.Invalidate();
        }

        private void UpdateSelected(EntityBase entity)
        {
            if (_SelectedEntity != null)
            {
                _SelectedEntity.IsSelected = false;
                _SelectedEntity.Invalidate();
            }
            _SelectedEntity = entity;
            entity.IsSelected = true;
            entity.Invalidate();
        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _RefPoint = new Point(e.X, e.Y);
            foreach (Connection con in _Connections)
            {
                if (con.From.Hit(_RefPoint)) //命中节点
                {
                    UpdateSelected(con.From);
                    _Tracking = true;
                    OnSelectedObjectChanged(con.From);
                    return;
                }
                if (con.To.Hit(_RefPoint)) //命中结点
                {
                    //if(_Tracking) to attach 不知是否合理啊。。。
                    UpdateSelected(con.To);
                    _Tracking = true;
                    OnSelectedObjectChanged(con.To);
                    return;
                }
            }
            foreach (ShapeBase shp in _Shapes)
            {
                if (shp.Hit(_RefPoint)) //命中形状
                {
                    _Tracking = true;
                    UpdateSelected(shp);
                    OnSelectedObjectChanged(shp);
                    return;
                }
            }
            foreach (Connection con in _Connections)
            {
                if (con.Hit(_RefPoint)) //命中连接线
                {
                    UpdateSelected(con);
                    OnSelectedObjectChanged(con);
                    return;
                }
            }
            if (_SelectedEntity != null) _SelectedEntity.IsSelected = false;
            _SelectedEntity = null;
            OnSelectedObjectChanged(null);
            Invalidate();
        }

        /// <summary>
        /// 鼠标抬起事件
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_Tracking)
            {
                Point p = new Point(e.X, e.Y);
                if (typeof(Connector).IsInstanceOfType(_SelectedEntity))
                {
                    Connector cop = _SelectedEntity as Connector;
                    foreach (ShapeBase shp in _Shapes)
                    {
                        Connector cop2 = shp.GetHitConnector(p);
                        if (cop2 != null)
                        {
                            if (cop2.Movable || cop.IfLinkSelf(cop2)) return;
                            cop.DetachConnector();
                            cop.AttachConnector(cop2);
                            cop2.IsHovered = false;
                            _Tracking = false;
                            return;
                        }
                    }
                    cop.DetachConnector();
                }
                _Tracking = false;
            }
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
            if (_Tracking)
            {
                _SelectedEntity.Move(new Point(p.X - _RefPoint.X, p.Y - _RefPoint.Y));
                _RefPoint = p;
                Invalidate();
                if (typeof(Connector).IsInstanceOfType(_SelectedEntity))
                {
                    foreach (ShapeBase spb in _Shapes)
                        spb.GetHitConnector(p);
                }
            }
            foreach (Connection con in _Connections)
            {
                if (con.From.Hit(p))
                {
                    UpdateHovered(con.From);
                    return;
                }
                else con.From.IsHovered = false;
                if (con.To.Hit(p))
                {
                    UpdateHovered(con.To);
                    return;
                }
                else con.To.IsHovered = false;
            }
            foreach (ShapeBase spb in _Shapes)
            {
                foreach (Connector cop in spb.Connectors)
                {
                    if (cop.Hit(p))
                    {
                        UpdateHovered(cop);
                        return;
                    }
                    else cop.IsHovered = false;
                }
                if (spb.Hit(p))
                {
                    UpdateHovered(spb);
                    return;
                }
                else spb.IsHovered = false;
            }
            foreach (Connection con in _Connections)
            {
                if (con.Hit(p)) UpdateHovered(con);
                else con.IsHovered = false;
            }
        }
        #endregion
    }
}