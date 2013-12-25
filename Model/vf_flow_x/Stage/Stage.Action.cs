using System.Threading;
using Vapula.Helper;
using Vapula.Flow;

namespace Vapula.Flow
{
    public partial class Stage
    {
        /// <summary>
        /// 获取图模型的公共日志器
        /// </summary>
        public ILogger Logger
        {
            get { return _Parent.Logger; }
        }

        public void Start()
        {
            Logger.WriteLog(LogType.Event,
                string.Format("阶段{0}包含{1}个节点",
                    Id, Nodes.Count));
            foreach (var node in Nodes)
                node.Start();
        }

        public void Wait()
        {
            foreach (var node in Nodes)
                node.Wait();
        }
    }
}