using System.Collections.Generic;
using System.Windows.Forms;
using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmProperty : DockContent
    {
        private object _SelectedObject = null;

        private void FormLayout_UpdateNodeProcess(NodeProcess node)
        {
            List<string> catalogs = new List<string>();
            foreach (var stub in node.ParamStubs)
            {
                ListViewGroup lvg = null;
                Parameter param = stub.Prototype;
                if (!catalogs.Contains(param.Catalog))
                {
                    lvg = new ListViewGroup(param.Catalog);
                    LsvParam.Groups.Add(lvg);
                    LsvParam.SetGroupState(lvg, ListViewGroupState.Collapsible);
                    catalogs.Add(param.Catalog);
                }
                else
                {
                    foreach (ListViewGroup group in LsvParam.Groups)
                    {
                        if (group.Header == param.Catalog)
                        {
                            lvg = group;
                            break;
                        }
                    }
                }
                ListViewItem lvi = new ListViewItem(
                    new string[] {
                            param.Name, 
                            param.Type.ToString(), 
                            stub.HasValue ? stub.Value : "（未设置）"
                        }, lvg);
                LsvParam.Items.Add(lvi);
            }
        }

        public FrmProperty()
        {
            InitializeComponent();
            SelectObject(null);
        }

        public void SelectObject(object obj)
        {
            if (obj == _SelectedObject) return;
            _SelectedObject = obj;
            LsvParam.Items.Clear();
            LsvParam.Groups.Clear();

            if (obj is NodeProcess)
            {
                NodeProcess node = obj as NodeProcess;
                FormLayout_UpdateNodeProcess(node);
            }
        }
    }
}
