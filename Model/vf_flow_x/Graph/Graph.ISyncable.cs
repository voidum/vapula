using Vapula.Model;

namespace Vapula.Flow
{
    public partial class Graph : ISyncable
    {
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
                target.SyncTarget = link;
                link.SyncTarget = target;
                _Links.Add(link);
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
    }
}
