namespace DecisionTreeUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.MnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_New = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuFile_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuFile_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAction = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAction_ConfigData = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit3 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuAction_Execute = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.BtSplit = new System.Windows.Forms.ToolStripButton();
            this.BtMerge = new System.Windows.Forms.ToolStripButton();
            this.BtProperty = new System.Windows.Forms.ToolStripButton();
            this.BtRemoveVar = new System.Windows.Forms.ToolStripButton();
            this.BtAddVar = new System.Windows.Forms.ToolStripButton();
            this.treeview = new System.Windows.Forms.TreeView();
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.LsvVar = new System.Windows.Forms.ListView();
            this.ColhVarName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dlgsavedct = new System.Windows.Forms.SaveFileDialog();
            this.dlgopendct = new System.Windows.Forms.OpenFileDialog();
            this.menubar.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuFile,
            this.MnuAction});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(464, 25);
            this.menubar.TabIndex = 1;
            this.menubar.Text = "menuStrip1";
            // 
            // MnuFile
            // 
            this.MnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuFile_New,
            this.MnuSplit1,
            this.MnuFile_Open,
            this.MnuFile_Save,
            this.MnuSplit2,
            this.MnuFile_Close});
            this.MnuFile.Name = "MnuFile";
            this.MnuFile.Size = new System.Drawing.Size(39, 21);
            this.MnuFile.Text = "&File";
            // 
            // MnuFile_New
            // 
            this.MnuFile_New.Name = "MnuFile_New";
            this.MnuFile_New.Size = new System.Drawing.Size(135, 22);
            this.MnuFile_New.Text = "New(&N)";
            this.MnuFile_New.Click += new System.EventHandler(this.MnuFile_New_Click);
            // 
            // MnuSplit1
            // 
            this.MnuSplit1.Name = "MnuSplit1";
            this.MnuSplit1.Size = new System.Drawing.Size(132, 6);
            // 
            // MnuFile_Open
            // 
            this.MnuFile_Open.Name = "MnuFile_Open";
            this.MnuFile_Open.Size = new System.Drawing.Size(135, 22);
            this.MnuFile_Open.Text = "Open(&O)...";
            this.MnuFile_Open.Click += new System.EventHandler(this.MnuFile_Open_Click);
            // 
            // MnuFile_Save
            // 
            this.MnuFile_Save.Name = "MnuFile_Save";
            this.MnuFile_Save.Size = new System.Drawing.Size(135, 22);
            this.MnuFile_Save.Text = "Save(&S)...";
            this.MnuFile_Save.Click += new System.EventHandler(this.MnuFile_Save_Click);
            // 
            // MnuSplit2
            // 
            this.MnuSplit2.Name = "MnuSplit2";
            this.MnuSplit2.Size = new System.Drawing.Size(132, 6);
            // 
            // MnuFile_Close
            // 
            this.MnuFile_Close.Name = "MnuFile_Close";
            this.MnuFile_Close.Size = new System.Drawing.Size(135, 22);
            this.MnuFile_Close.Text = "Close(&C)";
            this.MnuFile_Close.Click += new System.EventHandler(this.MnuFile_Close_Click);
            // 
            // MnuAction
            // 
            this.MnuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuAction_ConfigData,
            this.MnuSplit3,
            this.MnuAction_Execute});
            this.MnuAction.Name = "MnuAction";
            this.MnuAction.Size = new System.Drawing.Size(56, 21);
            this.MnuAction.Text = "&Action";
            // 
            // MnuAction_ConfigData
            // 
            this.MnuAction_ConfigData.Name = "MnuAction_ConfigData";
            this.MnuAction_ConfigData.Size = new System.Drawing.Size(215, 22);
            this.MnuAction_ConfigData.Text = "Config Data Source(&D)...";
            this.MnuAction_ConfigData.Click += new System.EventHandler(this.MnuClass_ConfigData_Click);
            // 
            // MnuSplit3
            // 
            this.MnuSplit3.Name = "MnuSplit3";
            this.MnuSplit3.Size = new System.Drawing.Size(212, 6);
            // 
            // MnuAction_Execute
            // 
            this.MnuAction_Execute.Name = "MnuAction_Execute";
            this.MnuAction_Execute.Size = new System.Drawing.Size(215, 22);
            this.MnuAction_Execute.Text = "Execute&&Output(&E)";
            this.MnuAction_Execute.Click += new System.EventHandler(this.MnuClass_Execute_Click);
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtSplit,
            this.BtMerge,
            this.BtProperty,
            this.BtRemoveVar,
            this.BtAddVar});
            this.toolbar.Location = new System.Drawing.Point(0, 25);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(464, 27);
            this.toolbar.TabIndex = 0;
            // 
            // BtSplit
            // 
            this.BtSplit.Image = ((System.Drawing.Image)(resources.GetObject("BtSplit.Image")));
            this.BtSplit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtSplit.Name = "BtSplit";
            this.BtSplit.Size = new System.Drawing.Size(57, 24);
            this.BtSplit.Text = "Split";
            this.BtSplit.Click += new System.EventHandler(this.BtSplit_Click);
            // 
            // BtMerge
            // 
            this.BtMerge.Image = ((System.Drawing.Image)(resources.GetObject("BtMerge.Image")));
            this.BtMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtMerge.Name = "BtMerge";
            this.BtMerge.Size = new System.Drawing.Size(71, 24);
            this.BtMerge.Text = "Merge";
            this.BtMerge.Click += new System.EventHandler(this.BtMerge_Click);
            // 
            // BtProperty
            // 
            this.BtProperty.Image = ((System.Drawing.Image)(resources.GetObject("BtProperty.Image")));
            this.BtProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtProperty.Name = "BtProperty";
            this.BtProperty.Size = new System.Drawing.Size(91, 24);
            this.BtProperty.Text = "Property...";
            this.BtProperty.Click += new System.EventHandler(this.BtProperty_Click);
            // 
            // BtRemoveVar
            // 
            this.BtRemoveVar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.BtRemoveVar.Image = ((System.Drawing.Image)(resources.GetObject("BtRemoveVar.Image")));
            this.BtRemoveVar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtRemoveVar.Name = "BtRemoveVar";
            this.BtRemoveVar.Size = new System.Drawing.Size(79, 24);
            this.BtRemoveVar.Text = "Remove";
            this.BtRemoveVar.Click += new System.EventHandler(this.BtRemoveVar_Click);
            // 
            // BtAddVar
            // 
            this.BtAddVar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.BtAddVar.Image = ((System.Drawing.Image)(resources.GetObject("BtAddVar.Image")));
            this.BtAddVar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtAddVar.Name = "BtAddVar";
            this.BtAddVar.Size = new System.Drawing.Size(65, 24);
            this.BtAddVar.Text = "Add...";
            this.BtAddVar.Click += new System.EventHandler(this.BtAddVar_Click);
            // 
            // treeview
            // 
            this.treeview.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeview.Location = new System.Drawing.Point(0, 52);
            this.treeview.Name = "treeview";
            this.treeview.Size = new System.Drawing.Size(290, 270);
            this.treeview.TabIndex = 3;
            this.treeview.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeview_NodeMouseClick);
            this.treeview.DoubleClick += new System.EventHandler(this.treeview_DoubleClick);
            // 
            // images
            // 
            this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
            this.images.TransparentColor = System.Drawing.Color.Transparent;
            this.images.Images.SetKeyName(0, "judge");
            this.images.Images.SetKeyName(1, "class");
            // 
            // LsvVar
            // 
            this.LsvVar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhVarName});
            this.LsvVar.Dock = System.Windows.Forms.DockStyle.Right;
            this.LsvVar.FullRowSelect = true;
            this.LsvVar.Location = new System.Drawing.Point(296, 52);
            this.LsvVar.Name = "LsvVar";
            this.LsvVar.Size = new System.Drawing.Size(168, 270);
            this.LsvVar.TabIndex = 4;
            this.LsvVar.UseCompatibleStateImageBehavior = false;
            this.LsvVar.View = System.Windows.Forms.View.Details;
            // 
            // ColhVarName
            // 
            this.ColhVarName.Text = "Variable";
            this.ColhVarName.Width = 150;
            // 
            // dlgsavedct
            // 
            this.dlgsavedct.DefaultExt = "xml";
            this.dlgsavedct.Filter = "决策树描述|*.xml";
            this.dlgsavedct.Title = "保存决策树描述";
            // 
            // dlgopendct
            // 
            this.dlgopendct.DefaultExt = "xml";
            this.dlgopendct.Filter = "决策树描述|*.xml";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 322);
            this.Controls.Add(this.LsvVar);
            this.Controls.Add(this.treeview);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.menubar);
            this.MainMenuStrip = this.menubar;
            this.MaximumSize = new System.Drawing.Size(480, 600);
            this.MinimumSize = new System.Drawing.Size(480, 360);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Decision Tree Classification";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.ToolStripMenuItem MnuFile;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_New;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Open;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Save;
        private System.Windows.Forms.ToolStripSeparator MnuSplit2;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Close;
        private System.Windows.Forms.ToolStripMenuItem MnuAction;
        private System.Windows.Forms.ToolStripSeparator MnuSplit1;
        private System.Windows.Forms.ToolStripMenuItem MnuAction_ConfigData;
        private System.Windows.Forms.ToolStripSeparator MnuSplit3;
        private System.Windows.Forms.ToolStripMenuItem MnuAction_Execute;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.TreeView treeview;
        private System.Windows.Forms.ToolStripButton BtSplit;
        private System.Windows.Forms.ToolStripButton BtMerge;
        private System.Windows.Forms.ToolStripButton BtProperty;
        private System.Windows.Forms.ImageList images;
        private System.Windows.Forms.ToolStripButton BtRemoveVar;
        private System.Windows.Forms.ToolStripButton BtAddVar;
        private System.Windows.Forms.ListView LsvVar;
        private System.Windows.Forms.ColumnHeader ColhVarName;
        private System.Windows.Forms.SaveFileDialog dlgsavedct;
        private System.Windows.Forms.OpenFileDialog dlgopendct;
    }
}

