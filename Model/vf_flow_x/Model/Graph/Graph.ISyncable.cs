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
                var link = AddLink();
                target.SyncTarget = link;
                link.SyncTarget = target;
            }
            else if (cmd == "remove-link")
            {
                var link = target.SyncTarget as Link;
                _Links.Remove(link);
                link.Dispose();
            }
            else if (cmd == "remove-node")
            {
                var node = target.SyncTarget as Node;
                _Nodes.Remove(node);
                node.Dispose();
            }
            else if (cmd == "remove-all")
            {
                foreach (var node in _Nodes)
                    node.Dispose();
                _Nodes.Clear();
                foreach (var link in _Links)
                    link.Dispose();
                _Links.Clear();
            }
            return null;
        }
    }
}
