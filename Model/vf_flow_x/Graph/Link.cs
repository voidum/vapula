using System;
using System.Xml.Linq;
using Vapula.Model;

namespace Vapula.Flow
{
    /// <summary>
    /// 模型图的关联，对应有向图的边
    /// </summary>
    public class Link : IDisposable, ISyncable
    {
        private Node _From = null;
        private Node _To = null;

        public Link() { }

        public XElement ToXML()
        {
            return null;
        }

        public Node From
        {
            get { return _From; }
            set
            {
                if (value == _From) return;
                if (_To != null)
                {
                    if (_From != null)
                    {
                        _From.OutNodes.Remove(_To);
                        _To.InNodes.Remove(_From);
                    }
                    if (value != null)
                    {
                        value.OutNodes.Add(_To);
                        _To.InNodes.Add(value);
                    }
                }
                _From = value;
            }
        }
    
        public Node To 
        {
            get { return _To; }
            set
            {
                if (value == _To) return;
                if (_From != null)
                {
                    if (_To != null)
                    {
                        _To.InNodes.Remove(_From);
                        _From.OutNodes.Remove(_To);
                    }
                    if (value != null)
                    {
                        value.InNodes.Add(_From);
                        _From.OutNodes.Add(value);
                    }
                }
                _To = value;
            }
        }

        /// <summary>
        /// <para>获取关联是否就绪</para>
        /// <para>仅用于检验关联完备</para>
        /// </summary>
        public bool IsReady 
        {
            get 
            {
                if (From == null || To == null)
                    return false;
                return true;
            }
        }

        public virtual void Dispose()
        {
            From = null;
            To = null;
            SyncTarget = null;
        }

        #region ISyncable
        private ISyncable _SyncTarget;
        
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
        #endregion
    }
}
