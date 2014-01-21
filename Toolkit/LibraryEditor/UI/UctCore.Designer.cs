namespace Vapula.Toolkit
{
    partial class UctCore
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
            this.treeview = new System.Windows.Forms.TreeView();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.BtLoadLibrary = new System.Windows.Forms.ToolStripButton();
            this.BtAddFunction = new System.Windows.Forms.ToolStripButton();
            this.BtAddParam = new System.Windows.Forms.ToolStripButton();
            this.BtRemove = new System.Windows.Forms.ToolStripButton();
            this.BtProperty = new System.Windows.Forms.ToolStripButton();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeview
            // 
            this.treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeview.ItemHeight = 22;
            this.treeview.Location = new System.Drawing.Point(0, 25);
            this.treeview.Name = "treeview";
            this.treeview.Size = new System.Drawing.Size(450, 275);
            this.treeview.TabIndex = 15;
            this.treeview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeview_AfterSelect);
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtLoadLibrary,
            this.BtAddFunction,
            this.BtAddParam,
            this.BtRemove,
            this.BtProperty});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(450, 25);
            this.toolbar.TabIndex = 14;
            // 
            // BtLoadLibrary
            // 
            this.BtLoadLibrary.Image = global::Vapula.Toolkit.Properties.Resources.open_s;
            this.BtLoadLibrary.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtLoadLibrary.Name = "BtLoadLibrary";
            this.BtLoadLibrary.Size = new System.Drawing.Size(64, 22);
            this.BtLoadLibrary.Text = "选择库";
            this.BtLoadLibrary.Click += new System.EventHandler(this.BtLoadLibrary_Click);
            // 
            // BtAddFunction
            // 
            this.BtAddFunction.Image = global::Vapula.Toolkit.Properties.Resources.function_s;
            this.BtAddFunction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtAddFunction.Name = "BtAddFunction";
            this.BtAddFunction.Size = new System.Drawing.Size(76, 22);
            this.BtAddFunction.Text = "添加功能";
            this.BtAddFunction.Click += new System.EventHandler(this.BtAddFunc_Click);
            // 
            // BtAddParam
            // 
            this.BtAddParam.Image = global::Vapula.Toolkit.Properties.Resources.parameter_s;
            this.BtAddParam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtAddParam.Name = "BtAddParam";
            this.BtAddParam.Size = new System.Drawing.Size(76, 22);
            this.BtAddParam.Text = "添加参数";
            this.BtAddParam.Click += new System.EventHandler(this.BtAddParam_Click);
            // 
            // BtRemove
            // 
            this.BtRemove.Image = global::Vapula.Toolkit.Properties.Resources.remove_s;
            this.BtRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtRemove.Name = "BtRemove";
            this.BtRemove.Size = new System.Drawing.Size(52, 22);
            this.BtRemove.Text = "移除";
            this.BtRemove.Click += new System.EventHandler(this.BtRemove_Click);
            // 
            // BtProperty
            // 
            this.BtProperty.Image = global::Vapula.Toolkit.Properties.Resources.detail_s;
            this.BtProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtProperty.Name = "BtProperty";
            this.BtProperty.Size = new System.Drawing.Size(61, 22);
            this.BtProperty.Text = "属性...";
            this.BtProperty.Click += new System.EventHandler(this.BtProperty_Click);
            // 
            // UctCore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeview);
            this.Controls.Add(this.toolbar);
            this.Name = "UctCore";
            this.Size = new System.Drawing.Size(450, 300);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeview;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton BtRemove;
        private System.Windows.Forms.ToolStripButton BtProperty;
        private System.Windows.Forms.ToolStripButton BtAddFunction;
        private System.Windows.Forms.ToolStripButton BtAddParam;
        private System.Windows.Forms.ToolStripButton BtLoadLibrary;
    }
}
