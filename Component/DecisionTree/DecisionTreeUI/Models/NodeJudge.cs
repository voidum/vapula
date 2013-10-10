using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DecisionTreeUI.Models
{
    public class NodeJudge : NodeBase
    {
        private string _Condition;
        private NodeBase _NodeYes = null;
        private NodeBase _NodeNo = null;

        public NodeJudge()
        {
            _Type = NodeType.Judge;
            _Condition = "";
        }

        public string Condition
        {
            get { return _Condition; }
            set { _Condition = value; }
        }

        public string CompiledCondition
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_Condition)) return "";
                Regex regex = new Regex("[a-zA-Z0-9]+");
                MatchCollection mc = regex.Matches(_Condition);
                List<string> ts1 = new List<string>();
                string ts1_0 = mc[0].Value;
                foreach (Match m in mc)
                {
                    double tmp;
                    if (double.TryParse(m.Value, out tmp))
                    {
                        ts1.Add(m.Value);
                        continue;
                    }
                    ts1.Add(string.Format("dv[\"{0}\"]", m.Value));
                }
                regex = new Regex("[^a-zA-Z0-9]+");
                mc = regex.Matches(_Condition);
                List<string> ts2 = new List<string>();
                foreach (Match m in mc) ts2.Add(m.Value);

                StringBuilder sb = new StringBuilder();
                int i = 0, j = 0;
                bool cur_token_is_var = (ts2.Count == 0) || (_Condition.IndexOf(ts1_0) < _Condition.IndexOf(ts2[0]));
                while (i < ts1.Count || j < ts2.Count)
                {
                    if (cur_token_is_var)
                    {
                        sb.Append(ts1[i++]);
                        cur_token_is_var = false;
                    }
                    else
                    {
                        sb.Append(ts2[j++]);
                        cur_token_is_var = true;
                    }
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Yes节点
        /// </summary>
        public override NodeBase NodeLeft
        {
            get { return _NodeYes; }
            set { _NodeYes = value; }
        }

        /// <summary>
        /// No节点
        /// </summary>
        public override NodeBase NodeRight
        {
            get { return _NodeNo; }
            set { _NodeNo = value; }
        }

        public override List<NodeBase> ChildNodes
        {
            get
            {
                List<NodeBase> nodes = new List<NodeBase>();
                Stack<NodeBase> stack = new Stack<NodeBase>();
                NodeBase node = this;
                while (node != null || stack.Count > 0)
                {
                    if (node != null)
                    {
                        if(node != this) nodes.Add(node);
                        stack.Push(node);
                        node = node.NodeLeft;
                    }
                    else
                    {
                        node = stack.Pop();
                        node = node.NodeRight;
                    }
                }
                return nodes;
            }
        }

        public override XElement ToXml()
        {
            XElement xe = base.ToXml();
            xe.Add(new XElement("expr",
                new XCData(CompiledCondition)));
            xe.Add(new XElement("oexpr",
                new XCData(Condition)));
            return xe;
        }
    }
}
