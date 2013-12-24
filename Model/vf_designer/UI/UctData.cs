using System;
using System.Windows.Forms;
using Vapula.Flow;

namespace Vapula.Designer
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
            LsvParam.Items.Clear();
            LsvParam.Groups.Clear();
            foreach (var node in model.Nodes)
            {
                var lvg = new ListViewGroup("node_" + node.Id, "节点" + node.Id);
                lvg.Tag = node;
                LsvParam.Groups.Add(lvg);
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
                    LsvParam.Items.Add(lvi);
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            int width = Width < 800 ? 800 : Width;
            int height = Height < 450 ? 450 : Height;
            Grp1.Width = TbxDescription.Width = LsvParam.Width = width / 2 - 40;
            LsvParam.Height = height - 110;
            TbxDescription.Top = LsvParam.Bottom - 1;
            Grp1.Height = TbxDescription.Height + LsvParam.Height;
            Grp1.Left = LsvParam.Right + 25;
            ColhTargetId.Width = (int)(LsvParam.Width * 0.2);
            ColhTargetName.Width = (int)(LsvParam.Width * 0.6);
            ColhTargetType.Width = (int)(LsvParam.Width * 0.2);
            ColhSourceId.Width = (int)(LsvSupply.Width * 0.2);
            ColhSourceName.Width = (int)(LsvSupply.Width * 0.6);
            ColhSourceType.Width = (int)(LsvSupply.Width * 0.2);
        }

        private void LsvParam_SelectedIndexChanged(object sender, EventArgs e)
        {
            LsvSupply.Items.Clear();
            LsvSupply.Groups.Clear();

            if (LsvParam.SelectedItems.Count != 1)
                return;
            var lvi_target = LsvParam.SelectedItems[0];
            var stub_target = (ParamStub)lvi_target.Tag;
            var param_target = stub_target.Prototype;
            var node_target = (Node)lvi_target.Group.Tag;

            TbxDescription.Text = param_target.Description;
            ChbxExport.Checked = stub_target.IsExport;

            if (param_target.Mode == ParamMode.Out)
                return;

            var nodes = node_target.InNodes;
            foreach (var node in nodes)
            {
                var lvg = new ListViewGroup("node_" + node.Id, "节点" + node.Id);
                lvg.Tag = node_target;
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
                    LsvSupply.Items.Add(lvi);
                }
            }
        }

        private void ChbxExport_CheckedChanged(object sender, EventArgs e)
        {
            if (LsvParam.SelectedItems.Count != 1)
                return;
            var lvi_target = LsvParam.SelectedItems[0];
            var stub_target = (ParamStub)lvi_target.Tag;
            stub_target.IsExport = ChbxExport.Checked;
        }

        private void LsvSupply_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LsvParam.SelectedItems.Count != 1)
                return;
            if (LsvSupply.SelectedItems.Count != 1)
                return;
            var lvi_target = LsvParam.SelectedItems[0];
            var stub_target = (ParamStub)lvi_target.Tag;
            var param_target = stub_target.Prototype;
            var node_target = (Node)lvi_target.Group.Tag;
            var lvi_supply = LsvSupply.SelectedItems[0];
            var stub_supply = (ParamStub)lvi_supply.Tag;
            //stub_target.Supply = stub_supply.
        }
    }
}
