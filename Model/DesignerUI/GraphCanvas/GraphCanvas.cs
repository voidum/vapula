using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 用于图的画布
    /// </summary>
    [ToolboxItem(true)]
    public class CanvasGraph : Canvas
    {
        #region 事件
        /// <summary>
        /// 通知容器当前画布的选中对象已经变更
        /// </summary>
        public event Action<Entity> OnSelectedChanged;
        #endregion

        #region 字段
        private bool _IfShowGrid = true;
        private bool _IfShowStatus = true;

        private Size _GridSize = new Size(10, 10);
        
        private int _Complexity = 0;
        private int _MaxComplexity = 100;

        private List<Shape> _Shapes = new List<Shape>();
        private List<Connection> _Connections = new List<Connection>();

        private Entity _HoveredEntity;
        private List<Entity> _SelectedEntities;

        private bool _IsDraging = false;
        private bool _IsAppendSelect = false;

        private Point _DragRefPoint;

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

        /// <summary>
        /// 获取状态Font
        /// </summary>
        public Font FontStatus
        {
            get { return _Cache.GetFont("1"); }
        }

        /// <summary>
        /// 获取控件外框线Pen
        /// </summary>
        public Pen PenOutlineCtrl
        {
            get { return _Cache.GetPen("1"); }
        }

        /// <summary>
        /// 获取工作区外框线Pen
        /// </summary>
        public Pen PenOutlineWork
        {
            get { return _Cache.GetPen("2"); }
        }

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

            BuildMenu();
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
            g.DrawRectangle(PenOutlineCtrl, _Padding + 50, 16, 101, 15);
            g.FillRectangle(BrushBack, _Padding + 51, 17, 100, 14);
            float cplxv = _Complexity * 100.0f / _MaxComplexity;
            g.FillRectangle(new SolidBrush(Color.FromArgb((int)(154 + cplxv), 238, (int)(144 - cplxv))), _Padding + 51, 17, cplxv, 14);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(245, 245, 245));
            g.DrawRectangle(PenOutlineWork, Outline);
            g.DrawRectangle(PenOutlineWork, new Rectangle(WorkRect.X - 1, WorkRect.Y - 1, WorkRect.Width + 2, WorkRect.Height + 2));
            g.FillRectangle(new SolidBrush(Color.White), WorkRect);
            if (_IfShowGrid) DrawGrid(g);
            if (_IfShowStatus) DrawStatus(g);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (var con in _Connections)
                con.Paint(g);
            foreach (var shp in _Shapes)
                shp.Paint(g);
        }
        #endregion

        #region 菜单
        /// <summary>
        /// 删除画布上的选中对象
        /// </summary>
        private void OnDeleteEntity(object sender, EventArgs e)
        {
            foreach (var ent in _SelectedEntities)
            {
                if (typeof(Shape).IsInstanceOfType(ent))
                {
                    Shape shp = ent as Shape;
                    foreach (Connector cop in shp.Connectors)
                        foreach (Connector cop2 in cop.Attached)
                            cop2.AttachedTo = null;
                    _Shapes.Remove(shp);
                    //_Model.RemoveNode(_SelectedEntity.Model as NodeBase);
                    Invalidate();
                }
                else if (typeof(Connection).IsInstanceOfType(ent))
                {
                    Connection con = ent as Connection;
                    Connector cop = con.From.AttachedTo;
                    if (cop != null) cop.Attached.Remove(con.From);
                    cop = con.To.AttachedTo;
                    if (cop != null) cop.Attached.Remove(con.To);
                    _Connections.Remove(con);
                    Invalidate();
                }
            }
        }

        private void OnDeleteAllEntities(object sender, EventArgs e)
        {
            _Connections.Clear();
            _Shapes.Clear();
            Invalidate();
        }

        /// <summary>
        /// 添加连接线
        /// </summary>
        private void OnNewConnection(object sender, EventArgs e)
        {
            //AddConnection(_RefPoint);
        }
        #endregion
        /*
        protected void UpdateHovered(Entity entity)
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

        private void UpdateSelected(Entity entity)
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
         */

        #region 事件响应
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control) 
                _IsAppendSelect = true;
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            MessageBox.Show("up");
            if (e.Control) { MessageBox.Show("ctrl"); }
            else { MessageBox.Show("unctrl"); }
            base.OnKeyUp(e);
        }

        /*
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _DragRefPoint = e.Location;
            if (e.Button == MouseButtons.Right)
            {
                _Menu.Show(this, e.Location);
                return;
            }
            foreach (Connection con in _Connections)
            {
                if (con.From.IsHit(_DragRefPoint)) //命中节点
                {
                    UpdateSelected(con.From);
                    _IsDraging = true;
                    OnSelectedChanged(con.From);
                    return;
                }
                if (con.To.IsHit(_DragRefPoint)) //命中结点
                {
                    //if(_Tracking) to attach 不知是否合理啊。。。
                    UpdateSelected(con.To);
                    _IsDraging = true;
                    OnSelectedChanged(con.To);
                    return;
                }
            }
            foreach (Shape shp in _Shapes)
            {
                if (shp.IsHit(_DragRefPoint)) //命中形状
                {
                    _IsDraging = true;
                    shp.MouseDown(e);
                    UpdateSelected(shp);
                    OnSelectedChanged(shp);
                    return;
                }
            }
            foreach (Connection con in _Connections)
            {
                if (con.IsHit(_DragRefPoint)) //命中连接线
                {
                    UpdateSelected(con);
                    OnSelectedChanged(con);
                    return;
                }
            }
            if (_SelectedEntity != null) _SelectedEntity.IsSelected = false;
            _SelectedEntity = null;
            OnSelectedChanged(null);
            Invalidate();
        }

        /// <summary>
        /// 鼠标抬起事件
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_Draging)
            {
                Point p = new Point(e.X, e.Y);
                if (typeof(Connector).IsInstanceOfType(_SelectedEntity))
                {
                    Connector cop = _SelectedEntity as Connector;
                    foreach (Shape shp in _Shapes)
                    {
                        Connector cop2 = shp.GetHitConnector(p);
                        if (cop2 != null)
                        {
                            if (cop2.Dragable || cop.IfLinkSelf(cop2)) return;
                            cop.DetachConnector();
                            cop.AttachConnector(cop2);
                            cop2.IsHovered = false;
                            _Draging = false;
                            return;
                        }
                    }
                    cop.DetachConnector();
                }
                _Draging = false;
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
            if (_Draging)
            {
                _SelectedEntity.MoveAs(
                    new Point(p.X - _DragRefPoint.X, p.Y - _DragRefPoint.Y));
                _DragRefPoint = p;
                Invalidate();
                if (typeof(Connector).IsInstanceOfType(_SelectedEntity))
                {
                    foreach (Shape spb in _Shapes)
                        spb.GetHitConnector(p);
                }
            }
            foreach (Connection con in _Connections)
            {
                if (con.From.IsHit(p))
                {
                    UpdateHovered(con.From);
                    return;
                }
                else con.From.IsHovered = false;
                if (con.To.IsHit(p))
                {
                    UpdateHovered(con.To);
                    return;
                }
                else con.To.IsHovered = false;
            }
            foreach (Shape spb in _Shapes)
            {
                foreach (Connector cop in spb.Connectors)
                {
                    if (cop.IsHit(p))
                    {
                        UpdateHovered(cop);
                        return;
                    }
                    else cop.IsHovered = false;
                }
                if (spb.IsHit(p))
                {
                    UpdateHovered(spb);
                    return;
                }
                else spb.IsHovered = false;
            }
            foreach (Connection con in _Connections)
            {
                if (con.IsHit(p)) UpdateHovered(con);
                else con.IsHovered = false;
            }
        }
         */
        #endregion

        #region 集合
        public bool Find(int id, out Entity entity)
        {
            entity = null;
            return false;
        }
        public bool Find(int id, out Shape shape)
        {
            shape = null;
            return false;
        }
        public bool Find(int id, out Connection connection)
        {
            connection = null;
            return false;
        }

        public int GetNewId(Entity trait)
        {

            return 0;
        }
        public int GetNewId(Shape trait)
        {
            return 0;
        }
        public int GetNewId(Connection trait)
        {
            return 0;
        }
        #endregion
    }
}