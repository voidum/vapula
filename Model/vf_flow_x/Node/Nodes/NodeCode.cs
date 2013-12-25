using System.Collections.Generic;
using Vapula.Helper;
using Vapula.Model;

namespace Vapula.Flow
{
    /// <summary>
    /// 节点：代码
    /// </summary>
    public class NodeCode : Node
    {
        public override NodeType Type
        {
            get { return NodeType.Code; }
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
