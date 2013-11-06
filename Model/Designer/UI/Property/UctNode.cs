using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TCM.Model.Designer
{
    public partial class UctNode : UserControl
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

        private Node _Model;
        public Node Model
        {
            get { return _Model; }
            set
            {
                _Model = value;
                FormLayout_UpdateNode(value);
                FormLayout_UpdateSDN(value);
                FormLayout_UpdateParamIn(value);
                FormLayout_UpdateParamOut(value);
            }
        }

        public UctNode()
        {
            InitializeComponent();
        }

        private void FormLayout_UpdateSDN(Node node)
        {
            CobxSDNodes.Items.Clear();
            NodeItem ni = new NodeItem();
            ni.Text = "（没有强依赖节点）";
            CobxSDNodes.Items.Add(ni);
            int index = 0;
            foreach (Node n in node.InNodes)
            {
                ni = new NodeItem();
                ni.Id = n.Id;
                ni.Text = "节点" + ni.Id.ToString();
                int i = CobxSDNodes.Items.Add(ni);
                if (node.SDN != null && ni.Id == node.SDN.Id)
                    index = i;
            }
            CobxSDNodes.DisplayMember = "Text";
            CobxSDNodes.SelectedIndex = index;
        }

        private void FormLayout_UpdateNode(Node node)
        {
            LblId.Text = node.Id.ToString();
            ChbxSPP.Checked = node.SPP;
        }

        private void FormLayout_UpdateParamIn(Node node)
        {
            LsvParamDst.Items.Clear();
            LsvParamDst.Groups.Clear();
            List<string> catalogs = new List<string>();
            foreach (var stub in node.ParamStubs)
            {
                ListViewGroup lvg = null;
                Parameter param = stub.Prototype;
                if (!catalogs.Contains(param.Catalog))
                {
                    lvg = new ListViewGroup(param.Catalog);
                    LsvParamDst.Groups.Add(lvg);
                    LsvParamDst.SetGroupState(lvg, ListViewGroupState.Collapsible);
                    catalogs.Add(param.Catalog);
                }
                else
                {
                    foreach (ListViewGroup group in LsvParamDst.Groups)
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
                            param.Id.ToString(),
                            param.Name, 
                            param.Type.ToString() 
                        }, lvg);
                lvi.Tag = stub;
                LsvParamDst.Items.Add(lvi);
            }
        }

        private void FormLayout_UpdateParamOut(Node node)
        {
            LsvParamSrc.Items.Clear();
            LsvParamSrc.Groups.Clear();
            foreach (var node_in in node.InNodes)
            {
                ListViewGroup lvg = new ListViewGroup("节点" + node_in.Id);
                LsvParamSrc.Groups.Add(lvg);
                LsvParamSrc.SetGroupState(lvg, ListViewGroupState.Collapsible);

                foreach (var stub in node_in.ParamStubs)
                {
                    Parameter param = stub.Prototype;
                    if (param.IsIn) continue;
                    ListViewItem lvi = new ListViewItem(
                        new string[] {
                            param.Id.ToString(),
                            param.Name, 
                            param.Type.ToString()
                        }, lvg);
                    lvi.Tag = stub;
                    LsvParamSrc.Items.Add(lvi);
                }
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
            foreach (Node node in _Model.InNodes)
            {
                if (node.Id == ni.Id)
                {
                    _Model.SDN = node;
                    break;
                }
            }
        }

        private void LsvParamDst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LsvParamDst.SelectedItems.Count != 1)
            {
                return;
            }
        }

        private void LsvParamDst_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                return;
            ListViewItem lvi = LsvParamDst.GetItemAt(e.X, e.Y);
            if (lvi == null) return;
        }

        private void LsvParamDst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = LsvParamDst.GetItemAt(e.X, e.Y);
            if (lvi == null) return;
        }

        private void BtAdvConfig_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("");
        }

        private void UctNode_Resize(object sender, EventArgs e)
        {
            int height = Height - panel1.Height;
            LsvParamSrc.Height = (int)(height * 0.6);
        }
    }
}
