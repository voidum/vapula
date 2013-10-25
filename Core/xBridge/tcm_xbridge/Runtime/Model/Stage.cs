using System.Collections.Generic;
using TCM.Helper;
using TCM.Model;
using System.Threading;

namespace TCM.Runtime
{
    /// <summary>
    /// 模型执行的阶段
    /// </summary>
    public class Stage
    {
        private ILogger _Logger = null;
        private ModelProxy _Model = null;
        private int _Id = -1;
        private List<Node> _Nodes = new List<Node>();
        private List<Invoker> _Invokers = new List<Invoker>();

        public ModelProxy Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public List<Node> Nodes
        {
            get { return _Nodes; }
        }

        public Stage NextStage
        {
            get { return null; }
        }

        public bool Add(Node node)
        {
            if (_Nodes.Contains(node))
                return false;
            _Nodes.Add(node);
            return true;
        }

        public bool Remove(Node node)
        {
            if (!_Nodes.Contains(node))
                return false;
            _Nodes.Remove(node);
            return true;
        }

        /// <summary>
        /// 获取或设置图模型的公共日志器
        /// </summary>
        public ILogger Logger
        {
            get
            {
                if (_Logger == null)
                    _Logger = Base.Logger;
                return _Logger;
            }
            set
            {
                _Logger = value;
            }
        }

        public Stage()
        {
        }

        public void Start()
        {
            Logger.WriteLog(LogType.Event,
                string.Format("阶段{0}包含{1}个节点",
                    Id, Nodes.Count));
            foreach (Node node in _Nodes)
            {
                if (node is NodeProcess)
                {
                    NodeProcess node_process
                        = node as NodeProcess;
                    Function func = node_process.Function;
                    Library lib = Library.Load(func.Library.Path);
                    lib.Mount();
                    Invoker invoker = lib.CreateInvoker(func.Id);
                    _Invokers.Add(invoker);
                    bool ret = invoker.Start();
                    Logger.WriteLog(LogType.Error,
                        string.Format("节点{0}的功能{1}启动{2}",
                            node.Id, func.Name,
                            ret ? "成功" : "失败"));
                }
            }
        }

        public void Wait()
        {
            foreach (var e in _Invokers)
            {
                while (e.Context.State != State.Idle)
                    Thread.Sleep(50);
                Logger.WriteLog(LogType.Debug, 
                    e.Envelope.Read(0));
            }
        }
    }
}
