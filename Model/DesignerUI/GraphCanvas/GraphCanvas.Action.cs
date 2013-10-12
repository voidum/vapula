using System;
using System.Drawing;

namespace TCM.Model.Designer
{
    public partial class CanvasGraph
    {
        private void ClearSelection()
        {
            foreach (var tmp_ent in _SelectedEntities)
                tmp_ent.IsSelected = false;
            _SelectedEntities.Clear();
        }

        /// <summary>
        /// 删除画布上的选中对象
        /// </summary>
        public void RemoveEntities(params Entity[] entities)
        {
            foreach (var ent in entities)
            {
                if (typeof(Shape).IsInstanceOfType(ent))
                {
                    Shape shp = ent as Shape;
                    _Shapes.Remove(shp);
                    shp.Dispose();
                    Invalidate();
                }
                else if (typeof(Connection).IsInstanceOfType(ent))
                {
                    Connection con = ent as Connection;
                    _Connections.Remove(con);
                    con.Dispose();
                    Invalidate();
                }
            }
        }

        public void RemoveAllEntities()
        {
            foreach (Connection con in _Connections)
                con.Dispose();
            _Connections.Clear();
            foreach (Shape shp in _Shapes)
                shp.Dispose();
            _Shapes.Clear();
            Invalidate();
        }

        /// <summary>
        /// 添加连接线
        /// </summary>
        public void AddConnection(Point p)
        {
            Connection con = new Connection(p, p);
            con.Canvas = this;
            _Connections.Add(con);
            ClearSelection();
            _SelectedEntities.Add(con.To);
            _IsDraging = true;
            Invalidate();
        }

        public void AddShapeProcess(Point p)
        {
            ShapeProcess shp = new ShapeProcess();
            shp.Canvas = this;
            shp.Location = p;
            _Shapes.Add(shp);
            Invalidate();
        }

        public void AddShapeDecision(Point p)
        {
            ShapeDecision shp = new ShapeDecision();
            shp.Canvas = this;
            shp.Location = p;
            _Shapes.Add(shp);
            Invalidate();
        }

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
