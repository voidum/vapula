using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCM.Model
{
    /// <summary>
    /// 节点：变量表
    /// </summary>
    public class NodeVariable : Node
    {
        private string _Code;

        public override NodeType Type
        {
            get { return NodeType.Variable; }
        }
    }
}
