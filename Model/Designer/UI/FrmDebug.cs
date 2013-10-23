using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmDebug : DockContent
    {
        public FrmDebug()
        {
            InitializeComponent();
        }

        public void SelectObject(object obj)
        {
            property.SelectedObject = obj;
        }
    }
}
