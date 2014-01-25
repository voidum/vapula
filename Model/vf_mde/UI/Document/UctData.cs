using System;
using System.Windows.Forms;
using Vapula.Flow;

namespace Vapula.MDE
{
    public partial class UctData : UserControl
    {
        public UctData()
        {
            InitializeComponent();
        }

        public void UI_UpdateData(Graph model)
        {
            LsvSupply.Items.Clear();
            LsvSupply.Groups.Clear();
            LsvTarget.Items.Clear();
            LsvTarget.Groups.Clear();
            foreach (var node in model.Nodes)
            {
                var lvg = new ListViewGroup("node_" + node.Id, "节点" + node.Id);
                LsvTarget.Groups.Add(lvg);
                foreach (var stub in node.ParamStubs)
                {
                    var param = stub.Prototype;
                    var lvi = new ListViewItem(
                        new string[] {
                            param.Id.ToString(), 
                            param.Name, 
                            param.Type.ToString() },
                            lvg);
                    lvi.Tag = stub;
                    LsvTarget.Items.Add(lvi);
                }
            }
            TbxDescription.Text = "";
            ChbxOptional.Enabled = false;
            BtSetValue.Enabled = false;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            int width = Width < 800 ? 800 : Width;
            int height = Height < 450 ? 450 : Height;
            Grp2.Width = Grp1.Width = width / 2 - 40;
            Grp2.Left = Grp1.Right + 25;
            Grp2.Height = Grp1.Height = height - 50;
            BtSetValue.Left = LsvSupply.Right - BtSetValue.Width;
            BtSetValue.Top = ChbxOptional.Top;
            ColhTargetId.Width = (int)(LsvTarget.Width * 0.15);
            ColhTargetName.Width = (int)(LsvTarget.Width * 0.65);
            ColhTargetType.Width = (int)(LsvTarget.Width * 0.2);
            ColhSupplyId.Width = (int)(LsvSupply.Width * 0.15);
            ColhSupplyName.Width = (int)(LsvSupply.Width * 0.65);
            ColhSupplyType.Width = (int)(LsvSupply.Width * 0.2);
        }

        private void LsvTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            LsvSupply.Items.Clear();
            LsvSupply.Groups.Clear();

            if (LsvTarget.SelectedItems.Count != 1)
                return;
            var lvi_target = LsvTarget.SelectedItems[0];
            var stub_target = lvi_target.Tag as ParamStub;
            var param_target = stub_target.Prototype;
            var node_target = stub_target.Parent;

            TbxDescription.Text = param_target.Description;
            ChbxExport.Checked = stub_target.IsExport;
            ChbxOptional.Checked = stub_target.IsOptional;

            if (param_target.Mode == ParamMode.Out)
                return;

            var nodes = node_target.InNodes;
            foreach (var node in nodes)
            {
                var lvg = new ListViewGroup("node_" + node.Id, "节点" + node.Id);
                LsvSupply.Groups.Add(lvg);
                foreach (var stub in node.ParamStubs)
                {
                    var param = stub.Prototype;
                    if (param.Mode == ParamMode.In)
                        continue;
                    var lvi = new ListViewItem(
                        new string[] {
                            param.Id.ToString(), 
                            param.Name, 
                            param.Type.ToString() },
                            lvg);
                    lvi.Tag = stub;
                    if (stub_target.Supply == stub.Self)
                        lvi.Checked = true;
                    LsvSupply.Items.Add(lvi);
                }
            }
        }

        private void ChbxExport_CheckedChanged(object sender, EventArgs e)
        {
            if (LsvTarget.SelectedItems.Count != 1)
                return;
            var lvi_target = LsvTarget.SelectedItems[0];
            var stub_target = lvi_target.Tag as ParamStub;
            if (ChbxExport.Checked)
            {
                stub_target.IsExport = true;
                LsvSupply.Enabled = false;
                ChbxOptional.Enabled = true;
            }
            else
            {
                stub_target.IsExport = false;
                LsvSupply.Enabled = true;
                ChbxOptional.Enabled = false;
                ChbxOptional.Checked = false;
            }
        }

        private void ChbxOptional_CheckedChanged(object sender, EventArgs e)
        {
            if (LsvTarget.SelectedItems.Count != 1)
                return;
            var lvi_target = LsvTarget.SelectedItems[0];
            var stub_target = lvi_target.Tag as ParamStub;
            if(ChbxOptional.Checked)
            {
                stub_target.IsOptional = true;
                BtSetValue.Enabled = true;
            }
            else
            {
                stub_target.IsOptional = false;
                BtSetValue.Enabled = false;
                stub_target.Value = null;
            }
        }

        private void LsvSupply_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (LsvSupply.Items.Count == 0)
                return;
            if (LsvTarget.SelectedItems.Count != 1)
                return;
            var lvi_target = LsvTarget.SelectedItems[0];
            var stub_target = lvi_target.Tag as ParamStub;
            var stub_supply = e.Item.Tag as ParamStub;

            if (e.Item.Checked)
            {
                foreach (ListViewItem lvi in LsvSupply.Items)
                {
                    if (lvi != e.Item)
                        lvi.Checked = false;
                }
                stub_target.Supply = stub_supply.Self;
            }
            else
            {
                if (stub_target.Supply != stub_supply.Self)
                    return;
                stub_target.Supply = ParamPoint.Null;
            }
        }

        private void BtSetValue_Click(object sender, EventArgs e)
        {
            if (LsvTarget.SelectedItems.Count != 1)
                return;
            var lvi_target = LsvTarget.SelectedItems[0];
            var stub_target = lvi_target.Tag as ParamStub;
            var dlg = new FrmParamValue(stub_target.Prototype.Type);
            dlg.Value = stub_target.Value;
            if (dlg.ShowDialog() == DialogResult.OK)
                stub_target.Value = dlg.Value;
        }
    }
}
