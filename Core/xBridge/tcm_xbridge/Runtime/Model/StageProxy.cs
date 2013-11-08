using System.Threading;
using TCM.Helper;
using TCM.Model;

namespace TCM.Runtime
{
    /// <summary>
    /// 模型阶段的执行代理
    /// </summary>
    public class StageProxy
    {
        private ILogger _Logger = null;
        private GraphProxy _Parent = null;
        private Stage _Model = null;

        public GraphProxy Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        public Stage Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        /// <summary>
        /// 获取图模型的公共日志器
        /// </summary>
        public ILogger Logger
        {
            get
            {
                if (_Parent == null)
                    _Logger = Base.Logger;
                return _Parent.Logger;
            }
        }

        public StageProxy()
        {
        }

        public void Start()
        {
            Logger.WriteLog(LogType.Event,
                string.Format("阶段{0}包含{1}个节点",
                    Model.Id, Model.Nodes.Count));
            foreach (Node node in Model.Nodes)
            {
                if (node.Type == NodeType.Process)
                    StartNode_Process(node as NodeProcess);
                if (node.Type == NodeType.Start)
                    StartNode_Start(node as NodeStart);
            }
        }

        public void Wait()
        {
            foreach (var node in Model.Nodes)
            {
                if (node.Type == NodeType.Process)
                    WaitNode_Process(node as NodeProcess);
            }
        }

        private void StartNode_Process(NodeProcess node)
        {
            Function func = node.Function;
            Library lib = Library.Load(func.Library.Path);
            lib.Mount();
            Invoker invoker = lib.CreateInvoker(func.Id);
            node.Tag["Invoker"] = invoker;
            bool ret = invoker.Start();
            Logger.WriteLog(LogType.Event,
                string.Format("节点{0}的功能{1}启动{2}",
                    node.Id, func.Name,
                    ret ? "成功" : "失败"));
        }

        private void StartNode_Start(NodeStart node)
        {
            Logger.WriteLog(LogType.Event,
                string.Format("起点节点{0}启动",
                    node.Id));
        }

        private void WaitNode_Process(NodeProcess node)
        {
            Invoker invoker = (Invoker)node.Tag["Invoker"];
            while (invoker.Context.State != State.Idle)
                Thread.Sleep(50);
            foreach (var stub in node.ParamStubs)
            {
                Parameter param = stub.Prototype;
                if (!param.IsIn)
                {
                    Logger.WriteLog(LogType.Debug,
                        string.Format("节点{0}的参数{1}的值为{2}",
                            node.Id,
                            param.Name,
                            invoker.Envelope.Read(param.Id)));
                }
            }
        }
    }
}
