using System.Collections.Generic;

namespace TCM.Model
{
    /// <summary>
    /// 模型图的阶段
    /// </summary>
    public class Stage
    {
        private int _Id = -1;
        private List<Node> _Nodes = new List<Node>();

        /// <summary>
        /// 获取或设置阶段标识
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// 获取阶段的节点集合
        /// </summary>
        public List<Node> Nodes
        {
            get { return _Nodes; }
        }

        /// <summary>
        /// 获取下一个阶段
        /// </summary>
        public Stage NextStage
        {
            get 
            {
                if (Nodes.Count == 0) 
                    return null;
                Stage stage = new Stage();
                foreach (Node node in Nodes)
                {
                    foreach (Link l1 in node.OutLinks)
                    {
                        Node target = l1.To;
                        if (target.SDN != null)
                        {
                            int max_si = 1;
                            foreach (Link l2 in target.InLinks)
                            {
                                if (l2.From.LSI > max_si)
                                    max_si = l2.From.LSI;
                            }
                            if (target.SDN.LSI < max_si)
                                continue;
                        }
                        stage.Add(target);
                    }
                }
                if (stage.Nodes.Count == 0)
                    return null;
                stage.Id = Id + 1;
                return stage;
            }
        }

        public Stage()
        {
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        public void Add(Node node)
        {
            if (!_Nodes.Contains(node))
                _Nodes.Add(node);
        }

        /// <summary>
        /// 移除节点
        /// </summary>
        public void Remove(Node node)
        {
            if (_Nodes.Contains(node))
                _Nodes.Remove(node);
        }
    }
}
