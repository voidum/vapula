namespace TCM
{
    partial class FrmBrowser
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
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.BtBack = new System.Windows.Forms.ToolStripButton();
            this.BtForward = new System.Windows.Forms.ToolStripButton();
            this.BtRefresh = new System.Windows.Forms.ToolStripButton();
            this.TbxURL = new System.Windows.Forms.ToolStripTextBox();
            this.BtOption = new System.Windows.Forms.ToolStripButton();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtBack,
            this.BtForward,
            this.BtRefresh,
            this.TbxURL,
            this.BtOption});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Padding = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.toolbar.Size = new System.Drawing.Size(284, 26);
            this.toolbar.TabIndex = 0;
            this.toolbar.SizeChanged += new System.EventHandler(this.toolbar_SizeChanged);
            // 
            // BtBack
            // 
            this.BtBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtBack.Image = global::TCM.Properties.Resources.back;
            this.BtBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtBack.Name = "BtBack";
            this.BtBack.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.BtBack.Size = new System.Drawing.Size(23, 22);
            this.BtBack.ToolTipText = "Back";
            // 
            // BtForward
            // 
            this.BtForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtForward.Image = global::TCM.Properties.Resources.forward;
            this.BtForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtForward.Name = "BtForward";
            this.BtForward.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.BtForward.Size = new System.Drawing.Size(23, 22);
            this.BtForward.ToolTipText = "Forward";
            // 
            // BtRefresh
            // 
            this.BtRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtRefresh.Image = global::TCM.Properties.Resources.refresh;
            this.BtRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtRefresh.Name = "BtRefresh";
            this.BtRefresh.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.BtRefresh.Size = new System.Drawing.Size(23, 22);
            this.BtRefresh.ToolTipText = "Refresh";
            // 
            // TbxURL
            // 
            this.TbxURL.AutoSize = false;
            this.TbxURL.Name = "TbxURL";
            this.TbxURL.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.TbxURL.Size = new System.Drawing.Size(50, 25);
            this.TbxURL.ToolTipText = "URL";
            this.TbxURL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbxURL_KeyPress);
            // 
            // BtOption
            // 
            this.BtOption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtOption.Image = global::TCM.Properties.Resources.menu;
            this.BtOption.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtOption.Name = "BtOption";
            this.BtOption.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.BtOption.Size = new System.Drawing.Size(23, 22);
            this.BtOption.ToolTipText = "Option";
            // 
            // FrmBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.toolbar);
            this.Name = "FrmBrowser";
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton BtBack;
        private System.Windows.Forms.ToolStripButton BtRefresh;
        private System.Windows.Forms.ToolStripTextBox TbxURL;
        private System.Windows.Forms.ToolStripButton BtForward;
        private System.Windows.Forms.ToolStripButton BtOption;
    }
}

