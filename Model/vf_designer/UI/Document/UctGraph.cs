using System;
using System.Drawing;
using System.Windows.Forms;
using Vapula.Flow;
using Vapula.Model;

namespace Vapula.Designer
{
    public partial class UctGraph : UserControl
    {
        private AppData _App = AppData.Instance;
        private Graph _Graph = null;
        private CanvasGraph _Canvas = null;

        public Graph Graph
        {
            get { return _Graph; }
        }

        public CanvasGraph Canvas 
        {
            get { return _Canvas; }
        }

        private Node Data_GetNodeByLvi(ListViewItem lvi)
        {
            if (_App.FormToolbox.IsAdvancedTool(lvi))
            {
                string type = lvi.Tag as string;
                Node node = null;
                if (type == "decision")
                    node = new NodeDecision();
                else if(type == "start")
                    node = new NodeStart();
                else if (type == "variable")
                    node = new NodeVariable();
                else if (type == "batch")
                    node = new NodeBatch();
                else if (type == "code")
                    node = new NodeCode();
                else 
                    return null;
                node.Parent = _Graph;
                return node;
            }
            else
            {
                var func = lvi.Tag as Function;
                var node_process = new NodeProcess();
                node_process.Parent = _Graph;
                node_process.Function = func;
                return node_process;
            }
        }

        private void Data_BindSync(ISyncable a, ISyncable b)
        {
            a.SyncTarget = b;
            b.SyncTarget = a;
        }

        private void UI_AlignCenter()
        {
            _Canvas.Left = Width > _Canvas.Width ? (Width - _Canvas.Width) / 2 : 0;
            _Canvas.Top = Height > _Canvas.Height ? (Height - _Canvas.Height) / 2 : 0;
        }

        public UctGraph()
        {
            InitializeComponent();
            _Canvas = new CanvasGraph(400, 300);
            _Canvas.AllowDrop = true;
            _Canvas.ContextMenuStrip = ctxmenu_canvas;

            _Canvas.DragEnter += new DragEventHandler(Canvas_DragEnter);
            _Canvas.DragDrop += new DragEventHandler(Canvas_DragDrop);
            _Canvas.SelectedChanged += new Action(Canvas_SelectedItemsChanged);
            _Canvas.MouseDoubleClick += new MouseEventHandler(Canvas_MouseDoubleClick);

            Controls.Add(_Canvas);

            _Graph = new Graph();
            Data_BindSync(_Canvas, _Graph);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if(_Canvas != null)
                UI_AlignCenter();
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            string[] fmts = e.Data.GetFormats();
            var lvi = 
                e.Data.GetData(typeof(ListViewItem)) 
                as ListViewItem;
            if (lvi == null) 
                return;
            var node = Data_GetNodeByLvi(lvi);

            Point p1 = new Point(e.X, e.Y);
            Point p2 = _Canvas.PointToClient(p1);

            Shape shape = null;
            if (node.Type == NodeType.Process)
                shape = _Canvas.AddShapeProcess(p2);
            else if (node.Type == NodeType.Decision)
                shape = _Canvas.AddShapeDecision(p2);
            else if (node.Type == NodeType.Start)
                shape = _Canvas.AddShapeStart(p2);
            else if (node.Type == NodeType.Variable)
                MessageBox.Show("未实现变量表");
            else if (node.Type == NodeType.Batch)
                MessageBox.Show("未实现批处理");
            else if (node.Type == NodeType.Code)
                MessageBox.Show("未实现代码");
            if (shape != null)
                Data_BindSync(shape, node);
        }

        private void Canvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_Canvas.SelectedEntities.Count == 1)
            {
                var entity = _Canvas.SelectedEntities[0];
                if (!(entity is Shape)) return;
                var node = entity.SyncTarget as Node;
                MessageBox.Show(node.Id.ToString());
            }
        }

        private void Canvas_SelectedItemsChanged()
        {
            if (_Canvas.SelectedEntities.Count == 1)
            {
                var entity = _Canvas.SelectedEntities[0];
                if (entity != null)
                    AppData.Instance.FormDebug.SelectObject(entity.SyncTarget);
            }
            else
            {
                AppData.Instance.FormDebug.SelectObject(null);
            }
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
