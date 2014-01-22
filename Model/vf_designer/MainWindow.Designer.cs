using WeifenLuo.WinFormsUI.Docking;
namespace Vapula.Designer
{
    partial class MainWindow
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
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin2 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin2 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient4 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient8 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient9 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient5 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient10 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient11 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient12 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient6 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient13 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient14 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.MnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_New = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuFile_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuFile_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit3 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuView_Toolbox = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuView_Workspace = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuView_Stage = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuView_Log = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit4 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuView_Window = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuModel = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuModel_ExecuteModel = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuModel_SimulateStage = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbar = new System.Windows.Forms.StatusStrip();
            this.paneldock = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.MnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp_Guide = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.menubar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuFile,
            this.MnuView,
            this.MnuModel,
            this.MnuHelp});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(584, 25);
            this.menubar.TabIndex = 1;
            // 
            // MnuFile
            // 
            this.MnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuFile_New,
            this.MnuFile_Open,
            this.MnuSplit1,
            this.MnuFile_Close,
            this.MnuSplit2,
            this.MnuFile_Save,
            this.MnuFile_SaveAs,
            this.MnuSplit3,
            this.MnuFile_Exit});
            this.MnuFile.Name = "MnuFile";
            this.MnuFile.Size = new System.Drawing.Size(58, 21);
            this.MnuFile.Text = "文件(&F)";
            // 
            // MnuFile_New
            // 
            this.MnuFile_New.Name = "MnuFile_New";
            this.MnuFile_New.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_New.Text = "新建(&N)";
            this.MnuFile_New.Click += new System.EventHandler(this.MnuFile_New_Click);
            // 
            // MnuFile_Open
            // 
            this.MnuFile_Open.Name = "MnuFile_Open";
            this.MnuFile_Open.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_Open.Text = "打开(&O)";
            // 
            // MnuSplit1
            // 
            this.MnuSplit1.Name = "MnuSplit1";
            this.MnuSplit1.Size = new System.Drawing.Size(149, 6);
            // 
            // MnuFile_Close
            // 
            this.MnuFile_Close.Name = "MnuFile_Close";
            this.MnuFile_Close.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_Close.Text = "关闭(&C)";
            // 
            // MnuSplit2
            // 
            this.MnuSplit2.Name = "MnuSplit2";
            this.MnuSplit2.Size = new System.Drawing.Size(149, 6);
            // 
            // MnuFile_Save
            // 
            this.MnuFile_Save.Name = "MnuFile_Save";
            this.MnuFile_Save.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_Save.Text = "保存(&S)";
            // 
            // MnuFile_SaveAs
            // 
            this.MnuFile_SaveAs.Name = "MnuFile_SaveAs";
            this.MnuFile_SaveAs.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_SaveAs.Text = "另存为(&A)...";
            // 
            // MnuSplit3
            // 
            this.MnuSplit3.Name = "MnuSplit3";
            this.MnuSplit3.Size = new System.Drawing.Size(149, 6);
            // 
            // MnuFile_Exit
            // 
            this.MnuFile_Exit.Name = "MnuFile_Exit";
            this.MnuFile_Exit.Size = new System.Drawing.Size(152, 22);
            this.MnuFile_Exit.Text = "退出(&X)";
            this.MnuFile_Exit.Click += new System.EventHandler(this.MnuFile_Exit_Click);
            // 
            // MnuView
            // 
            this.MnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuView_Toolbox,
            this.MnuView_Workspace,
            this.MnuView_Stage,
            this.MnuView_Log,
            this.MnuSplit4,
            this.MnuView_Window});
            this.MnuView.Name = "MnuView";
            this.MnuView.Size = new System.Drawing.Size(60, 21);
            this.MnuView.Text = "视图(&V)";
            // 
            // MnuView_Toolbox
            // 
            this.MnuView_Toolbox.Name = "MnuView_Toolbox";
            this.MnuView_Toolbox.Size = new System.Drawing.Size(152, 22);
            this.MnuView_Toolbox.Text = "工具箱(&T)";
            this.MnuView_Toolbox.Click += new System.EventHandler(this.MnuView_Toolbox_Click);
            // 
            // MnuView_Workspace
            // 
            this.MnuView_Workspace.Name = "MnuView_Workspace";
            this.MnuView_Workspace.Size = new System.Drawing.Size(152, 22);
            this.MnuView_Workspace.Text = "工作空间(&W)";
            this.MnuView_Workspace.Click += new System.EventHandler(this.MnuView_Workspace_Click);
            // 
            // MnuView_Stage
            // 
            this.MnuView_Stage.Name = "MnuView_Stage";
            this.MnuView_Stage.Size = new System.Drawing.Size(152, 22);
            this.MnuView_Stage.Text = "阶段(&S)";
            this.MnuView_Stage.Click += new System.EventHandler(this.MnuView_Stage_Click);
            // 
            // MnuView_Log
            // 
            this.MnuView_Log.Name = "MnuView_Log";
            this.MnuView_Log.Size = new System.Drawing.Size(152, 22);
            this.MnuView_Log.Text = "日志(&L)";
            this.MnuView_Log.Click += new System.EventHandler(this.MnuView_Log_Click);
            // 
            // MnuSplit4
            // 
            this.MnuSplit4.Name = "MnuSplit4";
            this.MnuSplit4.Size = new System.Drawing.Size(149, 6);
            // 
            // MnuView_Window
            // 
            this.MnuView_Window.Name = "MnuView_Window";
            this.MnuView_Window.Size = new System.Drawing.Size(152, 22);
            this.MnuView_Window.Text = "窗口(&W)...";
            // 
            // MnuModel
            // 
            this.MnuModel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuModel_ExecuteModel,
            this.MnuModel_SimulateStage});
            this.MnuModel.Name = "MnuModel";
            this.MnuModel.Size = new System.Drawing.Size(64, 21);
            this.MnuModel.Text = "模型(&M)";
            // 
            // MnuModel_ExecuteModel
            // 
            this.MnuModel_ExecuteModel.Name = "MnuModel_ExecuteModel";
            this.MnuModel_ExecuteModel.Size = new System.Drawing.Size(152, 22);
            this.MnuModel_ExecuteModel.Text = "开始执行(&E)";
            this.MnuModel_ExecuteModel.Click += new System.EventHandler(this.MnuModel_ExecuteModel_Click);
            // 
            // MnuModel_SimulateStage
            // 
            this.MnuModel_SimulateStage.Name = "MnuModel_SimulateStage";
            this.MnuModel_SimulateStage.Size = new System.Drawing.Size(152, 22);
            this.MnuModel_SimulateStage.Text = "分析阶段(&S)";
            this.MnuModel_SimulateStage.Click += new System.EventHandler(this.MnuModel_SimulateStage_Click);
            // 
            // statusbar
            // 
            this.statusbar.Location = new System.Drawing.Point(0, 340);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(584, 22);
            this.statusbar.SizingGrip = false;
            this.statusbar.TabIndex = 2;
            // 
            // paneldock
            // 
            this.paneldock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneldock.DockBackColor = System.Drawing.SystemColors.Control;
            this.paneldock.DockLeftPortion = 0.2D;
            this.paneldock.DockRightPortion = 0.21D;
            this.paneldock.Location = new System.Drawing.Point(0, 25);
            this.paneldock.Name = "paneldock";
            this.paneldock.Size = new System.Drawing.Size(584, 315);
            dockPanelGradient4.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient4.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin2.DockStripGradient = dockPanelGradient4;
            tabGradient8.EndColor = System.Drawing.SystemColors.Control;
            tabGradient8.StartColor = System.Drawing.SystemColors.Control;
            tabGradient8.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin2.TabGradient = tabGradient8;
            autoHideStripSkin2.TextFont = new System.Drawing.Font("微软雅黑", 9F);
            dockPanelSkin2.AutoHideStripSkin = autoHideStripSkin2;
            tabGradient9.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient9.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient9.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient2.ActiveTabGradient = tabGradient9;
            dockPanelGradient5.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient5.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient2.DockStripGradient = dockPanelGradient5;
            tabGradient10.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient10.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient10.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient2.InactiveTabGradient = tabGradient10;
            dockPaneStripSkin2.DocumentGradient = dockPaneStripGradient2;
            dockPaneStripSkin2.TextFont = new System.Drawing.Font("微软雅黑", 9F);
            tabGradient11.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient11.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient11.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient11.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient2.ActiveCaptionGradient = tabGradient11;
            tabGradient12.EndColor = System.Drawing.SystemColors.Control;
            tabGradient12.StartColor = System.Drawing.SystemColors.Control;
            tabGradient12.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient2.ActiveTabGradient = tabGradient12;
            dockPanelGradient6.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient6.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient2.DockStripGradient = dockPanelGradient6;
            tabGradient13.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient13.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient13.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient13.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient2.InactiveCaptionGradient = tabGradient13;
            tabGradient14.EndColor = System.Drawing.Color.Transparent;
            tabGradient14.StartColor = System.Drawing.Color.Transparent;
            tabGradient14.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient2.InactiveTabGradient = tabGradient14;
            dockPaneStripSkin2.ToolWindowGradient = dockPaneStripToolWindowGradient2;
            dockPanelSkin2.DockPaneStripSkin = dockPaneStripSkin2;
            this.paneldock.Skin = dockPanelSkin2;
            this.paneldock.TabIndex = 4;
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
            // 
            // MnuHelp_About
            // 
            this.MnuHelp_About.Name = "MnuHelp_About";
            this.MnuHelp_About.Size = new System.Drawing.Size(176, 22);
            this.MnuHelp_About.Text = "关于模型设计器(&A)";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.paneldock);
            this.Controls.Add(this.statusbar);
            this.Controls.Add(this.menubar);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menubar;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模型设计器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.StatusStrip statusbar;
        private WeifenLuo.WinFormsUI.Docking.DockPanel paneldock;
        private System.Windows.Forms.ToolStripMenuItem MnuFile;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_New;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Open;
        private System.Windows.Forms.ToolStripSeparator MnuSplit1;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Close;
        private System.Windows.Forms.ToolStripSeparator MnuSplit2;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Save;
        private System.Windows.Forms.ToolStripSeparator MnuSplit3;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_Exit;
        private System.Windows.Forms.ToolStripMenuItem MnuFile_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem MnuModel;
        private System.Windows.Forms.ToolStripMenuItem MnuModel_ExecuteModel;
        private System.Windows.Forms.ToolStripMenuItem MnuView;
        private System.Windows.Forms.ToolStripMenuItem MnuView_Window;
        private System.Windows.Forms.ToolStripMenuItem MnuModel_SimulateStage;
        private System.Windows.Forms.ToolStripMenuItem MnuView_Log;
        private System.Windows.Forms.ToolStripSeparator MnuSplit4;
        private System.Windows.Forms.ToolStripMenuItem MnuView_Toolbox;
        private System.Windows.Forms.ToolStripMenuItem MnuView_Stage;
        private System.Windows.Forms.ToolStripMenuItem MnuView_Workspace;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp_Guide;
        private System.Windows.Forms.ToolStripMenuItem MnuHelp_About;
    }
}

