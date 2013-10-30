using System.Collections.Generic;
using System.Threading;
using TCM.Helper;
using TCM.Model;

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
            get 
            {
                Stage stage = new Stage();
                foreach (Node node in Nodes)
                {
                    foreach (Link link in node.OutLinks)
                        stage.Add(link.To);
                }
                if (stage.Nodes.Count > 0)
                    return stage;
                return null;
            }
        }

        public void Add(Node node)
        {
            if (!_Nodes.Contains(node))
                _Nodes.Add(node);
        }

        public void Remove(Node node)
        {
            if (_Nodes.Contains(node))
                _Nodes.Remove(node);
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
                    node.Tag = invoker;
                    bool ret = invoker.Start();
                    Logger.WriteLog(LogType.Event,
                        string.Format("节点{0}的功能{1}启动{2}",
                            node.Id, func.Name,
                            ret ? "成功" : "失败"));
                }
            }
        }

        public void Wait()
        {
            foreach (var node in _Nodes)
            {
                Invoker invoker = node.Tag as Invoker;
                while (invoker.Context.State != State.Idle)
                    Thread.Sleep(50);
                Logger.WriteLog(LogType.Debug,
                    invoker.Envelope.Read(1));
            }
        }
    }
}
