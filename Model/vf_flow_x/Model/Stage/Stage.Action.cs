using System.Threading;
using Vapula.Helper;
using Vapula.Flow;

namespace Vapula.Flow
{
    public partial class Stage
    {
        public void Start()
        {
            /*
            throw new Exception(
                string.Format("阶段{0}包含{1}个节点",
                    Id, Nodes.Count));
             */
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