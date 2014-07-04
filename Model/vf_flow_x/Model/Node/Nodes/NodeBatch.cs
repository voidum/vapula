using System.Collections.Generic;
using Vapula.Helper;
using Vapula.Model;

namespace Vapula.Flow
{
    /// <summary>
    /// 节点：批处理
    /// </summary>
    public class NodeBatch : Node
    {
        public override NodeType Type
        {
            get { return NodeType.Batch; }
        }

        public override bool CanCustomParam
        {
            get { return true; }
        }

        public override List<Parameter> Parameters
        {
            get { return new List<Parameter>(); }
        }

        public override bool Valid()
        {
            return true;
        }

        public override void Start()
        {
        }

        public override void Wait()
        {
        }
    }
}
