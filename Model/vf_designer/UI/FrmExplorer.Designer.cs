namespace Vapula.Designer
{
    partial class FrmExplorer
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
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.BtFunction = new System.Windows.Forms.ToolStripDropDownButton();
            this.BtAddFunc = new System.Windows.Forms.ToolStripMenuItem();
            this.移除模型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.split = new System.Windows.Forms.ToolStripSeparator();
            this.BtDesign = new System.Windows.Forms.ToolStripButton();
            this.treeview = new System.Windows.Forms.TreeView();
            this.BtRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtFunction,
            this.BtRefresh,
            this.split,
            this.BtDesign});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(284, 25);
            this.toolbar.TabIndex = 1;
            // 
            // BtFunction
            // 
            this.BtFunction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtAddFunc,
            this.移除模型ToolStripMenuItem});
            this.BtFunction.Image = global::Vapula.Designer.Properties.Resources.function_s;
            this.BtFunction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtFunction.Name = "BtFunction";
            this.BtFunction.Size = new System.Drawing.Size(73, 22);
            this.BtFunction.Text = "模型库";
            // 
            // BtAddFunc
            // 
            this.BtAddFunc.Image = global::Vapula.Designer.Properties.Resources.function_add_s;
            this.BtAddFunc.Name = "BtAddFunc";
            this.BtAddFunc.Size = new System.Drawing.Size(152, 22);
            this.BtAddFunc.Text = "添加模型...";
            // 
            // 移除模型ToolStripMenuItem
            // 
            this.移除模型ToolStripMenuItem.Image = global::Vapula.Designer.Properties.Resources.function_remove_s;
            this.移除模型ToolStripMenuItem.Name = "移除模型ToolStripMenuItem";
            this.移除模型ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.移除模型ToolStripMenuItem.Text = "移除模型";
            // 
            // split
            // 
            this.split.Name = "split";
            this.split.Size = new System.Drawing.Size(6, 25);
            // 
            // BtDesign
            // 
            this.BtDesign.Image = global::Vapula.Designer.Properties.Resources.design_s;
            this.BtDesign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtDesign.Name = "BtDesign";
            this.BtDesign.Size = new System.Drawing.Size(64, 22);
            this.BtDesign.Text = "设计器";
            // 
            // treeview
            // 
            this.treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeview.ItemHeight = 22;
            this.treeview.Location = new System.Drawing.Point(0, 25);
            this.treeview.Name = "treeview";
            this.treeview.Size = new System.Drawing.Size(284, 237);
            this.treeview.TabIndex = 2;
            this.treeview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeview_AfterSelect);
            // 
            // BtRefresh
            // 
            this.BtRefresh.Image = global::Vapula.Designer.Properties.Resources.refresh_s;
            this.BtRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtRefresh.Name = "BtRefresh";
            this.BtRefresh.Size = new System.Drawing.Size(52, 22);
            this.BtRefresh.Text = "刷新";
            this.BtRefresh.Click += new System.EventHandler(this.BtRefresh_Click);
            // 
            // FrmExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.treeview);
            this.Controls.Add(this.toolbar);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmExplorer";
            this.Text = "模型库管理器";
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.TreeView treeview;
        private System.Windows.Forms.ToolStripSeparator split;
        private System.Windows.Forms.ToolStripButton BtDesign;
        private System.Windows.Forms.ToolStripDropDownButton BtFunction;
        private System.Windows.Forms.ToolStripMenuItem BtAddFunc;
        private System.Windows.Forms.ToolStripMenuItem 移除模型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton BtRefresh;
    }
}