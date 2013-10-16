using System.Collections.Generic;

namespace TCM.Model
{
    /// <summary>
    /// 基于有向图描述的模型图
    /// </summary>
    public class Graph
    {
        private List<Node> _Nodes 
            = new List<Node>();
        private List<Link> _Links 
            = new List<Link>();
        private List<Stage> _Stages
            = new List<Stage>();
        private Stage _CurrentStage = null;

        public List<Node> Nodes
        {
            get { return _Nodes; }
        }

        public Stage FirstStage
        {
            get
            {
                Stage stage = new Stage();
                foreach (Node node in _Nodes)
                {
                    if (node.Executable)
                        stage.Add(node);
                }
                return stage;
            }
        }

        public Stage CurrentStage
        {
            get { return _CurrentStage; }
            set { _CurrentStage = value; }
        }

        public bool Start()
        {
            Stage stage = FirstStage;
            while (stage != null)
            {
                stage.Run();
                _Stages.Add(stage);
                stage = stage.NextStage;
                CurrentStage = stage;
            }
            return false;
        }

        public bool Pause() 
        {
            return false;
        }

        public bool Stop()
        {
            return false;
        }
    }
}
