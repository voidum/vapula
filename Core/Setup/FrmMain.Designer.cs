namespace TCM.Setup
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
            this.TbxLog = new System.Windows.Forms.TextBox();
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.MnuAction = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAction_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAction_Execute = new System.Windows.Forms.ToolStripMenuItem();
            this.menubar.SuspendLayout();
            this.SuspendLayout();
            // 
            // TbxLog
            // 
            this.TbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbxLog.Location = new System.Drawing.Point(0, 25);
            this.TbxLog.Multiline = true;
            this.TbxLog.Name = "TbxLog";
            this.TbxLog.ReadOnly = true;
            this.TbxLog.Size = new System.Drawing.Size(344, 207);
            this.TbxLog.TabIndex = 0;
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuAction});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(344, 25);
            this.menubar.TabIndex = 1;
            this.menubar.Text = "menuStrip1";
            // 
            // MnuAction
            // 
            this.MnuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuAction_Load,
            this.MnuAction_Execute});
            this.MnuAction.Name = "MnuAction";
            this.MnuAction.Size = new System.Drawing.Size(72, 21);
            this.MnuAction.Text = "Action(&A)";
            // 
            // MnuAction_Load
            // 
            this.MnuAction_Load.Name = "MnuAction_Load";
            this.MnuAction_Load.Size = new System.Drawing.Size(152, 22);
            this.MnuAction_Load.Text = "Load(&L)";
            this.MnuAction_Load.Click += new System.EventHandler(this.MnuAction_Load_Click);
            // 
            // MnuAction_Execute
            // 
            this.MnuAction_Execute.Name = "MnuAction_Execute";
            this.MnuAction_Execute.Size = new System.Drawing.Size(152, 22);
            this.MnuAction_Execute.Text = "Execute(&E)";
            this.MnuAction_Execute.Click += new System.EventHandler(this.MnuAction_Execute_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 232);
            this.Controls.Add(this.TbxLog);
            this.Controls.Add(this.menubar);
            this.MainMenuStrip = this.menubar;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup";
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TbxLog;
        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.ToolStripMenuItem MnuAction;
        private System.Windows.Forms.ToolStripMenuItem MnuAction_Load;
        private System.Windows.Forms.ToolStripMenuItem MnuAction_Execute;
    }
}

