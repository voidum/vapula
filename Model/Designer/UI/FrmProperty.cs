using System.Windows.Forms;
using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmProperty : DockContent
    {
        private object _SelectedObject = null;

        private UctNull _CtrlNull = null;
        private UctNode _CtrlNode = null;

        public FrmProperty()
        {
            InitializeComponent();
            _CtrlNull = new UctNull();
            _CtrlNull.Dock = DockStyle.Fill;

            _CtrlNode = new UctNode();
            _CtrlNode.Dock = DockStyle.Fill;

            SelectObject(null);
        }

        public void SelectObject(object obj)
        {
            if (obj == _SelectedObject && 
                _SelectedObject != null) 
                return;
            _SelectedObject = obj;
            Controls.Clear();
            if (obj is Node)
            {
                Controls.Add(_CtrlNode);
                Node node = obj as Node;
                _CtrlNode.Model = node;
            }
            else 
            {
                Controls.Add(_CtrlNull);
            }
        }
    }
}
