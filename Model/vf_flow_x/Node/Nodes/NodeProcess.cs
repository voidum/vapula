using System.Threading;
using Vapula.Helper;
using Vapula.Model;
using Vapula.Runtime;

namespace Vapula.Flow
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
                //foreach (var param in value.Parameters)
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
                if(_Function.Attach["SmallIcon"] != null)
                    return _Function.Attach["SmallIcon"];
            }
            else if(cmd == "get-name")
                return _Function.Name;
            return base.Sync(cmd, attach);
        }

        public override bool Valid()
        {
            return true;
        }

        public override void Start()
        {
            var lib = Runtime.Library.Load(_Function.Library.Path);
            lib.Mount();
            var invoker = lib.CreateInvoker(_Function.Id);
            Attach["Invoker"] = invoker;
            bool ret = invoker.Start();
            Logger.WriteLog(LogType.Event,
                string.Format("节点{0}的功能{1}启动",
                    _Id, _Function.Name,
                    ret ? "成功" : "失败"));
        }

        public override void Wait()
        {
            var invoker = (Invoker)Attach["Invoker"];
            while (invoker.Context.State != State.Idle)
                Thread.Sleep(50);
            foreach (var stub in ParamStubs)
            {
                /*
                if (param.Mode != ParamMode.In)
                {
                    Logger.WriteLog(LogType.Debug,
                        string.Format("节点{0}的参数{1}的值为{2}",
                            node.Id,
                            param.Name,
                            invoker.Envelope.Read(param.Id)));
                }
                 */
            }
        }
    }
}
