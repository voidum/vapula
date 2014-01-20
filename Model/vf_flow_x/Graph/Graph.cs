using System.Collections.Generic;
using System.Xml.Linq;

namespace Vapula.Flow
{
    /// <summary>
    /// 基于有向图描述的模型图
    /// </summary>
    public partial class Graph
    {
        #region 字段
        private List<Node> _Nodes 
            = new List<Node>();
        private List<Link> _Links 
            = new List<Link>();
        #endregion

        #region 构造
        public Graph() 
        {
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 加载模型图
        /// </summary>
        public static Graph Load(string path)
        {
            var graph = new Graph();
            return graph;
        }

        public XElement ToXML()
        {
            var xml_graph = new XElement("graph");
            var xml_nodes = new XElement("nodes");
            var xml_links = new XElement("nodes");
            foreach (var node in _Nodes)
                xml_nodes.Add(node.ToXML());
            foreach (var link in _Links)
                xml_links.Add(link.ToXML());
            xml_graph.Add(xml_nodes);
            xml_graph.Add(xml_links);
            return xml_graph;
        }
        #endregion

        #region 索引器
        /// <summary>
        /// 根据指定标识获取节点
        /// </summary>
        public Node this[int id]
        {
            get
            {
                foreach (var node in _Nodes)
                    if (node.Id == id) 
                        return node;
                return null;
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取图的节点集合
        /// </summary>
        public List<Node> Nodes
        {
            get { return _Nodes; }
        }

        /// <summary>
        /// 获取图的关联集合
        /// </summary>
        public List<Link> Links
        {
            get { return _Links; }
        }

        /// <summary>
        /// 获取新的节点标识
        /// </summary>
        public int NewNodeId
        {
            get
            {
                var ids = new List<int>();
                foreach (var node in _Nodes)
                    ids.Add(node.Id);
                ids.Sort();
                for (int i = 1; i <= ids.Count; i++)
                    if (i != ids[i - 1])
                        return i;
                return ids.Count + 1;
            }
        }

        /// <summary>
        /// 获取起始阶段
        /// </summary>
        public Stage FirstStage
        {
            get
            {
                var stage = new Stage(this);
                foreach (var node in Nodes)
                {
                    if (node.InNodes.Count == 0)
                        stage.Add(node);
                }
                if (stage.Nodes.Count == 0)
                    return null;
                stage.Id = 1;
                return stage;
            }
        }
        #endregion

        #region 集合
        /// <summary>
        /// 根据坐标检索参数存根
        /// </summary>
        public ParamStub FindParamStub(ParamPoint location)
        {
            var node = this[location.NodeId];
            if (node != null)
                return node[location.ParamId];
            return null;
        }

        public Link AddLink()
        {
            var link = new Link();
            _Links.Add(link);
            return link;
        }
        #endregion
    }
}
