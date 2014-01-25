using Irisecol;

namespace Vapula.MDE
{
    partial class FrmToolbox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LsvTools = new Irisecol.IricListView();
            this.ctxmenubar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MnuExpandGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuCollapseGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSwitchView = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuLibMng = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ctxmenubar.SuspendLayout();
            this.SuspendLayout();
            // 
            // LsvTools
            // 
            this.LsvTools.ContextMenuStrip = this.ctxmenubar;
            this.LsvTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvTools.Location = new System.Drawing.Point(0, 0);
            this.LsvTools.MultiSelect = false;
            this.LsvTools.Name = "LsvTools";
            this.LsvTools.Size = new System.Drawing.Size(254, 412);
            this.LsvTools.TabIndex = 1;
            this.LsvTools.UseCompatibleStateImageBehavior = false;
            this.LsvTools.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.LsvTools_ItemDrag);
            // 
            // ctxmenubar
            // 
            this.ctxmenubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuExpandGroup,
            this.MnuCollapseGroup,
            this.MnuSwitchView,
            this.MnuSplit1,
            this.MnuLibMng});
            this.ctxmenubar.Name = "menubar";
            this.ctxmenubar.Size = new System.Drawing.Size(154, 120);
            // 
            // MnuExpandGroup
            // 
            this.MnuExpandGroup.Image = global::Vapula.MDE.Properties.Resources.toggle_plus_s;
            this.MnuExpandGroup.Name = "MnuExpandGroup";
            this.MnuExpandGroup.Size = new System.Drawing.Size(153, 22);
            this.MnuExpandGroup.Text = "展开所有组";
            this.MnuExpandGroup.Click += new System.EventHandler(this.MnuExpandGroup_Click);
            // 
            // MnuCollapseGroup
            // 
            this.MnuCollapseGroup.Image = global::Vapula.MDE.Properties.Resources.toggle_minus_s;
            this.MnuCollapseGroup.Name = "MnuCollapseGroup";
            this.MnuCollapseGroup.Size = new System.Drawing.Size(153, 22);
            this.MnuCollapseGroup.Text = "折叠所有组";
            this.MnuCollapseGroup.Click += new System.EventHandler(this.MnuCollapseGroup_Click);
            // 
            // MnuSwitchView
            // 
            this.MnuSwitchView.Name = "MnuSwitchView";
            this.MnuSwitchView.Size = new System.Drawing.Size(153, 22);
            this.MnuSwitchView.Text = "显示大/小图标";
            this.MnuSwitchView.Click += new System.EventHandler(this.MnuSwitchView_Click);
            // 
            // MnuSplit1
            // 
            this.MnuSplit1.Name = "MnuSplit1";
            this.MnuSplit1.Size = new System.Drawing.Size(150, 6);
            // 
            // MnuLibMng
            // 
            this.MnuLibMng.Name = "MnuLibMng";
            this.MnuLibMng.Size = new System.Drawing.Size(153, 22);
            this.MnuLibMng.Text = "组件管理器...";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(23, 23);
            // 
            // FrmToolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 412);
            this.Controls.Add(this.LsvTools);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmToolbox";
            this.Text = "组件箱";
            this.Load += new System.EventHandler(this.FrmToolbox_Load);
            this.ctxmenubar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private IricListView LsvTools;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ContextMenuStrip ctxmenubar;
        private System.Windows.Forms.ToolStripMenuItem MnuCollapseGroup;
        private System.Windows.Forms.ToolStripSeparator MnuSplit1;
        private System.Windows.Forms.ToolStripMenuItem MnuLibMng;
        private System.Windows.Forms.ToolStripMenuItem MnuExpandGroup;
        private System.Windows.Forms.ToolStripMenuItem MnuSwitchView;
    }
}