namespace sample_xpipe
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtSend = new System.Windows.Forms.Button();
            this.TbxInput = new System.Windows.Forms.TextBox();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.TbxLog = new System.Windows.Forms.TextBox();
            this.BtStart = new System.Windows.Forms.ToolStripButton();
            this.BtClose = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TbxInput);
            this.panel1.Controls.Add(this.BtSend);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 242);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 80);
            this.panel1.TabIndex = 4;
            // 
            // BtSend
            // 
            this.BtSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtSend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BtSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtSend.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtSend.Location = new System.Drawing.Point(389, 0);
            this.BtSend.Name = "BtSend";
            this.BtSend.Size = new System.Drawing.Size(75, 80);
            this.BtSend.TabIndex = 5;
            this.BtSend.Text = "Send";
            this.BtSend.UseVisualStyleBackColor = true;
            this.BtSend.Click += new System.EventHandler(this.BtSend_Click);
            // 
            // TbxInput
            // 
            this.TbxInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbxInput.Location = new System.Drawing.Point(0, 0);
            this.TbxInput.Multiline = true;
            this.TbxInput.Name = "TbxInput";
            this.TbxInput.Size = new System.Drawing.Size(389, 80);
            this.TbxInput.TabIndex = 6;
            this.TbxInput.WordWrap = false;
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtStart,
            this.BtClose});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(464, 25);
            this.toolbar.TabIndex = 6;
            // 
            // TbxLog
            // 
            this.TbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbxLog.Location = new System.Drawing.Point(0, 25);
            this.TbxLog.Multiline = true;
            this.TbxLog.Name = "TbxLog";
            this.TbxLog.ReadOnly = true;
            this.TbxLog.Size = new System.Drawing.Size(464, 217);
            this.TbxLog.TabIndex = 7;
            // 
            // BtStart
            // 
            this.BtStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtStart.Image = ((System.Drawing.Image)(resources.GetObject("BtStart.Image")));
            this.BtStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtStart.Name = "BtStart";
            this.BtStart.Size = new System.Drawing.Size(68, 22);
            this.BtStart.Text = "Start Pipe";
            this.BtStart.Click += new System.EventHandler(this.BtStart_Click);
            // 
            // BtClose
            // 
            this.BtClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtClose.Image = ((System.Drawing.Image)(resources.GetObject("BtClose.Image")));
            this.BtClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtClose.Name = "BtClose";
            this.BtClose.Size = new System.Drawing.Size(73, 22);
            this.BtClose.Text = "Close Pipe";
            this.BtClose.Click += new System.EventHandler(this.BtClose_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 322);
            this.Controls.Add(this.TbxLog);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sample xPipe";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TbxInput;
        private System.Windows.Forms.Button BtSend;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.TextBox TbxLog;
        private System.Windows.Forms.ToolStripButton BtStart;
        private System.Windows.Forms.ToolStripButton BtClose;
    }
}

