namespace Vapula.Designer.UI
{
    partial class UctGraph
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
            this.components = new System.ComponentModel.Container();
            this.ctxmenu_canvas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MnuDeleteSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxmenu_canvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxmenu_canvas
            // 
            this.ctxmenu_canvas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuDeleteSelected,
            this.MnuDeleteAll});
            this.ctxmenu_canvas.Name = "ctxcanvasmenu";
            this.ctxmenu_canvas.Size = new System.Drawing.Size(147, 48);
            // 
            // MnuDeleteSelected
            // 
            this.MnuDeleteSelected.Name = "MnuDeleteSelected";
            this.MnuDeleteSelected.Size = new System.Drawing.Size(146, 22);
            this.MnuDeleteSelected.Text = "删除选定图元";
            this.MnuDeleteSelected.Click += new System.EventHandler(this.MnuDeleteSelected_Click);
            // 
            // MnuDeleteAll
            // 
            this.MnuDeleteAll.Name = "MnuDeleteAll";
            this.MnuDeleteAll.Size = new System.Drawing.Size(146, 22);
            this.MnuDeleteAll.Text = "删除所有图元";
            this.MnuDeleteAll.Click += new System.EventHandler(this.MnuDeleteAll_Click);
            // 
            // UctGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UctGraph";
            this.Size = new System.Drawing.Size(100, 100);
            this.ctxmenu_canvas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ctxmenu_canvas;
        private System.Windows.Forms.ToolStripMenuItem MnuDeleteSelected;
        private System.Windows.Forms.ToolStripMenuItem MnuDeleteAll;
    }
}
