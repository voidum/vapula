using System;
using System.Windows.Forms;
using xDockPanel;
using System.Drawing;

namespace TCM.Model.Designer
{
    public partial class FrmDocument : DockContent
    {
        private CanvasGraph _Canvas = null;

        public Graph Graph
        {
            get 
            {
                if (_Canvas != null)
                    return _Canvas.Graph;
                return null;
            }
        }

        private void FormLayout_AlignCenter()
        {
            _Canvas.Left = Width > _Canvas.Width ? (Width - _Canvas.Width) / 2 : 0;
            _Canvas.Top = Height > _Canvas.Height ? (Height - _Canvas.Height) / 2 : 0;
        }

        public FrmDocument()
        {
            InitializeComponent();
            _Canvas = new CanvasGraph(400, 300);
            _Canvas.AllowDrop = true;
            _Canvas.DragEnter += new DragEventHandler(Canvas_DragEnter);
            _Canvas.DragDrop += new DragEventHandler(Canvas_DragDrop);
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
            string fmt_target = null;
            foreach (string fmt in fmts)
                if (fmt.Contains("TCM.Model.Node"))
                    fmt_target = fmt;
            Node node =
                e.Data.GetData(fmt_target) as Node;
            if (node == null) return;
            Graph.Nodes.Add(node);
            Point p1 = new Point(e.X, e.Y);
            Point p2 = _Canvas.PointToClient(p1);
            if (node.Type == NodeType.Process)
                _Canvas.AddShapeProcess(p2);
            else if (node.Type == NodeType.Decision)
                _Canvas.AddShapeDecision(p2);
        }
    }
}
