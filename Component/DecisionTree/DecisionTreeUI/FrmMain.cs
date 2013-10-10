using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using DecisionTreeUI.Models;
using TCM.Runtime;

namespace DecisionTreeUI
{
    public partial class FrmMain : Form
    {
        private void InitLang()
        {
            Text = AppData.LangPack["FrmMain"];
            MnuFile.Text = AppData.LangPack["Menu_File"];
            MnuFile_New.Text = AppData.LangPack["Menu_File_New"];
            MnuFile_Open.Text = AppData.LangPack["Menu_File_Open"];
            MnuFile_Save.Text = AppData.LangPack["Menu_File_Save"];
            MnuFile_Close.Text = AppData.LangPack["Menu_File_Close"];
            MnuAction.Text = AppData.LangPack["Menu_Action"];
            MnuAction_ConfigData.Text = AppData.LangPack["Menu_Action_ConfigData"];
            MnuAction_Execute.Text = AppData.LangPack["Menu_Action_Execute"];
            BtSplit.Text = AppData.LangPack["Split"];
            BtMerge.Text = AppData.LangPack["Merge"];
            BtProperty.Text = AppData.LangPack["BtProperty"];
            BtAddVar.Text = AppData.LangPack["BtAddVar"];
            BtRemoveVar.Text = AppData.LangPack["BtRemoveVar"];
            ColhVarName.Text = AppData.LangPack["Var"];
        }

        private void SaveDecisionTree(string path)
        {
            XDocument xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("root",
                    new XElement("nodes")));
            XElement xe = xdoc.Element("root").Element("nodes");
            foreach (NodeBase node in AppData.Nodes)
                xe.Add(node.ToXml());
            XElement xvars = new XElement("vars");
            foreach (Mapping mapping in AppData.Mappings)
                xvars.Add(new XElement("var",mapping.Name));
            xdoc.Element("root").Add(xvars);
            xdoc.Save(path);
        }

        private void SaveDataDescriptor(string path) 
        {
            XDocument xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("root",
                    new XElement("output",
                        new XElement("file",
                            new XCData(AppData.OutDir + "\\" + AppData.OutName)),
                        new XElement("format", AppData.OutFmt)),
                    new XElement("mappings")));
            XElement xemap = xdoc.Element("root").Element("mappings");
            foreach (Mapping mapping in AppData.Mappings)
            {
                XElement xe = new XElement("mapping",
                    new XElement("name",
                        new XCData(mapping.Name)),
                    new XElement("file",
                        new XCData(mapping.File)),
                    new XElement("band",mapping.BandIndex));
                xemap.Add(xe);
            }
            xdoc.Save(path);
        }

        private void FormLayout_SetNodeText(TreeNode tn) 
        {
            NodeBase node = tn.Tag as NodeBase;
            if (node == null) return;
            if(node.Id != 0)
                tn.Text = string.Format("{0}[{1}]", node.Name, node.IsYes ? "Yes" : "No");
            else
                tn.Text = node.Name;
        }

        private TreeNode RestoreNode(NodeBase node)
        {
            TreeNode tn = new TreeNode();
            tn.Tag = node;
            FormLayout_SetNodeText(tn);
            switch (node.Type) 
            {
                case NodeType.Class:
                    tn.ImageIndex = 1;
                    tn.BackColor = (node as NodeClass).ClassColor;
                    break;
                case NodeType.Judge:
                    tn.ImageIndex = 0;
                    tn.BackColor = Color.Transparent;
                    break;
                default: throw new Exception("无法恢复不规范的节点。");
            }
            return tn;
        }

        /// <summary>
        /// <para>决策树变迁与分化</para>
        /// <para>不提供母本，将重新构建</para>
        /// </summary>
        private TreeNode CreateNode(NodeType type, bool isyes, TreeNode tn = null)
        {
            bool hasprev = false;
            NodeBase node = null;
            //去除历史节点，并保证完备
            if (tn == null) tn = new TreeNode();
            else
            {
                tn.Nodes.Clear();
                node = tn.Tag as NodeBase;
                List<NodeBase> nodes = node.ChildNodes;
                foreach (NodeBase n in nodes) AppData.Nodes.Remove(n);
                if (tn.Parent != null) hasprev = true;
                AppData.Nodes.Remove(node);
            }

            //构建新节点
            switch (type)
            {
                case NodeType.Class:
                    NodeClass nc = new NodeClass();
                    nc.Id = NodeBase.GetNewId(AppData.NodeIds);
                    nc.InheritId = NodeBase.GetNewId(AppData.NodeIdsClass);
                    nc.Name = AppData.LangPack["Class"] + (nc.InheritId + 1).ToString();
                    nc.ClassColor = NodeClass.GetNewColor(AppData.NodeColors);
                    tn.ImageIndex = 1;
                    tn.Tag = nc;
                    tn.BackColor = nc.ClassColor;
                    AppData.Nodes.Add(nc);
                    break;
                case NodeType.Judge:
                    NodeJudge nj = new NodeJudge();
                    nj.Id = NodeBase.GetNewId(AppData.NodeIds);
                    nj.InheritId = NodeBase.GetNewId(AppData.NodeIdsJudge);
                    nj.Name = AppData.LangPack["Judge"] + (nj.InheritId + 1).ToString();
                    tn.ImageIndex = 0;
                    tn.Tag = nj;
                    tn.BackColor = Color.Transparent;
                    AppData.Nodes.Add(nj);
                    TreeNode tn1 = CreateNode(NodeType.Class, true);
                    TreeNode tn2 = CreateNode(NodeType.Class, false);
                    tn.Nodes.Add(tn1);
                    tn.Nodes.Add(tn2);
                    nj.NodeLeft = tn1.Tag as NodeBase;
                    nj.NodeRight = tn2.Tag as NodeBase;
                    break;
                default: throw new Exception("无法生成不规范的节点。");
            }
            node = tn.Tag as NodeBase;
            node.IsYes = isyes;
            FormLayout_SetNodeText(tn);

            if (hasprev) //保证前后关联完备
            {
                NodeBase nprev = tn.Parent.Tag as NodeBase;
                if (isyes) nprev.NodeLeft = tn.Tag as NodeBase;
                else nprev.NodeRight = tn.Tag as NodeBase;
            }
            return tn;
        }

        private void FormLayout_LoadData()
        {
            foreach (Mapping map in AppData.Mappings)
            {
                ListViewItem lvi = new ListViewItem(map.Name);
                LsvVar.Items.Add(lvi);
            }
            List<TreeNode> LstTNode = new List<TreeNode>();
            foreach (NodeBase node in AppData.Nodes)
            {
                TreeNode tn = RestoreNode(node);
                LstTNode.Add(tn);
            }
            treeview.Nodes.Clear();
            foreach (TreeNode tn in LstTNode)
            {
                NodeBase node = tn.Tag as NodeBase;
                if (node.Id == 0) treeview.Nodes.Add(tn);
                foreach (TreeNode tn2 in LstTNode) 
                {
                    if (tn2 == tn) continue;
                    NodeBase node2 = tn2.Tag as NodeBase;
                    if (node2 == node.NodeLeft || node2 == node.NodeRight)
                    {
                        tn.Nodes.Add(tn2);
                        continue;
                    }
                }
            }
            treeview.ExpandAll();
        }

        public FrmMain()
        {
            InitializeComponent();
            InitLang();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            treeview.ImageList = images;
            
            TreeNode tn = CreateNode(NodeType.Judge, false);
            treeview.Nodes.Add(tn);
            treeview.ExpandAll();
            LsvVar.Items.Clear();
        }

        private void BtSplit_Click(object sender, EventArgs e)
        {
            if (treeview.SelectedNode == null) return;
            TreeNode tn = treeview.SelectedNode;
            NodeBase node = tn.Tag as NodeBase;
            if (node.Type == NodeType.Judge)
            {
                MessageBox.Show(
                    AppData.LangPack["FrmMain_BtSplit_1"],
                    AppData.LangPack["Caution"]);
                return;
            }

            tn = CreateNode(NodeType.Judge, (tn.Tag as NodeBase).IsYes, tn);
            treeview.ExpandAll();
        }

        private void BtMerge_Click(object sender, EventArgs e)
        {
            if (treeview.SelectedNode == null) return;
            TreeNode tn = treeview.SelectedNode;
            NodeBase node = tn.Tag as NodeBase;
            if (node.Type == NodeType.Class)
            {
                MessageBox.Show(
                    AppData.LangPack["FrmMain_BtMerge_1"],
                    AppData.LangPack["Caution"]);
                return;
            }
            if (tn.Parent == null) return;
            bool isyes = true;
            if (tn.PrevNode != null) isyes = !(tn.PrevNode.Tag as NodeBase).IsYes;
            if (tn.NextNode != null) isyes = !(tn.NextNode.Tag as NodeBase).IsYes;
            tn = CreateNode(NodeType.Class, isyes, tn);
            treeview.ExpandAll();
        }

        private void treeview_DoubleClick(object sender, EventArgs e)
        {
            if (treeview.SelectedNode == null) return;
            TreeNode tn = treeview.SelectedNode;
            tn.Expand();

            NodeBase node = tn.Tag as NodeBase;
            switch (node.Type)
            {
                case NodeType.Class:
                    AppData.FormClass.Model = node as NodeClass;
                    AppData.FormClass.ShowDialog();
                    FormLayout_SetNodeText(tn);
                    tn.BackColor = AppData.FormClass.ClassColor;
                    break;
                case NodeType.Judge:
                    AppData.FormJudge.Model = node as NodeJudge;
                    AppData.FormJudge.ShowDialog();
                    FormLayout_SetNodeText(tn);
                    break;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                e.Cancel = true;
                Application.Exit();
            }
        }

        private void BtProperty_Click(object sender, EventArgs e)
        {
            treeview_DoubleClick(sender, e);
        }

        private void treeview_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }

        private void MnuFile_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MnuFile_New_Click(object sender, EventArgs e)
        {
            AppData.InitData();
            LsvVar.Items.Clear();
            treeview.Nodes.Clear();
            TreeNode tn = CreateNode(NodeType.Judge, true);
            treeview.Nodes.Add(tn);
            treeview.ExpandAll();
        }

        private void MnuClass_ConfigData_Click(object sender, EventArgs e)
        {
            AppData.FormCfgData.ShowDialog();
        }

        private void BtAddVar_Click(object sender, EventArgs e)
        {
            PIE.Controls.DlgInputText dlg = 
                new PIE.Controls.DlgInputText(
                    AppData.LangPack["FrmMain_BtAddVar_1"],
                    AppData.LangPack["FrmMain_BtAddVar_2"]);
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Regex regex = new Regex("[^a-zA-Z0-9]");
            string varname = regex.Replace(dlg.Result,"");
            regex = new Regex("^[0-9]+");
            varname = regex.Replace(varname,"");
            if (string.IsNullOrWhiteSpace(varname))
            {
                MessageBox.Show(
                    AppData.LangPack["FrmMain_BtAddVar_3"],
                    AppData.LangPack["Caution"]);
                return;
            }
            foreach (ListViewItem item in LsvVar.Items)
            {
                if (item.Text == varname)
                {
                    MessageBox.Show(
                        string.Format(AppData.LangPack["FrmMain_BtAddVar_4"], varname),
                        AppData.LangPack["Caution"]);
                    return;
                }
            }
            LsvVar.Items.Add(new ListViewItem(varname));
            Mapping mapping = new Mapping();
            mapping.Name = varname;
            AppData.Mappings.Add(mapping);
        }

        private void BtRemoveVar_Click(object sender, EventArgs e)
        {
            if (LsvVar.SelectedItems.Count <= 0) return;
            foreach (ListViewItem item in LsvVar.SelectedItems)
            {
                LsvVar.Items.Remove(item);
                for (int i = 0; i < AppData.Mappings.Count; i++)
                {
                    Mapping map = AppData.Mappings[i];
                    if (map.Name == item.Text)
                    {
                        AppData.Mappings.Remove(map);
                        break;
                    }
                }
            }
        }

        private void MnuFile_Save_Click(object sender, EventArgs e)
        {
            if (dlgsavedct.ShowDialog() != DialogResult.OK) return;
            SaveDecisionTree(dlgsavedct.FileName);
        }

        private void MnuFile_Open_Click(object sender, EventArgs e)
        {
            if (dlgopendct.ShowDialog() != DialogResult.OK) return;
            AppData.InitData();
            LsvVar.Items.Clear();
            treeview.Nodes.Clear();
            XDocument xdoc = XDocument.Load(dlgopendct.FileName);
            XElement xe = xdoc.Element("root").Element("nodes");
            foreach (XElement tmpxe in xe.Elements("node"))
            {
                NodeBase node = NodeBase.Parse(tmpxe);
                switch (node.Type)
                {
                    case NodeType.Class:
                        node.InheritId = NodeBase.GetNewId(AppData.NodeIdsClass);
                        break;
                    case NodeType.Judge:
                        node.InheritId = NodeBase.GetNewId(AppData.NodeIdsJudge);
                        break;
                }
                AppData.Nodes.Add(node);
            }
            foreach (XElement tmpxe in xe.Elements("node"))
            {
                int tmpid = int.Parse(tmpxe.Attribute("id").Value);
                NodeBase node =  NodeBase.GetNodeById(tmpid, AppData.Nodes);
                tmpid = int.Parse(tmpxe.Attribute("nl").Value);
                node.NodeLeft = NodeBase.GetNodeById(tmpid, AppData.Nodes);
                if(node.NodeLeft != null) node.NodeLeft.IsYes = true;

                tmpid = int.Parse(tmpxe.Attribute("nr").Value);
                node.NodeRight = NodeBase.GetNodeById(tmpid, AppData.Nodes);
                if (node.NodeRight != null) node.NodeRight.IsYes = false;
            }
            xe = xdoc.Element("root").Element("vars");
            foreach (XElement tmpxe in xe.Elements("var"))
            {
                Mapping map = new Mapping();
                map.Name = tmpxe.Value;
                AppData.Mappings.Add(map);
            }
            FormLayout_LoadData();
        }

        private void MnuClass_Execute_Click(object sender, EventArgs e)
        {
            string datestr = DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = Application.StartupPath + "\\" + datestr;
            try
            {
                SaveDecisionTree(path + "-dt.xml");
                SaveDataDescriptor(path + "-ds.xml");
                ExecutorFunction exec = new ExecutorFunction(AppData.TcmModHandle);
                if (!exec.Mount(AppData.TcmFuncDesc))
                {
                    MessageBox.Show(AppData.LangPack["FrmMain_Execute_1"]);
                    return;
                }
                exec.Envelope.Write(0, path + "-dt.xml");
                exec.Envelope.Write(1, path + "-ds.xml");
                Thread thread = new Thread(new ParameterizedThreadStart(ThreadProc_ExecDTC));
                thread.Start(exec);
                AppData.FormProgress = new PIE.Controls.DlgProgress();
                AppData.FormProgress.OnCancel = FormProg_Cancel;
                AppData.FormProgress.ShowDialog();
                File.Delete(path + "-dt.xml");
                File.Delete(path + "-ds.xml");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(AppData.LangPack["FrmMain_Execute_2"]);
                Console.WriteLine(ex.Message);
            }
        }

        private bool FormProg_Cancel()
        {
            return false;
        }

        private void ThreadProc_ExecDTC(object param)
        {
            while (AppData.FormProgress == null || !AppData.FormProgress.IsShown) Thread.Sleep(50);
            ExecutorFunction exec = param as ExecutorFunction;
            if (!exec.Start())
            {
                MessageBox.Show(AppData.LangPack["FrmMain_ThreadProc_1"]);
                AppData.FormProgress.Invoke_CloseForm();
                return;
            }
            AppData.FormProgress.Invoke_UpdateText(AppData.LangPack["FrmMain_ThreadProc_2"]);
            while (exec.State != State.Idle)
            {
                AppData.FormProgress.Invoke_UpdateProgress(exec.Progress);
                Thread.Sleep(100);
            }
            AppData.FormProgress.Invoke_CloseForm();
            if (exec.ReturnCode == ReturnCode.Normal)
                MessageBox.Show(AppData.LangPack["FrmMain_ThreadProc_3"]);
            else
                MessageBox.Show(AppData.LangPack["FrmMain_ThreadProc_4"]);
        }
    }
}
