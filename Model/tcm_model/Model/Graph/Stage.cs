using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCM.Model
{
    public class Stage
    {
        private List<Node> _Nodes
            = new List<Node>();

        public List<Node> Nodes
        {
            get { return _Nodes; }
        }

        public Stage NextStage
        {
            get { return null; }
        }

        public bool Add(Node node)
        {
            if (_Nodes.Contains(node))
                return false;
            _Nodes.Add(node);
            return true;
        }

        public bool Remove(Node node)
        {
            if (!_Nodes.Contains(node))
                return false;
            _Nodes.Remove(node);
            return true;
        }

        public bool Run()
        {
            return false;
        }
    }
}
