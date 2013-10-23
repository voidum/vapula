using System.Collections.Generic;
using System.Drawing;

namespace TCM.Model.Designer
{
    public partial class CanvasGraph
    {
        /// <summary>
        /// 选择图元
        /// </summary>
        private void SelectEntity(Entity entity)
        {
            if (!_IsMultiSelect)
                ClearSelection();
            if (entity != null)
            {
                _IsDraging = true;
                entity.IsSelected = true;
                _SelectedEntities.Add(entity);
            }
            OnSelectedChanged();
        }

        /// <summary>
        /// 清除选中
        /// </summary>
        private void ClearSelection()
        {
            foreach (var tmp_ent in _SelectedEntities)
                tmp_ent.IsSelected = false;
            _SelectedEntities.Clear();
        }

        /// <summary>
        /// 移除画布上的选中图元
        /// </summary>
        public void RemoveEntities(params Entity[] entities)
        {
            foreach (var ent in entities)
            {
                if (ent is Shape)
                {
                    Shape shp = ent as Shape;
                    _Shapes.Remove(shp);
                    SyncTarget.Sync("remove-node", shp);
                    shp.Dispose();
                }
                else if (ent is Connection)
                {
                    Connection con = ent as Connection;
                    _Connections.Remove(con);
                    SyncTarget.Sync("remove-link", con);
                    con.Dispose();
                }
            }
            Invalidate();
        }

        /// <summary>
        /// 移除所有图元
        /// </summary>
        public void RemoveAllEntities()
        {
            foreach (Connection con in _Connections)
                con.Dispose();
            _Connections.Clear();
            foreach (Shape shp in _Shapes)
                shp.Dispose();
            _Shapes.Clear();
            SyncTarget.Sync("remove-all", null);
            Invalidate();
        }

        /// <summary>
        /// 添加连接线
        /// </summary>
        public Connection AddConnection(Point p)
        {
            Connection con = new Connection(p, p);
            con.Canvas = this;
            _Connections.Add(con);
            ClearSelection();
            _SelectedEntities.Add(con.To);
            _IsDraging = true;
            SyncTarget.Sync("add-link", con);
            Invalidate();
            return con;
        }

        /// <summary>
        /// 添加执行形状
        /// </summary>
        public Shape AddShapeProcess(Point p)
        {
            ShapeProcess shp = new ShapeProcess(p);
            shp.Canvas = this;
            _Shapes.Add(shp);
            Invalidate();
            return shp;
        }

        /// <summary>
        /// 添加决策形状
        /// </summary>
        public Shape AddShapeDecision(Point p)
        {
            ShapeDecision shp = new ShapeDecision(p);
            shp.Canvas = this;
            _Shapes.Add(shp);
            Invalidate();
            return shp;
        }

        #region 集合
        /// <summary>
        /// 获取指定标识对应的形状
        /// </summary>
        public Shape FindShape(int id)
        {
            foreach (var shp in _Shapes)
                if (shp.Id == id)
                    return shp;
            return null;
        }

        /// <summary>
        /// 获取指定标识对应的连接线
        /// </summary>
        public Connection FindConnection(int id)
        {
            foreach (var con in _Connections)
                if (con.Id == id)
                    return con;
            return null;
        }

        /// <summary>
        /// 获取图元新标识
        /// </summary>
        public override int GetNewId(Entity entity)
        {
            List<int> ids = new List<int>();
            if (entity is Connection)
                foreach (var con in _Connections)
                    ids.Add(con.Id);
            else if(entity is Shape)
                foreach (var shp in _Shapes)
                    ids.Add(shp.Id);
            ids.Sort();
            for (int i = 0; i < ids.Count; i++)
                if (ids[i] != i) return i;
            return ids.Count;
        }
        #endregion
    }
}
