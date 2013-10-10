using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace DecisionTreeUI.Models
{
    public enum NodeType
    {
        Class,
        Judge
    }

    public class NodeBase
    {
        protected int _Id;
        protected int _InheritId;
        protected NodeType _Type;
        protected string _Name;
        private bool _IsYes;

        public static int GetNewId(List<int> ids)
        {
            if (ids.Count <= 0) return 0;
            ids.Sort();
            int tmp = 0;
            for (int i = 0; i < ids.Count; i++)
            {
                if (tmp != ids[i]) return tmp;
                tmp++;
            }
            return ids.Count;
        }

        public static NodeBase GetNodeById(int id,List<NodeBase> nodes) 
        {
            if (id < 0) return null;
            foreach (NodeBase node in nodes)
                if (id == node.Id) return node;
            return null;
        }

        public static NodeBase Parse(XElement xe)
        {
            NodeBase node = null;
            NodeType type = (NodeType)(int.Parse(xe.Attribute("type").Value));
            switch (type)
            {
                case NodeType.Class:
                    string[] colorstrs = 
                        xe.Element("color").Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (colorstrs.Length != 3) return null;
                    node = new NodeClass();
                    (node as NodeClass).ClassColor =
                        Color.FromArgb(int.Parse(colorstrs[0]), int.Parse(colorstrs[1]), int.Parse(colorstrs[2]));
                    break;
                case NodeType.Judge:
                    node = new NodeJudge();
                    (node as NodeJudge).Condition = xe.Element("oexpr").Value;
                    break;
            }
            node.Id = int.Parse(xe.Attribute("id").Value);
            node.Name = xe.Element("name").Value;
            return node;
        }

        public virtual XElement ToXml()
        {
            XElement xe = new XElement("node",
                new XAttribute("id", _Id),
                new XAttribute("nl", NodeLeft == null ? "-1" : NodeLeft.Id.ToString()),
                new XAttribute("nr", NodeRight == null ? "-1" : NodeRight.Id.ToString()),
                new XAttribute("type", (uint)_Type),
                new XElement("name",new XCData(_Name)));
            return xe;
        }

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int InheritId 
        {
            get { return _InheritId; }
            set { _InheritId = value; }
        }

        public bool IsYes
        {
            get { return _IsYes; }
            set { _IsYes = value; }
        }

        public NodeType Type 
        {
            get { return _Type; }
        }

        public string Name
        {
            get { return _Name; }
            set 
            {
                if (string.IsNullOrEmpty(value)) _Name = _Id.ToString();
                else _Name = value;
            }
        }

        public virtual NodeBase NodeLeft
        {
            get { return null; }
            set { return; }
        }

        public virtual NodeBase NodeRight
        {
            get { return null; }
            set { return; }
        }

        public virtual List<NodeBase> ChildNodes 
        {
            get { return new List<NodeBase>(); }
        }
    }
}
