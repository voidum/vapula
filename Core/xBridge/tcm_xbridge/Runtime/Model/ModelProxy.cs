using System.Collections.Generic;
using TCM.API;
using TCM.Helper;
using TCM.Model;

namespace TCM.Runtime
{
    public class ModelProxy
    {
        private ILogger _Logger = null;
        private Graph _Model;
        private List<Stage> _Stages = new List<Stage>();

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

        public List<Stage> Stages
        {
            get { return _Stages; }
        }

        public Stage FirstStage
        {
            get
            {
                Stage stage = new Stage();
                foreach (Node node in _Model.Nodes)
                {
                    if (node.IsReady)
                        stage.Add(node);
                }
                if (stage.Nodes.Count > 0)
                    return stage;
                return null;
            }
        }

        public ModelProxy(Graph model)
        {
            _Model = model;
        }

        public bool Start()
        {
            //测试TCM Bridge可用
            try { Bridge.TestBridge(); }
            catch
            {
                Logger.WriteLog(LogType.Critical, "框架损坏，不能加载 TCM Bridge");
                return false;
            }

            //加载模型涉及的驱动
            DriverHub driver_hub = DriverHub.Instance;
            foreach (string rt in RuntimeIds)
            {
                if (!driver_hub.Link(rt))
                {
                    Logger.WriteLog(LogType.Error, "不能加载必须的驱动 " + rt);
                    return false;
                }
            }

            if (!_Model.IsValid)
            {
                Logger.WriteLog(LogType.Error, "模型没有通过有效性验证");
                return false;
            }

            Stage stage = FirstStage;
            while (stage != null)
            {
                stage.Logger = Logger;
                stage.Start();
                stage.Wait();
                Logger.WriteLog(LogType.Event,
                    string.Format("阶段{0}执行完成", stage.Id));
                stage = stage.NextStage;
                Stages.Add(stage);
            }
            Logger.WriteLog(LogType.Event, "模型执行完成");
            return true;
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
