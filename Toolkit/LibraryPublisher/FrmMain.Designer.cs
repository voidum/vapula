namespace TCM.Toolkit
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
            this.dlgfile = new System.Windows.Forms.OpenFileDialog();
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.MnuLib = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuLib_New = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuLib_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuLib_Publish = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuLib_Config = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit3 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuLib_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp_Guide = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbar = new System.Windows.Forms.StatusStrip();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.BtAdd = new System.Windows.Forms.ToolStripDropDownButton();
            this.BtAddFunc = new System.Windows.Forms.ToolStripMenuItem();
            this.BtAddParam = new System.Windows.Forms.ToolStripMenuItem();
            this.BtRemove = new System.Windows.Forms.ToolStripButton();
            this.BtProperty = new System.Windows.Forms.ToolStripButton();
            this.treeview = new System.Windows.Forms.TreeView();
            this.menubar.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuLib,
            this.MnuHelp});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(304, 25);
            this.menubar.TabIndex = 9;
            this.menubar.Text = "menuStrip1";
            // 
            // MnuLib
            // 
            this.MnuLib.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuLib_New,
            this.MnuLib_Open,
            this.MnuSplit1,
            this.MnuLib_Publish,
            this.MnuSplit2,
            this.MnuLib_Config,
            this.MnuSplit3,
            this.MnuLib_Exit});
            this.MnuLib.Name = "MnuLib";
            this.MnuLib.Size = new System.Drawing.Size(58, 21);
            this.MnuLib.Text = "组件(&L)";
            // 
            // MnuLib_New
            // 
            this.MnuLib_New.Name = "MnuLib_New";
            this.MnuLib_New.Size = new System.Drawing.Size(151, 22);
            this.MnuLib_New.Text = "新建发布(&N)";
            this.MnuLib_New.Click += new System.EventHandler(this.MnuLib_New_Click);
            // 
            // MnuLib_Open
            // 
            this.MnuLib_Open.Name = "MnuLib_Open";
            this.MnuLib_Open.Size = new System.Drawing.Size(151, 22);
            this.MnuLib_Open.Text = "打开配置(&O)...";
            this.MnuLib_Open.Click += new System.EventHandler(this.MnuLib_Open_Click);
            // 
            // MnuSplit1
            // 
            this.MnuSplit1.Name = "MnuSplit1";
            this.MnuSplit1.Size = new System.Drawing.Size(148, 6);
            // 
            // MnuLib_Publish
            // 
            this.MnuLib_Publish.Name = "MnuLib_Publish";
            this.MnuLib_Publish.Size = new System.Drawing.Size(151, 22);
            this.MnuLib_Publish.Text = "发布(&P)...";
            this.MnuLib_Publish.Click += new System.EventHandler(this.MnuLib_Publish_Click);
            // 
            // MnuSplit2
            // 
            this.MnuSplit2.Name = "MnuSplit2";
            this.MnuSplit2.Size = new System.Drawing.Size(148, 6);
            // 
            // MnuLib_Config
            // 
            this.MnuLib_Config.Name = "MnuLib_Config";
            this.MnuLib_Config.Size = new System.Drawing.Size(151, 22);
            this.MnuLib_Config.Text = "配置(&C)...";
            // 
            // MnuSplit3
            // 
            this.MnuSplit3.Name = "MnuSplit3";
            this.MnuSplit3.Size = new System.Drawing.Size(148, 6);
            // 
            // MnuLib_Exit
            // 
            this.MnuLib_Exit.Name = "MnuLib_Exit";
            this.MnuLib_Exit.Size = new System.Drawing.Size(151, 22);
            this.MnuLib_Exit.Text = "退出(&X)";
            this.MnuLib_Exit.Click += new System.EventHandler(this.MnuLib_Exit_Click);
            // 
            // MnuHelp
            // 
            this.MnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuHelp_Guide,
            this.MnuHelp_About});
            this.MnuHelp.Name = "MnuHelp";
            this.MnuHelp.Size = new System.Drawing.Size(61, 21);
            this.MnuHelp.Text = "帮助(&H)";
            // 
            // MnuHelp_Guide
            // 
            this.MnuHelp_Guide.Name = "MnuHelp_Guide";
            this.MnuHelp_Guide.Size = new System.Drawing.Size(176, 22);
            this.MnuHelp_Guide.Text = "用户指导(&G)";
            this.MnuHelp_Guide.Click += new System.EventHandler(this.MnuHelp_Guide_Click);
            // 
            // MnuHelp_About
            // 
            this.MnuHelp_About.Name = "MnuHelp_About";
            this.MnuHelp_About.Size = new System.Drawing.Size(176, 22);
            this.MnuHelp_About.Text = "关于组件发布器(&A)";
            this.MnuHelp_About.Click += new System.EventHandler(this.MnuHelp_About_Click);
            // 
            // statusbar
            // 
            this.statusbar.Location = new System.Drawing.Point(0, 390);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(304, 22);
            this.statusbar.SizingGrip = false;
            this.statusbar.TabIndex = 11;
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtAdd,
            this.BtRemove,
            this.BtProperty});
            this.toolbar.Location = new System.Drawing.Point(0, 25);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(304, 25);
            this.toolbar.TabIndex = 12;
            // 
            // BtAdd
            // 
            this.BtAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtAddFunc,
            this.BtAddParam});
            this.BtAdd.Image = global::TCM.Toolkit.Properties.Resources.add_s;
            this.BtAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtAdd.Name = "BtAdd";
            this.BtAdd.Size = new System.Drawing.Size(61, 22);
            this.BtAdd.Text = "添加";
            // 
            // BtAddFunc
            // 
            this.BtAddFunc.Image = global::TCM.Toolkit.Properties.Resources.function_s;
            this.BtAddFunc.Name = "BtAddFunc";
            this.BtAddFunc.Size = new System.Drawing.Size(100, 22);
            this.BtAddFunc.Text = "功能";
            this.BtAddFunc.Click += new System.EventHandler(this.BtAddFunc_Click);
            // 
            // BtAddParam
            // 
            this.BtAddParam.Image = global::TCM.Toolkit.Properties.Resources.attach_s;
            this.BtAddParam.Name = "BtAddParam";
            this.BtAddParam.Size = new System.Drawing.Size(100, 22);
            this.BtAddParam.Text = "参数";
            this.BtAddParam.Click += new System.EventHandler(this.BtAddParam_Click);
            // 
            // BtRemove
            // 
            this.BtRemove.Image = global::TCM.Toolkit.Properties.Resources.remove_s;
            this.BtRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtRemove.Name = "BtRemove";
            this.BtRemove.Size = new System.Drawing.Size(52, 22);
            this.BtRemove.Text = "移除";
            this.BtRemove.Click += new System.EventHandler(this.BtRemove_Click);
            // 
            // BtProperty
            // 
            this.BtProperty.Image = global::TCM.Toolkit.Properties.Resources.detail_s;
            this.BtProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtProperty.Name = "BtProperty";
            this.BtProperty.Size = new System.Drawing.Size(61, 22);
            this.BtProperty.Text = "属性...";
            this.BtProperty.Click += new System.EventHandler(this.BtProperty_Click);
            // 
            // treeview
            // 
            this.treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeview.ItemHeight = 24;
            this.treeview.Location = new System.Drawing.Point(0, 50);
            this.treeview.Name = "treeview";
            this.treeview.Size = new System.Drawing.Size(304, 340);
            this.treeview.TabIndex = 13;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 412);
            this.Controls.Add(this.treeview);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.statusbar);
            this.Controls.Add(this.menubar);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "组件发布器";
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
        private System.Windows.Forms.ToolStripMenuItem MnuLib;
        private System.Windows.Forms.ToolStripMenuItem MnuLib_New;
        private System.Windows.Forms.ToolStripMenuItem MnuLib_Open;
        private System.Windows.Forms.ToolStripMenuItem MnuLib_Publish;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp_Guide;
        private System.Windows.Forms.ToolStripSeparator MnuSplit1;
        private System.Windows.Forms.ToolStripMenuItem MnuLib_Exit;
        private System.Windows.Forms.ToolStripSeparator MnuSplit2;
        private System.Windows.Forms.ToolStripMenuItem MnuLib_Config;
        private System.Windows.Forms.StatusStrip statusbar;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton BtRemove;
        private System.Windows.Forms.TreeView treeview;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp_About;
        private System.Windows.Forms.ToolStripButton BtProperty;
        private System.Windows.Forms.ToolStripSeparator MnuSplit3;
        private System.Windows.Forms.ToolStripDropDownButton BtAdd;
        private System.Windows.Forms.ToolStripMenuItem BtAddFunc;
        private System.Windows.Forms.ToolStripMenuItem BtAddParam;

    }
}

