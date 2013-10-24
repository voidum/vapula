using System.Collections.Generic;
using System.Windows.Forms;
using xDockPanel;

namespace TCM.Model.Designer
{
    public partial class FrmProperty : DockContent
    {
        public FrmProperty()
        {
            InitializeComponent();
            SelectObject(null);
        }

        private GroupBox FormLayout_CreateGroupBox()
        {
            GroupBox grp = new GroupBox();
            grp.Dock = DockStyle.Top;
            grp.Padding = new Padding(5, 5, 5, 10);
            grp.AutoSize = true;
            return grp;
        }

        public void SelectObject(object obj)
        {
            Controls.Clear();
            bool need_ctrl_null = true;
            if (obj is NodeProcess)
            {
                NodeProcess node = obj as NodeProcess;
                if (node.Function.Parameters.Count > 0)
                {
                    List<string> catalogs = node.Function.Catalogs;
                    foreach (string catalog in catalogs)
                    {
                        GroupBox grp = FormLayout_CreateGroupBox();
                        grp.Text = catalog;
                        Controls.Add(grp);
                    }
                    foreach (var stub in node.ParamStubs)
                    {
                        UctParameter ctrl = new UctParameter(stub);
                        ctrl.Dock = DockStyle.Top;
                        foreach (Control grp in Controls)
                            if (stub.Prototype.Catalog == grp.Text)
                                grp.Controls.Add(ctrl);
                    }
                    need_ctrl_null = false;
                }
            }
            if (need_ctrl_null) 
            {
                UctNull ctrl_null = new UctNull();
                ctrl_null.Dock = DockStyle.Fill;
                Controls.Add(ctrl_null);
            }
        }
    }
}
