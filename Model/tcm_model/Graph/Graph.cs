using System.Collections.Generic;

namespace TCM.Model
{
    /// <summary>
    /// 基于有向图描述的模型图
    /// </summary>
    public class Graph : ISyncable
    {
        private List<Node> _Nodes 
            = new List<Node>();
        private List<Stage> _Stages 
            = new List<Stage>();
        private List<Link> _Links 
            = new List<Link>();

        /// <summary>
        /// 获取图的节点集合
        /// </summary>
        public List<Node> Nodes
        {
            get { return _Nodes; }
        }

        /// <summary>
        /// 获取图的关联集合
        /// </summary>
        public List<Link> Links
        {
            get { return _Links; }
        }

        /// <summary>
        /// 获取图的阶段集合
        /// </summary>
        public List<Stage> Stages
        {
            get { return _Stages; }
        }

        /// <summary>
        /// 获取新的节点标识
        /// </summary>
        public int NewNodeId
        {
            get
            {
                List<int> ids = new List<int>();
                foreach (Node node in _Nodes)
                    ids.Add(node.Id);
                ids.Sort();
                for (int i = 1; i <= ids.Count; i++)
                    if (i != ids[i - 1])
                        return i;
                return ids.Count + 1;
            }
        }

        /// <summary>
        /// 获取起始阶段
        /// </summary>
        public Stage FirstStage
        {
            get
            {
                Stage stage = new Stage();
                foreach (Node node in Nodes)
                {
                    if (node.SPP || node.InLinks.Count == 0)
                        stage.Add(node);
                }
                if (stage.Nodes.Count == 0)
                    return null;
                stage.Id = 1;
                return stage;
            }
        }

        #region ISyncable
        private ISyncable _SyncTarget = null;

        public ISyncable SyncTarget
        {
            get { return _SyncTarget; }
            set { _SyncTarget = value; }
        }

        public object Sync(string cmd, object attach)
        {
            ISyncable target = attach as ISyncable;
            if (cmd == "add-link")
            {
                Link link = new Link();
                _Links.Add(link);
                target.SyncTarget = link;
                link.SyncTarget = target;
            }
            else if (cmd == "remove-link")
            {
                Link link = target.SyncTarget as Link;
                link.Dispose();
                _Links.Remove(link);
            }
            else if (cmd == "remove-node")
            {
                Node node = target.SyncTarget as Node;
                node.Dispose();
                _Nodes.Remove(node);
            }
            else if (cmd == "remove-all")
            {
                foreach (Node node in _Nodes)
                    node.Dispose();
                _Nodes.Clear();
                foreach (Link link in _Links)
                    link.Dispose();
                _Links.Clear();
            }
            return null;
        }
        #endregion
    }
}
