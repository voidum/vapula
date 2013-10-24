using System.Collections.Generic;
using TCM.Helper;
using TCM.Model;

namespace TCM.Runtime
{
    public class ModelProxy
    {
        private ILogger _Logger = null;
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

        private Graph _Model;
        public Graph Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        public List<string> RuntimeIds
        {
            get
            {
                List<string> strs = new List<string>();
                foreach (Node node in _Model.Nodes)
                {
                    if (node.Type == NodeType.Process)
                    {
                        string rt = (node as NodeProcess).Function.Library.Runtime;
                        if (!strs.Contains(rt))
                            strs.Add(rt);
                    }
                }
                return strs;
            }
        }

        public ModelProxy(Graph model)
        {
            _Model = model;
        }

        public bool Start()
        {
            DriverHub driver_hub = DriverHub.Instance;
            foreach (string rt in RuntimeIds)
                driver_hub.Link(rt);

            Stage stage = _Model.FirstStage;
            while (stage != null)
            {
                Logger.WriteLog(LogType.Event,
                    string.Format("阶段{0}包含{1}个节点",
                        _Model.Stages.Count + 1,
                        stage.Nodes.Count));
                foreach (Node node in stage.Nodes)
                {
                }
                _Model.Stages.Add(stage);
                stage = stage.NextStage;
                _Model.CurrentStage = stage;
            }
            return false;
        }

        public bool Pause()
        {
            return false;
        }

        public bool Stop()
        {
            return false;
        }
    }
}
