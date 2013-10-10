namespace ImageScripter
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
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.MnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.启动RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp_Guide = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbar = new System.Windows.Forms.StatusStrip();
            this.CtrlTab = new System.Windows.Forms.TabControl();
            this.TabPage_Data = new System.Windows.Forms.TabPage();
            this.TabPage_Code = new System.Windows.Forms.TabPage();
            this.TbxCode = new AdvRichTextbox.CodeTextbox();
            this.TabPage_Log = new System.Windows.Forms.TabPage();
            this.MnuSplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.menubar.SuspendLayout();
            this.CtrlTab.SuspendLayout();
            this.TabPage_Code.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuFile,
            this.MnuDebug,
            this.MnuHelp});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(784, 25);
            this.menubar.TabIndex = 0;
            this.menubar.Text = "menuStrip1";
            // 
            // MnuFile
            // 
            this.MnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.MnuSplit1,
            this.MnuFile_Exit});
            this.MnuFile.Name = "MnuFile";
            this.MnuFile.Size = new System.Drawing.Size(58, 21);
            this.MnuFile.Text = "文件(&F)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "新建脚本(&N)";
            // 
            // MnuFile_Exit
            // 
            this.MnuFile_Exit.Name = "MnuFile_Exit";
            this.MnuFile_Exit.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_Exit.Text = "退出(&X)";
            // 
            // MnuDebug
            // 
            this.MnuDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动RToolStripMenuItem});
            this.MnuDebug.Name = "MnuDebug";
            this.MnuDebug.Size = new System.Drawing.Size(61, 21);
            this.MnuDebug.Text = "调试(&D)";
            // 
            // 启动RToolStripMenuItem
            // 
            this.启动RToolStripMenuItem.Name = "启动RToolStripMenuItem";
            this.启动RToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.启动RToolStripMenuItem.Text = "启动(&S)";
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
            this.MnuHelp_Guide.Size = new System.Drawing.Size(183, 22);
            this.MnuHelp_Guide.Text = "用户指导(&G)";
            // 
            // MnuHelp_About
            // 
            this.MnuHelp_About.Name = "MnuHelp_About";
            this.MnuHelp_About.Size = new System.Drawing.Size(183, 22);
            this.MnuHelp_About.Text = "关于PIE Scripter(&A)";
            // 
            // statusbar
            // 
            this.statusbar.Location = new System.Drawing.Point(0, 440);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(784, 22);
            this.statusbar.SizingGrip = false;
            this.statusbar.TabIndex = 1;
            // 
            // CtrlTab
            // 
            this.CtrlTab.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.CtrlTab.Controls.Add(this.TabPage_Data);
            this.CtrlTab.Controls.Add(this.TabPage_Code);
            this.CtrlTab.Controls.Add(this.TabPage_Log);
            this.CtrlTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CtrlTab.Location = new System.Drawing.Point(0, 25);
            this.CtrlTab.Margin = new System.Windows.Forms.Padding(0);
            this.CtrlTab.Name = "CtrlTab";
            this.CtrlTab.Padding = new System.Drawing.Point(12, 4);
            this.CtrlTab.SelectedIndex = 0;
            this.CtrlTab.Size = new System.Drawing.Size(784, 415);
            this.CtrlTab.TabIndex = 2;
            // 
            // TabPage_Data
            // 
            this.TabPage_Data.Location = new System.Drawing.Point(4, 4);
            this.TabPage_Data.Margin = new System.Windows.Forms.Padding(0);
            this.TabPage_Data.Name = "TabPage_Data";
            this.TabPage_Data.Size = new System.Drawing.Size(776, 387);
            this.TabPage_Data.TabIndex = 0;
            this.TabPage_Data.Text = "数据";
            this.TabPage_Data.UseVisualStyleBackColor = true;
            // 
            // TabPage_Code
            // 
            this.TabPage_Code.Controls.Add(this.TbxCode);
            this.TabPage_Code.Location = new System.Drawing.Point(4, 4);
            this.TabPage_Code.Margin = new System.Windows.Forms.Padding(0);
            this.TabPage_Code.Name = "TabPage_Code";
            this.TabPage_Code.Size = new System.Drawing.Size(776, 387);
            this.TabPage_Code.TabIndex = 1;
            this.TabPage_Code.Text = "代码";
            this.TabPage_Code.UseVisualStyleBackColor = true;
            // 
            // TbxCode
            // 
            this.TbxCode.CodeFont = new System.Drawing.Font("Consolas", 11F);
            this.TbxCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbxCode.Location = new System.Drawing.Point(0, 0);
            this.TbxCode.Margin = new System.Windows.Forms.Padding(0);
            this.TbxCode.Name = "TbxCode";
            this.TbxCode.Size = new System.Drawing.Size(776, 387);
            this.TbxCode.TabIndex = 1;
            // 
            // TabPage_Log
            // 
            this.TabPage_Log.Location = new System.Drawing.Point(4, 4);
            this.TabPage_Log.Margin = new System.Windows.Forms.Padding(0);
            this.TabPage_Log.Name = "TabPage_Log";
            this.TabPage_Log.Size = new System.Drawing.Size(776, 387);
            this.TabPage_Log.TabIndex = 2;
            this.TabPage_Log.Text = "日志";
            this.TabPage_Log.UseVisualStyleBackColor = true;
            // 
            // MnuSplit1
            // 
            this.MnuSplit1.Name = "MnuSplit1";
            this.MnuSplit1.Size = new System.Drawing.Size(149, 6);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.CtrlTab);
            this.Controls.Add(this.statusbar);
            this.Controls.Add(this.menubar);
            this.MainMenuStrip = this.menubar;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PIE Scripter";
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.CtrlTab.ResumeLayout(false);
            this.TabPage_Code.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.StatusStrip statusbar;
        private System.Windows.Forms.ToolStripMenuItem MnuFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Exit;
        private System.Windows.Forms.ToolStripMenuItem MnuDebug;
        private System.Windows.Forms.ToolStripMenuItem 启动RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp_Guide;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp_About;
        private System.Windows.Forms.TabControl CtrlTab;
        private System.Windows.Forms.TabPage TabPage_Data;
        private System.Windows.Forms.TabPage TabPage_Code;
        private AdvRichTextbox.CodeTextbox TbxCode;
        private System.Windows.Forms.TabPage TabPage_Log;
        private System.Windows.Forms.ToolStripSeparator MnuSplit1;
    }
}

