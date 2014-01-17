using System;
using System.Collections.Generic;
using Vapula.Model;

namespace Vapula.Flow
{
    /// <summary>
    /// 节点：起点
    /// </summary>
    public class NodeStart : Node
    {
        public override NodeType Type
        {
            get { return NodeType.Start; }
        }

        public override bool CanCustomParam
        {
            get { return false; }
        }

        public override List<Parameter> Parameters
        {
            get { return new List<Parameter>(); }
        }

        public override bool Valid()
        {
            if (InNodes.Count > 0)
                throw new Exception(
                    string.Format("起点节点{0}具有输入", _Id));
            return true;
        }

        public override void Start()
        {
            //throw new Exception(string.Format("起点节点{0}启动", _Id));
        }

        public override void Wait()
        {
            throw new System.NotImplementedException();
        }
    }
}
