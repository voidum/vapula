using System.Collections.Generic;

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

        #region 索引器
        /// <summary>
        /// 根据指定标识获取节点
        /// </summary>
        public Node this[int id]
        {
            get
            {
                foreach (Node node in _Nodes)
                    if (node.Id == id) return node;
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
                List<int> ids = new List<int>();
                foreach (Node node in _Nodes)
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
                Stage stage = new Stage(this);
                foreach (Node node in Nodes)
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
            Node node = this[location.NodeId];
            if (node != null)
                return node[location.ParamId];
            return null;
        }
        #endregion
    }
}
