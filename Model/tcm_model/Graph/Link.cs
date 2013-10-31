using System;

namespace TCM.Model
{
    /// <summary>
    /// 模型图的关联，对应有向图的边
    /// </summary>
    public class Link : IDisposable, ISyncable
    {
        private Node _From;
        private Node _To;
        private ISyncable _SyncTarget;

        public Node From
        {
            get { return _From; }
            set
            {
                if (value == _From) return;
                if (_From != null)
                    _From.OutLinks.Remove(this);
                if (value != null)
                    value.OutLinks.Add(this);
                _From = value;
            }
        }
        internal Node QuickSetter_From
        {
            set { _From = value; }
        }

        public Node To 
        {
            get { return _To; }
            set
            {
                if (value == _To) return;
                if (_To != null)
                    _To.InLinks.Remove(this);
                if (value != null)
                    value.InLinks.Add(this);
                _To = value;
            }
        }
        internal Node QuickSetter_To
        {
            set { _To = value; }
        }

        public ISyncable SyncTarget
        {
            get { return _SyncTarget; }
            set { _SyncTarget = value; }
        }

        public object Sync(string cmd, object attach)
        {
            ISyncable target = attach as ISyncable;
            if (cmd == "detach-from")
                From = null;
            else if (cmd == "detach-to")
                To = null;
            else if (cmd == "attach-from")
            {
                Node node = target.SyncTarget as Node;
                From = node;
            }
            else if (cmd == "attach-to")
            {
                Node node = target.SyncTarget as Node;
                To = node;
            }
            return null;
        }

        public virtual void Dispose()
        {
            From = null;
            To = null;
            SyncTarget = null;
        }
    }
}
