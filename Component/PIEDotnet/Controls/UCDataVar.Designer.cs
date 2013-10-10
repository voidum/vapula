namespace PIE.Controls
{
    partial class UCDataVar
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.BtAddVar = new System.Windows.Forms.ToolStripButton();
            this.BtRemoveVar = new System.Windows.Forms.ToolStripButton();
            this.toolsplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtLinkData = new System.Windows.Forms.ToolStripButton();
            this.LsvDataVar = new System.Windows.Forms.ListView();
            this.ColhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhDetail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtAddVar,
            this.BtRemoveVar,
            this.toolsplit1,
            this.BtLinkData});
            this.toolbar.Location = new System.Drawing.Point(0, 223);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(440, 27);
            this.toolbar.TabIndex = 0;
            // 
            // BtAddVar
            // 
            this.BtAddVar.Image = global::PIE.Properties.Resources.link_add;
            this.BtAddVar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtAddVar.Name = "BtAddVar";
            this.BtAddVar.Size = new System.Drawing.Size(89, 24);
            this.BtAddVar.Text = "添加变量...";
            this.BtAddVar.ToolTipText = "添加一个变量以映射数据源";
            this.BtAddVar.Click += new System.EventHandler(this.BtAddVar_Click);
            // 
            // BtRemoveVar
            // 
            this.BtRemoveVar.Image = global::PIE.Properties.Resources.link_delete;
            this.BtRemoveVar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtRemoveVar.Name = "BtRemoveVar";
            this.BtRemoveVar.Size = new System.Drawing.Size(80, 24);
            this.BtRemoveVar.Text = "移除变量";
            this.BtRemoveVar.ToolTipText = "移除选定的变量";
            this.BtRemoveVar.Click += new System.EventHandler(this.BtRemoveVar_Click);
            // 
            // toolsplit1
            // 
            this.toolsplit1.Name = "toolsplit1";
            this.toolsplit1.Size = new System.Drawing.Size(6, 27);
            // 
            // BtLinkData
            // 
            this.BtLinkData.Image = global::PIE.Properties.Resources.link_data;
            this.BtLinkData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtLinkData.Name = "BtLinkData";
            this.BtLinkData.Size = new System.Drawing.Size(101, 24);
            this.BtLinkData.Text = "映射数据源...";
            this.BtLinkData.ToolTipText = "映射数据源到选定的变量";
            this.BtLinkData.Click += new System.EventHandler(this.BtLinkData_Click);
            // 
            // LsvDataVar
            // 
            this.LsvDataVar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LsvDataVar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhName,
            this.ColhSource,
            this.ColhDetail});
            this.LsvDataVar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvDataVar.FullRowSelect = true;
            this.LsvDataVar.GridLines = true;
            this.LsvDataVar.Location = new System.Drawing.Point(0, 0);
            this.LsvDataVar.Margin = new System.Windows.Forms.Padding(0);
            this.LsvDataVar.MultiSelect = false;
            this.LsvDataVar.Name = "LsvDataVar";
            this.LsvDataVar.Size = new System.Drawing.Size(440, 223);
            this.LsvDataVar.TabIndex = 1;
            this.LsvDataVar.UseCompatibleStateImageBehavior = false;
            this.LsvDataVar.View = System.Windows.Forms.View.Details;
            this.LsvDataVar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LsvDataVar_MouseDoubleClick);
            // 
            // ColhName
            // 
            this.ColhName.Text = "变量名";
            this.ColhName.Width = 75;
            // 
            // ColhSource
            // 
            this.ColhSource.Text = "数据源";
            this.ColhSource.Width = 200;
            // 
            // ColhDetail
            // 
            this.ColhDetail.Text = "映射方式";
            this.ColhDetail.Width = 160;
            // 
            // UCDataVar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LsvDataVar);
            this.Controls.Add(this.toolbar);
            this.Name = "UCDataVar";
            this.Size = new System.Drawing.Size(440, 250);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton BtAddVar;
        private System.Windows.Forms.ToolStripButton BtRemoveVar;
        private System.Windows.Forms.ListView LsvDataVar;
        private System.Windows.Forms.ColumnHeader ColhName;
        private System.Windows.Forms.ColumnHeader ColhSource;
        private System.Windows.Forms.ColumnHeader ColhDetail;
        private System.Windows.Forms.ToolStripSeparator toolsplit1;
        private System.Windows.Forms.ToolStripButton BtLinkData;
    }
}
