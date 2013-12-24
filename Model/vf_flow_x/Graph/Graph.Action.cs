using System.Collections.Generic;
using Vapula.API;
using Vapula.Helper;
using Vapula.Runtime;

namespace Vapula.Flow
{
    public partial class Graph
    {
        private ILogger _Logger = null;

        /// <summary>
        /// 获取或设置图模型的公共日志器
        /// </summary>
        public ILogger Logger
        {
            get { return _Logger; }
            set { _Logger = value; }
        }

        public bool LoadAllDrivers()
        {
            DriverHub driver_hub = DriverHub.Instance;
            foreach (var node in Nodes)
            {
                if (node.Type == NodeType.Process)
                {
                    var n = (node as NodeProcess);
                    string runtime = n.Function.Library.Runtime;
                    if (!driver_hub.Link(runtime))
                    {
                        Logger.WriteLog(
                            LogType.Error, 
                            "没有成功加载驱动：" + runtime);
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Valid()
        {
            if (Nodes.Count == 0) 
            {
                Logger.WriteLog(LogType.Error, "模型没有节点");
                return false;
            }
            foreach (var link in Links)
            {
                if (!link.IsReady)
                {
                    Logger.WriteLog(LogType.Warning,
                        string.Format("存在不完备的关联 [{0}] -> [{1}]",
                            link.From == null ? "空" : link.From.Id.ToString(),
                            link.To == null ? "空" : link.To.Id.ToString()));
                }
            }
            foreach (var node in Nodes)
            {
                if (!node.Valid())
                    return false;
            }
            return true; 
        }

        private void Reset() 
        {
        }

        public bool Start()
        {
            //测试Vapula Bridge可用
            try 
            {
                Bridge.TestBridge(); 
            }
            catch
            {
                Logger.WriteLog(LogType.Critical, "框架损坏，不能加载 Vapula Bridge");
                return false;
            }

            if (!LoadAllDrivers()) 
            {
                Logger.WriteLog(LogType.Error, "驱动加载失败");
                return false;
            }

            //校验模型
            if (!Valid())
            {
                Logger.WriteLog(LogType.Error, "模型没有通过有效性验证");
                return false;
            }

            //复位模型
            Reset();

            //分阶段执行模型
            Stage stage = FirstStage;
            while (stage != null)
            {
                stage.Start();
                stage.Wait();
                Logger.WriteLog(LogType.Event,
                    string.Format("阶段{0}执行完成", stage.Id));
                stage = stage.NextStage;
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
