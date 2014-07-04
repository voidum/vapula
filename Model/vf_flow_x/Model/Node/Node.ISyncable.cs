using Vapula.Model;

namespace Vapula.Flow
{
    public abstract partial class Node : ISyncable
    {
        protected ISyncable _SyncTarget;

        /// <summary>
        /// 获取或设置同步目标
        /// </summary>
        public ISyncable SyncTarget
        {
            get { return _SyncTarget; }
            set { _SyncTarget = value; }
        }

        public virtual object Sync(string cmd, object attach)
        {
            if (cmd == "get-id")
            {
                return _Id;
            }
            return null;
        }
    }
}
