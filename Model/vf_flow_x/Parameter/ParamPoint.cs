using System;

namespace Vapula.Flow
{
    /// <summary>
    /// 参数坐标
    /// </summary>
    public class ParamPoint
    {
        public static ParamPoint Null
            = new ParamPoint(-1, -1);

        private int _NodeId;
        private int _ParamId;

        public int NodeId
        {
            get { return _NodeId; }
            set { _NodeId = value; }
        }

        public int ParamId
        {
            get { return _ParamId; }
            set { _ParamId = value; }
        }

        public bool IsValid
        {
            get { return _NodeId > 0 && _ParamId > 0; }
        }

        public ParamPoint(int node, int param)
        {
            _NodeId = node;
            _ParamId = param;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ParamPoint))
                return false;
            var point = (ParamPoint)obj;
            return point.NodeId == this.NodeId && point.ParamId == this.ParamId;
        }

        public static bool operator ==(ParamPoint left, ParamPoint right)
        {
            return left.NodeId == right.NodeId && left.ParamId == right.ParamId;
        }

        public static bool operator !=(ParamPoint left, ParamPoint right)
        {
            return !(left == right);
        }
    }
}
