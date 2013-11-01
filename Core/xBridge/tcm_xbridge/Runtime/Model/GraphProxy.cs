using System.Collections.Generic;
using TCM.API;
using TCM.Helper;
using TCM.Model;

namespace TCM.Runtime
{
    public class GraphProxy
    {
        private ILogger _Logger = null;
        private Graph _Model;
        private StageProxy _StageProxy = null;

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

        public bool ValidModel()
        {
            if (_Model.Nodes.Count == 0) 
            {
                Logger.WriteLog(LogType.Error, "模型没有节点");
                return false;
            }
            foreach (Node node in _Model.Nodes)
            {
                if (!node.IsReady)
                {
                    Logger.WriteLog(LogType.Error,
                        string.Format("节点{0}的参数不完备，模型验证终止", node.Id));
                    return false;
                }
            }
            foreach (Link link in _Model.Links)
            {
                if (!link.IsReady)
                {
                    Logger.WriteLog(LogType.Error,
                        "存在不完备的关联，模型验证终止");
                    return false;
                }
            }
            return true; 
        }

        private void ClearModel() 
        {
            foreach (Node node in _Model.Nodes)
                node.LSI = 0;
        }

        public GraphProxy(Graph model)
        {
            _Model = model;
            _StageProxy = new StageProxy();
            _StageProxy.Parent = this;
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

            //校验模型
            if (!ValidModel())
            {
                Logger.WriteLog(LogType.Error, "模型没有通过有效性验证");
                return false;
            }

            //清理模型
            ClearModel();

            //分阶段执行模型
            Stage stage = Model.FirstStage;
            while (stage != null)
            {
                _StageProxy.Model = stage;
                _StageProxy.Start();
                _StageProxy.Wait();
                Logger.WriteLog(LogType.Event,
                    string.Format("阶段{0}执行完成", stage.Id));
                stage = stage.NextStage;
            }
            Logger.WriteLog(LogType.Event, "模型执行完成");
            return true;
        }

        public void Wait()
        {
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
