using System.Collections.Generic;

namespace Vapula.Flow
{
    /// <summary>
    /// 模型图的阶段
    /// </summary>
    public partial class Stage
    {
        private int _Id = -1;
        private Graph _Parent 
            = null;
        private List<Node> _Nodes 
            = new List<Node>();

        /// <summary>
        /// 获取或设置阶段标识
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// 获取所属图
        /// </summary>
        public Graph Parent
        {
            get { return _Parent; }
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
                Stage stage = new Stage(_Parent);
                foreach (var n in Nodes)
                {
                    foreach (var n_o in n.OutNodes)
                        stage.Add(n_o);
                }
                List<Node> ns = new List<Node>();
                foreach (var n in stage.Nodes)
                {
                    foreach (var n_i in n.InNodes)
                    {
                        if (!stage.Nodes.Contains(n_i))
                            continue;
                        ns.Add(n);
                        break;
                    }
                }
                foreach (var n in ns)
                    stage.Nodes.Remove(n);
                if (stage.Nodes.Count == 0)
                    return null;
                stage.Id = Id + 1;
                return stage;
            }
        }

        public Stage(Graph parent)
        {
            _Parent = parent;
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
