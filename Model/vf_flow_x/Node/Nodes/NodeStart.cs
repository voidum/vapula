using Vapula.Helper;

namespace Vapula.Flow
{
    /// <summary>
    /// 节点：起点
    /// </summary>
    public class NodeStart : Node
    {
        public override NodeType Type
        {
            get { return NodeType.Start; }
        }

        public override bool Valid()
        {
            if (InNodes.Count > 0)
            {
                Logger.WriteLog(LogType.Error,
                    string.Format("起点节点{0}具有输入", _Id));
                return false;
            }
            return true;
        }

        public override void Start()
        {
            Logger.WriteLog(LogType.Event,
                string.Format("起点节点{0}启动", _Id));
        }

        public override void Wait()
        {
            throw new System.NotImplementedException();
        }
    }
}
