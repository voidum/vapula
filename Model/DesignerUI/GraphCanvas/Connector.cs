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
            get { return (_Container is Connection); }
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
            _Container = container;
            if (!Dragable) 
                _Attached = new List<Connector>();
            _Location = location;
            _Container = container;
        }

        public override void ConfigCache()
        {
            base.ConfigCache();
            _Cache.CachePen("1",
                new Pen(Color.FromArgb(200, Color.SkyBlue), 1f));
            _Cache.CachePen("2",
                new Pen(Color.FromArgb(150, Color.DarkRed), 2f));
            _Cache.CacheBrush("1",
                new SolidBrush(Color.FromArgb(200, Color.SkyBlue)));
            _Cache.CacheBrush("2",
                new SolidBrush(Color.FromArgb(150, Color.PaleGreen)));
        }
        #endregion

        #region 方法
        /// <summary>
        /// 判断自身到目标的关联是自关联
        /// </summary>
        private bool IsSelfConnect(Connector target)
        {
            var con = _Container as Connection;
            var cot = con.GetAnotherEnd(this);
            if (cot.AttachedTo == null) return false;
            if (target.Container != cot.AttachedTo.Container)
                return false;
            return true;
        }

        /// <summary>
        /// 将当前（主动）结点关联到指定（被动）结点
        /// </summary>
        public void AttachConnector(Connector target)
        {
            //要求目标不可移动
            if (target.Dragable)
                return;

            //要求不是自关联
            if (IsSelfConnect(target))
                return;

            //要求不重复关联
            //if (!IsRepeatConnect(target))
            //    return;

            target._Attached.Add(this);
            _AttachedTo = target;

            if (_Container.SyncTarget != null)
            {
                string sync_cmd = "attach-" +
                    ((_Container as Connection).From == this ? "from" : "to");
                _Container.SyncTarget.Sync(sync_cmd, target.Container);
            }
            MoveTo(new Point(target.Location.X, target.Location.Y)); //吸附
        }

        /// <summary>
        /// 解除当前（主动）结点与被动结点的关联
        /// </summary>
        public void DetachConnector()
        {
            if (_AttachedTo != null)
            {
                _AttachedTo._Attached.Remove(this);
                _AttachedTo = null;
                string sync_cmd = "detach-" + 
                    ((_Container as Connection).From == this ? "from" : "to");
                _Container.SyncTarget.Sync(sync_cmd, null);
            }
        }
        #endregion

        #region 重写
        public override bool IsHit(Point p)
        {
            Point b = _Location;
            b.Offset(-6, -6);
            Rectangle rp = new Rectangle(p.X, p.Y, 0, 0);
            Rectangle rd = new Rectangle(b.X, b.Y, 12, 12);
            return rd.Contains(rp);
        }

        public override void MoveAs(Point v)
        {
            _Location.X += v.X;
            _Location.Y += v.Y;
            if (_Attached != null)
                foreach (Connector cot in _Attached)
                    cot.MoveAs(v);
            Invalidate();
        }

        public override void MoveTo(Point p)
        {
            _Location.X = p.X;
            _Location.Y = p.Y;
            if (_Attached != null)
                foreach (Connector cot in _Attached)
                    cot.MoveTo(p);
            Invalidate();
        }

        public override void Invalidate()
        {
            _Canvas.Invalidate();
        }

        public override void Invalidate(Rectangle rect)
        {
            throw new NotImplementedException();
        }

        public override void OnPaint(Graphics g)
        {
            if (_IsHovered)
            {
                Pen pen = _Cache.GetPen("1");
                Brush brush1 = _Cache.GetBrush("1");
                Brush brush2 = _Cache.GetBrush("2");
                g.DrawEllipse(pen, _Location.X - 12, _Location.Y - 12, 24, 24);
                g.FillEllipse(Dragable ? brush1 : brush2, _Location.X - 11, _Location.Y - 11, 22, 22);
            }
            else if (_IsSelected)
            {
                Pen pen = _Cache.GetPen("2");
                g.DrawEllipse(pen, _Location.X - 4, _Location.Y - 4, 8, 8);
                g.FillEllipse(Brushes.Red, _Location.X - 3, _Location.Y - 3, 6, 6);
            }
        }

        public override void Dispose()
        {
            if (_AttachedTo != null)
                _AttachedTo.Attached.Remove(this);
            if (_Attached != null)
            {
                foreach (var cop in _Attached)
                    cop.AttachedTo = null;
                _Attached.Clear();
            }
            base.Dispose();
        }
        #endregion
    }
}