using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCM.Model
{
    /// <summary>
    /// 模型图的关联，对应有向图的边
    /// </summary>
    public class Link
    {
        private Node _From;
        private Node _To;
        private bool _IsDelay = false;
        private Dictionary<int, int> _Mapping
            = new Dictionary<int, int>();

        public Node From
        {
            get { return _From; }
            set { _From = value; }
        }

        public Node To 
        {
            get { return _To; }
            set { _To = value; }
        }

        public bool IsDelay
        {
            get { return _IsDelay; }
            set { _IsDelay = value; }
        }

        public bool HasMapTo(int id)
        {
            return _Mapping.ContainsValue(id);
        }
    }
}
