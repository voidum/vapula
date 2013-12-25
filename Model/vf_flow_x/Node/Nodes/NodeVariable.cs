using System;
using System.Collections.Generic;
using Vapula.Model;

namespace Vapula.Flow
{
    /// <summary>
    /// 节点：变量
    /// </summary>
    public class NodeVariable : Node
    {
        private List<Parameter> _Parameters 
            = new List<Parameter>();

        public override NodeType Type
        {
            get { return NodeType.Variable; }
        }

        public override bool CanCustomParam
        {
            get { return true; }
        }

        public override List<Parameter> Parameters
        {
            get { return _Parameters; }
        }
        
        public override bool Valid()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Wait()
        {
            throw new NotImplementedException();
        }
    }
}
