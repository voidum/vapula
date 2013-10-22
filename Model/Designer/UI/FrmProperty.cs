using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmProperty : DockContent
    {
        public FrmProperty()
        {
            InitializeComponent();
        }

        public void SelectObject(object obj)
        {
            property.SelectedObject = obj;
        }
    }
}
