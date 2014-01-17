using System;
using Vapula.API;
using Vapula.Helper;
using Vapula.Runtime;

namespace Vapula.Flow
{
    public partial class Graph
    {
        public bool LoadAllDrivers()
        {
            var driver_hub = DriverHub.Instance;
            foreach (var node in Nodes)
            {
                if (node.Type == NodeType.Process)
                {
                    var n = (node as NodeProcess);
                    string runtime = n.Function.Library.Runtime;
                    if (!driver_hub.Link(runtime))
                        throw new Exception("无法加载驱动" + runtime);
                }
            }
            return true;
        }

        public bool Valid()
        {
            if (Nodes.Count == 0) 
                throw new Exception("模型没有节点");
            foreach (var link in Links)
            {
                if (!link.IsReady)
                {
                    throw new Exception(
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
                string ver = Bridge.MarshalString(
                    Bridge.GetVersion(), false); 
            }
            catch
            {
                throw new Exception("框架损坏");
            }

            if (!LoadAllDrivers()) 
                throw new Exception("驱动加载失败");

            //校验模型
            if (!Valid())
                throw new Exception("模型没有通过有效性验证");

            //复位模型
            Reset();

            //分阶段执行模型
            Stage stage = FirstStage;
            while (stage != null)
            {
                stage.Start();
                stage.Wait();
                //throw new Exception(string.Format("阶段{0}执行完成", stage.Id));
                stage = stage.NextStage;
            }
            //throw new Exception("模型执行完成");
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
