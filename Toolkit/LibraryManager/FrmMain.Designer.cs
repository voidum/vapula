namespace Vapula.ComManager
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
            this.LsvCom = new System.Windows.Forms.ListView();
            this.ColhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhFuncTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhPublisher = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LblDescription = new System.Windows.Forms.Label();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.BtAddCom = new System.Windows.Forms.ToolStripButton();
            this.BtRemoveCom = new System.Windows.Forms.ToolStripButton();
            this.BtClearCom = new System.Windows.Forms.ToolStripButton();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // LsvCom
            // 
            this.LsvCom.CheckBoxes = true;
            this.LsvCom.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhName,
            this.ColhFuncTotal,
            this.ColhPublisher,
            this.ColhVersion});
            this.LsvCom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvCom.FullRowSelect = true;
            this.LsvCom.GridLines = true;
            this.LsvCom.Location = new System.Drawing.Point(0, 0);
            this.LsvCom.Margin = new System.Windows.Forms.Padding(0);
            this.LsvCom.MultiSelect = false;
            this.LsvCom.Name = "LsvCom";
            this.LsvCom.Size = new System.Drawing.Size(534, 304);
            this.LsvCom.TabIndex = 6;
            this.LsvCom.UseCompatibleStateImageBehavior = false;
            this.LsvCom.View = System.Windows.Forms.View.Details;
            // 
            // ColhName
            // 
            this.ColhName.Text = "组件名称";
            this.ColhName.Width = 200;
            // 
            // ColhFuncTotal
            // 
            this.ColhFuncTotal.Text = "功能数量";
            this.ColhFuncTotal.Width = 80;
            // 
            // ColhPublisher
            // 
            this.ColhPublisher.Text = "发布方";
            this.ColhPublisher.Width = 150;
            // 
            // ColhVersion
            // 
            this.ColhVersion.Text = "版本";
            this.ColhVersion.Width = 100;
            // 
            // LblDescription
            // 
            this.LblDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LblDescription.Location = new System.Drawing.Point(0, 304);
            this.LblDescription.Margin = new System.Windows.Forms.Padding(0);
            this.LblDescription.Name = "LblDescription";
            this.LblDescription.Size = new System.Drawing.Size(534, 75);
            this.LblDescription.TabIndex = 7;
            // 
            // toolbar
            // 
            this.toolbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtAddCom,
            this.BtRemoveCom,
            this.BtClearCom});
            this.toolbar.Location = new System.Drawing.Point(0, 379);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(534, 25);
            this.toolbar.TabIndex = 8;
            // 
            // BtAddCom
            // 
            this.BtAddCom.Image = ((System.Drawing.Image)(resources.GetObject("BtAddCom.Image")));
            this.BtAddCom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtAddCom.Name = "BtAddCom";
            this.BtAddCom.Size = new System.Drawing.Size(76, 22);
            this.BtAddCom.Text = "添加组件";
            // 
            // BtRemoveCom
            // 
            this.BtRemoveCom.Image = ((System.Drawing.Image)(resources.GetObject("BtRemoveCom.Image")));
            this.BtRemoveCom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtRemoveCom.Name = "BtRemoveCom";
            this.BtRemoveCom.Size = new System.Drawing.Size(76, 22);
            this.BtRemoveCom.Text = "移除组件";
            // 
            // BtClearCom
            // 
            this.BtClearCom.Image = ((System.Drawing.Image)(resources.GetObject("BtClearCom.Image")));
            this.BtClearCom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtClearCom.Name = "BtClearCom";
            this.BtClearCom.Size = new System.Drawing.Size(76, 22);
            this.BtClearCom.Text = "清理组件";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 404);
            this.Controls.Add(this.LsvCom);
            this.Controls.Add(this.LblDescription);
            this.Controls.Add(this.toolbar);
            this.MinimumSize = new System.Drawing.Size(550, 440);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "组件管理器";
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LsvCom;
        private System.Windows.Forms.ColumnHeader ColhName;
        private System.Windows.Forms.ColumnHeader ColhFuncTotal;
        private System.Windows.Forms.ColumnHeader ColhPublisher;
        private System.Windows.Forms.ColumnHeader ColhVersion;
        private System.Windows.Forms.Label LblDescription;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton BtRemoveCom;
        private System.Windows.Forms.ToolStripButton BtAddCom;
        private System.Windows.Forms.ToolStripButton BtClearCom;
    }
}

