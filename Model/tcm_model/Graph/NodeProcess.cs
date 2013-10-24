using System;
using System.ComponentModel;

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

        public override bool IsReady
        {
            get
            {
                foreach (var stub in _ParamStubs)
                {
                    if (!stub.Prototype.IsIn) continue;
                    if (stub.HasValue) continue;
                    Link link_capture = null;
                    foreach (var link in _InLinks)
                    {
                        if (!link.HasMap(stub.Prototype.Id, false))
                            continue;
                        if (link.From != null
                            && link.From.LastStage != null)
                        {
                            link_capture = link;
                            break;
                        }
                    }
                    if (link_capture == null)
                        return false;
                }
                return true;
            }
        }
    }
}
