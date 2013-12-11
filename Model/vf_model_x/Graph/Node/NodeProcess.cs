using System.Collections.Generic;

namespace TCM.Model
{
    /// <summary>
    /// 节点：处理
    /// </summary>
    public class NodeProcess : Node
    {
        private Function _Function 
            = null;

        public Function Function
        {
            get { return _Function; }
            set 
            {
                foreach (var param in value.Parameters)
                    _ParamStubs.Add(param.CreateParaStub());
                _Function = value; 
            }
        }

        public override NodeType Type
        {
            get { return NodeType.Process; }
        }

        public override object Sync(string cmd, object attach)
        {
            if (cmd == "get-icon") 
            {
                if(_Function.Tag["SmallIcon"] != null)
                    return _Function.Tag["SmallIcon"];
            }
            else if(cmd == "get-text")
                return _Function.Name;
            return base.Sync(cmd, attach);
        }
    }
}
