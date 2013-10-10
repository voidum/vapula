namespace TCM.ComPubMini
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.dlgfile = new System.Windows.Forms.OpenFileDialog();
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.MnuCom = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuCom_New = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuCom_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuCom_Publish = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuCom_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp_Guide = new System.Windows.Forms.ToolStripMenuItem();
            this.treeview = new System.Windows.Forms.TreeView();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.BtAddFunc = new System.Windows.Forms.ToolStripButton();
            this.BtAddParam = new System.Windows.Forms.ToolStripButton();
            this.BtProperty = new System.Windows.Forms.ToolStripButton();
            this.BtRemove = new System.Windows.Forms.ToolStripButton();
            this.menubar.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuCom,
            this.MnuHelp});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(334, 25);
            this.menubar.TabIndex = 9;
            this.menubar.Text = "menuStrip1";
            // 
            // MnuCom
            // 
            this.MnuCom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuCom_New,
            this.MnuCom_Load,
            this.MnuSplit1,
            this.MnuCom_Publish,
            this.MnuSplit2,
            this.MnuCom_Exit});
            this.MnuCom.Name = "MnuCom";
            this.MnuCom.Size = new System.Drawing.Size(60, 21);
            this.MnuCom.Text = "组件(&C)";
            // 
            // MnuCom_New
            // 
            this.MnuCom_New.Name = "MnuCom_New";
            this.MnuCom_New.Size = new System.Drawing.Size(151, 22);
            this.MnuCom_New.Text = "新建发布(&N)";
            this.MnuCom_New.Click += new System.EventHandler(this.MnuCom_New_Click);
            // 
            // MnuCom_Load
            // 
            this.MnuCom_Load.Name = "MnuCom_Load";
            this.MnuCom_Load.Size = new System.Drawing.Size(151, 22);
            this.MnuCom_Load.Text = "打开配置(&O)...";
            this.MnuCom_Load.Click += new System.EventHandler(this.MnuCom_Load_Click);
            // 
            // MnuSplit1
            // 
            this.MnuSplit1.Name = "MnuSplit1";
            this.MnuSplit1.Size = new System.Drawing.Size(148, 6);
            // 
            // MnuCom_Publish
            // 
            this.MnuCom_Publish.Name = "MnuCom_Publish";
            this.MnuCom_Publish.Size = new System.Drawing.Size(151, 22);
            this.MnuCom_Publish.Text = "发布(&P)...";
            this.MnuCom_Publish.Click += new System.EventHandler(this.MnuCom_Publish_Click);
            // 
            // MnuSplit2
            // 
            this.MnuSplit2.Name = "MnuSplit2";
            this.MnuSplit2.Size = new System.Drawing.Size(148, 6);
            // 
            // MnuCom_Exit
            // 
            this.MnuCom_Exit.Name = "MnuCom_Exit";
            this.MnuCom_Exit.Size = new System.Drawing.Size(151, 22);
            this.MnuCom_Exit.Text = "退出(&X)";
            this.MnuCom_Exit.Click += new System.EventHandler(this.MnuCom_Exit_Click);
            // 
            // MnuHelp
            // 
            this.MnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuHelp_Guide});
            this.MnuHelp.Name = "MnuHelp";
            this.MnuHelp.Size = new System.Drawing.Size(61, 21);
            this.MnuHelp.Text = "帮助(&H)";
            // 
            // MnuHelp_Guide
            // 
            this.MnuHelp_Guide.Name = "MnuHelp_Guide";
            this.MnuHelp_Guide.Size = new System.Drawing.Size(141, 22);
            this.MnuHelp_Guide.Text = "用户指导(&G)";
            this.MnuHelp_Guide.Click += new System.EventHandler(this.MnuHelp_Guide_Click);
            // 
            // treeview
            // 
            this.treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeview.FullRowSelect = true;
            this.treeview.Location = new System.Drawing.Point(0, 25);
            this.treeview.Margin = new System.Windows.Forms.Padding(0);
            this.treeview.Name = "treeview";
            this.treeview.Size = new System.Drawing.Size(334, 330);
            this.treeview.TabIndex = 11;
            // 
            // toolbar
            // 
            this.toolbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtAddFunc,
            this.BtAddParam,
            this.BtProperty,
            this.BtRemove});
            this.toolbar.Location = new System.Drawing.Point(0, 355);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(334, 27);
            this.toolbar.TabIndex = 10;
            // 
            // BtAddFunc
            // 
            this.BtAddFunc.Image = ((System.Drawing.Image)(resources.GetObject("BtAddFunc.Image")));
            this.BtAddFunc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtAddFunc.Name = "BtAddFunc";
            this.BtAddFunc.Size = new System.Drawing.Size(80, 24);
            this.BtAddFunc.Text = "新增功能";
            this.BtAddFunc.Click += new System.EventHandler(this.BtAddFunc_Click);
            // 
            // BtAddParam
            // 
            this.BtAddParam.Image = ((System.Drawing.Image)(resources.GetObject("BtAddParam.Image")));
            this.BtAddParam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtAddParam.Name = "BtAddParam";
            this.BtAddParam.Size = new System.Drawing.Size(80, 24);
            this.BtAddParam.Text = "新增参数";
            this.BtAddParam.Click += new System.EventHandler(this.BtAddParam_Click);
            // 
            // BtProperty
            // 
            this.BtProperty.Image = ((System.Drawing.Image)(resources.GetObject("BtProperty.Image")));
            this.BtProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtProperty.Name = "BtProperty";
            this.BtProperty.Size = new System.Drawing.Size(80, 24);
            this.BtProperty.Text = "修改属性";
            this.BtProperty.Click += new System.EventHandler(this.BtProperty_Click);
            // 
            // BtRemove
            // 
            this.BtRemove.Image = ((System.Drawing.Image)(resources.GetObject("BtRemove.Image")));
            this.BtRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtRemove.Name = "BtRemove";
            this.BtRemove.Size = new System.Drawing.Size(68, 24);
            this.BtRemove.Text = "移除项";
            this.BtRemove.Click += new System.EventHandler(this.BtRemove_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 382);
            this.Controls.Add(this.treeview);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.menubar);
            this.MinimumSize = new System.Drawing.Size(350, 420);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "组件发布器(mini)";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgfile;
        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.ToolStripMenuItem MnuCom;
        private System.Windows.Forms.ToolStripMenuItem MnuCom_New;
        private System.Windows.Forms.ToolStripMenuItem MnuCom_Load;
        private System.Windows.Forms.ToolStripMenuItem MnuCom_Publish;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp_Guide;
        private System.Windows.Forms.ToolStripSeparator MnuSplit1;
        private System.Windows.Forms.ToolStripMenuItem MnuCom_Exit;
        private System.Windows.Forms.ToolStripSeparator MnuSplit2;
        private System.Windows.Forms.TreeView treeview;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton BtAddFunc;
        private System.Windows.Forms.ToolStripButton BtAddParam;
        private System.Windows.Forms.ToolStripButton BtRemove;
        private System.Windows.Forms.ToolStripButton BtProperty;

    }
}

