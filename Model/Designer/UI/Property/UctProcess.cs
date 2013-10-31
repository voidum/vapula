using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TCM.Model.Designer
{
    public partial class UctProcess : UserControl
    {
        private class NodeItem 
        {
            private int _Id;
            private string _Text;

            public int Id 
            {
                get { return _Id; }
                set { _Id = value; }
            }

            public string Text
            {
                get { return _Text; }
                set { _Text = value; }
            }
        }

        private NodeProcess _Model;
        public NodeProcess Model
        {
            get { return _Model; }
            set 
            {
                _Model = value;
                FormLayout_UpdateNode(value);
                FormLayout_UpdateSDN(value);
                FormLayout_UpdateParam(value);
            }
        }

        public UctProcess()
        {
            InitializeComponent();
        }

        private void FormLayout_UpdateSDN(NodeProcess node)
        {
            CobxSDNodes.Items.Clear();
            NodeItem ni = new NodeItem();
            ni.Text = "（没有强依赖节点）";
            CobxSDNodes.Items.Add(ni);
            int index = 0;
            foreach (Link link in node.InLinks)
            {
                ni = new NodeItem();
                ni.Id = link.From.Id;
                ni.Text = "节点" + ni.Id.ToString();
                int i = CobxSDNodes.Items.Add(ni);
                if (node.SDN != null && ni.Id == node.SDN.Id)
                    index = i;
            }
            CobxSDNodes.DisplayMember = "Text";
            CobxSDNodes.SelectedIndex = index;
        }

        private void FormLayout_UpdateNode(NodeProcess node)
        {
            LblId.Text = node.Id.ToString();
            ChbxSPP.Checked = node.SPP;
        }

        private void FormLayout_UpdateParam(NodeProcess node)
        {
            LsvParam.Items.Clear();
            LsvParam.Groups.Clear();
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

        private void ChbxSPP_CheckedChanged(object sender, EventArgs e)
        {
            _Model.SPP = ChbxSPP.Checked;
        }

        private void UctProcess_Enter(object sender, EventArgs e)
        {
            FormLayout_UpdateSDN(_Model);
        }

        private void CobxSDNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            NodeItem ni = CobxSDNodes.SelectedItem as NodeItem; 
            if (ni == null)
                return;
            if (ni.Id == 0)
                _Model.SDN = null;
            foreach (Link link in _Model.InLinks)
            {
                if (link.From.Id == ni.Id)
                {
                    _Model.SDN = link.From;
                    break;
                }
            }
        }
    }
}
