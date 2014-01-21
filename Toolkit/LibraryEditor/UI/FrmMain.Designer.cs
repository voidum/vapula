namespace Vapula.Toolkit
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
            this.MnuFile_New = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_Split1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_Split2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuFile_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_Split3 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuTool = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuTool_Option = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp_Guide = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbar = new System.Windows.Forms.StatusStrip();
            this.CtrlTab = new System.Windows.Forms.TabControl();
            this.TabCore = new System.Windows.Forms.TabPage();
            this.TabUI = new System.Windows.Forms.TabPage();
            this.TabLic = new System.Windows.Forms.TabPage();
            this.menubar.SuspendLayout();
            this.CtrlTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuFile,
            this.MnuTool,
            this.MnuHelp});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(584, 25);
            this.menubar.TabIndex = 9;
            this.menubar.Text = "menuStrip1";
            // 
            // MnuFile
            // 
            this.MnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuFile_New,
            this.MnuFile_Open,
            this.MnuFile_Split1,
            this.MnuFile_Save,
            this.MnuFile_SaveAs,
            this.MnuFile_Split2,
            this.MnuFile_Close,
            this.MnuFile_Split3,
            this.MnuFile_Exit});
            this.MnuFile.Name = "MnuFile";
            this.MnuFile.Size = new System.Drawing.Size(58, 21);
            this.MnuFile.Text = "文件(&F)";
            // 
            // MnuFile_New
            // 
            this.MnuFile_New.Name = "MnuFile_New";
            this.MnuFile_New.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_New.Text = "新建组件(&N)";
            this.MnuFile_New.Click += new System.EventHandler(this.MnuFile_New_Click);
            // 
            // MnuFile_Open
            // 
            this.MnuFile_Open.Name = "MnuFile_Open";
            this.MnuFile_Open.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_Open.Text = "打开组件(&O)...";
            this.MnuFile_Open.Click += new System.EventHandler(this.MnuFile_Open_Click);
            // 
            // MnuFile_Split1
            // 
            this.MnuFile_Split1.Name = "MnuFile_Split1";
            this.MnuFile_Split1.Size = new System.Drawing.Size(149, 6);
            // 
            // MnuFile_Save
            // 
            this.MnuFile_Save.Name = "MnuFile_Save";
            this.MnuFile_Save.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_Save.Text = "保存(&S)...";
            this.MnuFile_Save.Click += new System.EventHandler(this.MnuFile_Save_Click);
            // 
            // MnuFile_SaveAs
            // 
            this.MnuFile_SaveAs.Name = "MnuFile_SaveAs";
            this.MnuFile_SaveAs.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_SaveAs.Text = "另存为(&A)...";
            this.MnuFile_SaveAs.Click += new System.EventHandler(this.MnuFile_SaveAs_Click);
            // 
            // MnuFile_Split2
            // 
            this.MnuFile_Split2.Name = "MnuFile_Split2";
            this.MnuFile_Split2.Size = new System.Drawing.Size(149, 6);
            // 
            // MnuFile_Close
            // 
            this.MnuFile_Close.Name = "MnuFile_Close";
            this.MnuFile_Close.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_Close.Text = "关闭组件(&C)";
            this.MnuFile_Close.Click += new System.EventHandler(this.MnuFile_Close_Click);
            // 
            // MnuFile_Split3
            // 
            this.MnuFile_Split3.Name = "MnuFile_Split3";
            this.MnuFile_Split3.Size = new System.Drawing.Size(149, 6);
            // 
            // MnuFile_Exit
            // 
            this.MnuFile_Exit.Name = "MnuFile_Exit";
            this.MnuFile_Exit.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_Exit.Text = "退出(&X)";
            this.MnuFile_Exit.Click += new System.EventHandler(this.MnuFile_Exit_Click);
            // 
            // MnuTool
            // 
            this.MnuTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuTool_Option});
            this.MnuTool.Name = "MnuTool";
            this.MnuTool.Size = new System.Drawing.Size(59, 21);
            this.MnuTool.Text = "工具(&T)";
            // 
            // MnuTool_Option
            // 
            this.MnuTool_Option.Name = "MnuTool_Option";
            this.MnuTool_Option.Size = new System.Drawing.Size(127, 22);
            this.MnuTool_Option.Text = "选项(&O)...";
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
            this.MnuHelp_About.Text = "关于组件编辑器(&A)";
            this.MnuHelp_About.Click += new System.EventHandler(this.MnuHelp_About_Click);
            // 
            // statusbar
            // 
            this.statusbar.Location = new System.Drawing.Point(0, 340);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(584, 22);
            this.statusbar.SizingGrip = false;
            this.statusbar.TabIndex = 11;
            // 
            // CtrlTab
            // 
            this.CtrlTab.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.CtrlTab.Controls.Add(this.TabCore);
            this.CtrlTab.Controls.Add(this.TabUI);
            this.CtrlTab.Controls.Add(this.TabLic);
            this.CtrlTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CtrlTab.Location = new System.Drawing.Point(0, 25);
            this.CtrlTab.Name = "CtrlTab";
            this.CtrlTab.SelectedIndex = 0;
            this.CtrlTab.Size = new System.Drawing.Size(584, 315);
            this.CtrlTab.TabIndex = 12;
            // 
            // TabCore
            // 
            this.TabCore.Location = new System.Drawing.Point(4, 4);
            this.TabCore.Name = "TabCore";
            this.TabCore.Size = new System.Drawing.Size(576, 289);
            this.TabCore.TabIndex = 0;
            this.TabCore.Text = "组件核心";
            this.TabCore.UseVisualStyleBackColor = true;
            // 
            // TabUI
            // 
            this.TabUI.Location = new System.Drawing.Point(4, 4);
            this.TabUI.Name = "TabUI";
            this.TabUI.Size = new System.Drawing.Size(576, 289);
            this.TabUI.TabIndex = 1;
            this.TabUI.Text = "用户界面";
            this.TabUI.UseVisualStyleBackColor = true;
            // 
            // TabLic
            // 
            this.TabLic.Location = new System.Drawing.Point(4, 4);
            this.TabLic.Name = "TabLic";
            this.TabLic.Size = new System.Drawing.Size(576, 289);
            this.TabLic.TabIndex = 2;
            this.TabLic.Text = "许可证";
            this.TabLic.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.CtrlTab);
            this.Controls.Add(this.statusbar);
            this.Controls.Add(this.menubar);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "组件编辑器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.CtrlTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.ToolStripMenuItem MnuFile;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_New;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Open;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Save;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp_Guide;
        private System.Windows.Forms.ToolStripSeparator MnuFile_Split1;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Exit;
        private System.Windows.Forms.ToolStripSeparator MnuFile_Split3;
        private System.Windows.Forms.StatusStrip statusbar;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp_About;
        private System.Windows.Forms.TabControl CtrlTab;
        private System.Windows.Forms.TabPage TabCore;
        private System.Windows.Forms.TabPage TabUI;
        private System.Windows.Forms.TabPage TabLic;
        private System.Windows.Forms.ToolStripMenuItem MnuTool;
        private System.Windows.Forms.ToolStripMenuItem MnuTool_Option;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_SaveAs;
        private System.Windows.Forms.ToolStripSeparator MnuFile_Split2;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Close;

    }
}

