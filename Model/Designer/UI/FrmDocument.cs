using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmDocument : DockContent
    {
        private Canvas _Canvas = null;

        private void FormLayout_AlignCenter()
        {
            _Canvas.Left = Width > _Canvas.Width ? (Width - _Canvas.Width) / 2 : 0;
            _Canvas.Top = Height > _Canvas.Height ? (Height - _Canvas.Height) / 2 : 0;
        }

        public FrmDocument()
        {
            InitializeComponent();
            _Canvas = new CanvasGraph(400, 300);
            Controls.Add(_Canvas);
        }

        private void FrmDocument_SizeChanged(object sender, System.EventArgs e)
        {
            FormLayout_AlignCenter();
        }
    }
}
