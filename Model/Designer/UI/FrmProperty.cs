using System.Windows.Forms;
using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmProperty : DockContent
    {
        private object _SelectedObject = null;

        private UctNull _CtrlNull = null;
        private UctProcess _CtrlProcess = null; 

        public FrmProperty()
        {
            InitializeComponent();
            _CtrlNull = new UctNull();
            _CtrlProcess = new UctProcess();
            _CtrlNull.Dock = DockStyle.Fill;
            _CtrlProcess.Dock = DockStyle.Fill;
            SelectObject(null);
        }

        public void SelectObject(object obj)
        {
            if (obj == _SelectedObject && 
                _SelectedObject != null) 
                return;
            _SelectedObject = obj;
            Controls.Clear();
            if (obj is NodeProcess)
            {
                Controls.Add(_CtrlProcess);
                NodeProcess node = obj as NodeProcess;
                _CtrlProcess.Model = node;
            }
            else 
            {
                Controls.Add(_CtrlNull);
            }
        }
    }
}
