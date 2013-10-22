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
        public override NodeType Type
        {
            get { return NodeType.Variable; }
        }

        private string _Code;

    }
}
