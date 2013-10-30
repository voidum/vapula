using System;
using System.ComponentModel;
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
        private bool _Priority = false;

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

        public override bool Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        public override bool IsReady
        {
            get
            {
                //ONLY check parameter
                return true;
            }
        }

        public override object Sync(string cmd, object attach)
        {
            if (cmd == "get-icon") 
            {
                Dictionary<string, object> tags
                    = _Function.Tag as Dictionary<string, object>;
                if(tags.ContainsKey("SmallIcon"))
                    return tags["SmallIcon"];
            }
            else if(cmd == "get-text")
            {
                return _Function.Name;
            }
            else if(cmd == "get-id")
            {
                return Id.ToString();
            }
            return base.Sync(cmd, attach);
        }
    }
}
