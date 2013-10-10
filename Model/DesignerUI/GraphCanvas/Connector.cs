using System;
using System.Collections.Generic;
using System.Drawing;

namespace TCM.Model.Designer
{
    /// <summary>
    /// 图元：连结点
    /// </summary>
    public class Connector : Entity
    {
        #region 字段
        protected bool _Dragable;
        protected Entity _Container;
        protected List<Connector> _Attached;
        protected Connector _AttachedTo;
        #endregion

        #region 属性
        /// <summary>
        /// 获取连结点是否能够拖动
        /// </summary>
        public bool Dragable
        {
            get { return _Dragable; }
        }

        /// <summary>
        /// 获取连结点容器图元
        /// </summary>
        public Entity Container
        {
            get { return _Container; }
        }

        /// <summary>
        /// <para>获取或设置关联到的连结点</para>
        /// </summary>
        public Connector AttachedTo
        {
            get { return _AttachedTo; }
            set { _AttachedTo = value; }
        }

        /// <summary>
        /// <para>获取被关联的连结点的集合</para>
        /// </summary>
        public List<Connector> Attached
        {
            get { return _Attached; }
        }

        /// <summary>
        /// 获取或设置连结点的位置
        /// </summary>
        public override Point Location
        {
            get { return _Location; }
            set
            {
                if (value == _Location) return;
                if (!Dragable)
                {
                    foreach (Connector cop in _Attached)
                        cop.Location = value;
                    _Location = value;
                }
                else
                {
                    Connection con = _Container as Connection;
                    con.Invalidate();
                }
                Invalidate();
            }
        }
        #endregion

        #region 构造
        /// <summary>
        /// <para>构造结点</para>
        /// <para>需要指定位置和容器</para>
        /// </summary>
        public Connector(Point location, Entity container)
        {
            if (container == null)
                throw new Exception("初始化连结点必须耦合容器。");
            _Dragable = container is Connection;
            if (!_Dragable) _Attached = new List<Connector>();
            _Location = location;
            _Container = container;
        }

        public override void ConfigCache()
        {
            base.ConfigCache();
            _Cache.CachePen("1",
                new Pen(Color.FromArgb(200, Color.DarkRed), 1f));
            _Cache.CachePen("2",
                new Pen(Color.FromArgb(200, Color.DarkRed), 2f));
            _Cache.CacheBrush("1",
                new SolidBrush(Color.FromArgb(200, Color.SkyBlue)));
            _Cache.CacheBrush("2",
                new SolidBrush(Color.FromArgb(150, Color.PaleGreen)));
        }
        #endregion

        #region 方法
        /// <summary>
        /// 检查结点所属关联的另一端是否链接了指定结点的容器
        /// </summary>
        public bool IfLink(Connector target)
        {
            if (!_Dragable) return false;
            Connection con = _Container as Connection;
            if (this == con.From)
            {
                if (con.To._AttachedTo == null) return false;
                return con.To._AttachedTo._Container == target._Container;
            }
            else
            {
                if (con.From._AttachedTo == null) return false;
                return con.From._AttachedTo._Container == target._Container;
            }
        }

        public bool IfLinkSelf(Connector c) { return false; }

        /// <summary>
        /// 将当前（主动）结点关联到指定（被动）结点
        /// </summary>
        public void AttachConnector(Connector c)
        {
            if (c._Dragable) return; //要求目标不可移动
            Shape shp = c.Container as Shape;
            if (shp.IfHasAttached(this)) return; //不重复关联
            Location = new Point(c.Location.X, c.Location.Y); //吸附
            _Canvas.Invalidate(); //TODO：此处更新区域太大
            c._Attached.Add(this);
            _AttachedTo = c;
        }

        /// <summary>
        /// 取消当前（主动）结点与被动结点的关联
        /// </summary>
        public void DetachConnector()
        {
            if (_AttachedTo == null) return;
            _AttachedTo._Attached.Remove(this);
            _AttachedTo = null;
        }
        #endregion
        #region 重写
        public override void Paint(Graphics g)
        {
            if (_IsHovered)
            {
                Pen pen = _Cache.GetPen("1");
                Brush brush1 = _Cache.GetBrush("1");
                Brush brush2 = _Cache.GetBrush("2");
                g.DrawEllipse(pen, _Location.X - 10, _Location.Y - 10, 20, 20);
                g.FillEllipse(_Dragable ? brush1 : brush2, _Location.X - 10, _Location.Y - 10, 20, 20);
            }
            else
            {
                Pen pen = _Cache.GetPen("2");
                g.DrawEllipse(pen, _Location.X - 4, _Location.Y - 4, 8, 8);
                if (_IsSelected)
                    g.FillEllipse(Brushes.Red, _Location.X - 3, _Location.Y - 3, 6, 6);
                else
                    g.FillEllipse(Brushes.WhiteSmoke, _Location.X - 3, _Location.Y - 3, 6, 6);
            }
        }

        public override bool IsHit(Point p)
        {
            Point b = _Location;
            b.Offset(-4, -4);
            Rectangle rp = new Rectangle(p.X, p.Y, 0, 0);
            Rectangle rd = new Rectangle(b.X, b.Y, 9, 9);
            return rd.Contains(rp);
        }

        public override void Invalidate()
        {
            Point p = _Location;
            p.Offset(-15, -15);
            _Canvas.Invalidate(new Rectangle(p.X, p.Y, 30, 30));
        }

        /// <summary>
        /// 根据指定矢量移动
        /// </summary>
        public override void MoveAs(Point v)
        {
            _Location.X += v.X;
            _Location.Y += v.Y;
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