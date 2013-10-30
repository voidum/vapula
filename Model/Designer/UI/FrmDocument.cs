using System;
using System.Drawing;
using System.Windows.Forms;
using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmDocument : DockContent
    {
        private CanvasGraph _Canvas = null;
        private Graph _Graph = null;

        public Graph Graph
        {
            get { return _Graph; }
        }

        private Node FormData_GetNodeByLvi(ListViewItem lvi) 
        {
            if (AppData.Instance.FormToolbox.IsBasicTool(lvi))
            {
                string type = lvi.Tag as string;
                if (type == "decision")
                {
                    NodeDecision node_decision = new NodeDecision();
                    return node_decision;
                }
                else if (type == "variable")
                {
                    NodeVariable node_variable = new NodeVariable();
                    return node_variable;
                }
                else if (type == "batch")
                {
                    NodeBatch node_batch = new NodeBatch();
                    return node_batch;
                }
                else return null;
            }
            else
            {
                Function func = lvi.Tag as Function;
                NodeProcess node_process = new NodeProcess();
                node_process.Function = func;
                return node_process;
            }
        }

        private void FormData_BindSync(ISyncable a, ISyncable b)
        {
            a.SyncTarget = b;
            b.SyncTarget = a;
        }

        private void FormLayout_AlignCenter()
        {
            _Canvas.Left = Width > _Canvas.Width ? (Width - _Canvas.Width) / 2 : 0;
            _Canvas.Top = Height > _Canvas.Height ? (Height - _Canvas.Height) / 2 : 0;
        }

        public FrmDocument()
        {
            InitializeComponent();
            ContextMenuStrip = ctxmenubar;
            _Canvas = new CanvasGraph(400, 300);
            _Canvas.AllowDrop = true;
            _Canvas.ContextMenuStrip = ctxcanvasmenu;
            _Canvas.DragEnter += new DragEventHandler(Canvas_DragEnter);
            _Canvas.DragDrop += new DragEventHandler(Canvas_DragDrop);
            _Canvas.SelectedChanged += new Action(Canvas_SelectedItemsChanged);
            _Graph = new Graph();
            FormData_BindSync(_Canvas, _Graph);
            Controls.Add(_Canvas);
        }

        private void FrmDocument_SizeChanged(object sender, EventArgs e)
        {
            FormLayout_AlignCenter();
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            string[] fmts = e.Data.GetFormats();
            ListViewItem lvi = 
                e.Data.GetData(typeof(ListViewItem)) 
                as ListViewItem;
            if (lvi == null) return;
            Node node = FormData_GetNodeByLvi(lvi);
            if(node == null) return;
            node.Id = Graph.GetNewNodeId();
            Graph.Nodes.Add(node);

            Point p1 = new Point(e.X, e.Y);
            Point p2 = _Canvas.PointToClient(p1);

            Shape shape = null;
            if (node.Type == NodeType.Process)
                shape = _Canvas.AddShapeProcess(p2);
            else if (node.Type == NodeType.Decision)
                shape = _Canvas.AddShapeDecision(p2);
            else if (node.Type == NodeType.Variable)
                MessageBox.Show("未实现变量表");
            else if (node.Type == NodeType.Batch)
                MessageBox.Show("未实现批处理");
            if (shape != null)
                FormData_BindSync(shape, node);
        }

        private void Canvas_SelectedItemsChanged()
        {
            if (_Canvas.SelectedEntities.Count == 1)
            {
                Entity entity = _Canvas.SelectedEntities[0];
                if (entity != null) 
                {
                    AppData.Instance.FormDebug.SelectObject(entity.SyncTarget);
                    AppData.Instance.FormProperty.SelectObject(entity.SyncTarget);
                }
            }
            else
            {
                AppData.Instance.FormDebug.SelectObject(null);
                AppData.Instance.FormProperty.SelectObject(null);
            }
        }

        private void MnuDebugCanvas_Click(object sender, EventArgs e)
        {
            AppData.Instance.FormDebug.SelectObject(_Canvas);
        }

        private void MnuDebugGraph_Click(object sender, EventArgs e)
        {
            AppData.Instance.FormDebug.SelectObject(_Graph);
        }

        private void MnuDeleteSelected_Click(object sender, EventArgs e)
        {
            _Canvas.RemoveEntities(_Canvas.SelectedEntities.ToArray());
        }

        private void MnuDeleteAll_Click(object sender, EventArgs e)
        {
            _Canvas.RemoveAllEntities();
        }
    }
}
