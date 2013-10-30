using System.Collections.Generic;
using TCM.Helper;

namespace TCM.Model
{
    /// <summary>
    /// 基于有向图描述的模型图
    /// </summary>
    public class Graph : ISyncable
    {

        private List<Node> _Nodes 
            = new List<Node>();
        private List<Link> _Links 
            = new List<Link>();

        public List<Node> Nodes
        {
            get { return _Nodes; }
        }

        public List<Link> Links
        {
            get { return _Links; }
        }

        public bool IsValid
        {
            get { return true; }
        }

        public int GetNewNodeId()
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
