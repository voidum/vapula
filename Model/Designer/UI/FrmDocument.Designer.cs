namespace TCM.Model.Designer
{
    partial class FrmDocument
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
            this.ctxmenu_doc = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MnuDebugCanvas = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuDebugGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxmenu_canvas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MnuDeleteSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxmenu_doc.SuspendLayout();
            this.ctxmenu_canvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxmenu_doc
            // 
            this.ctxmenu_doc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuDebugCanvas,
            this.MnuDebugGraph});
            this.ctxmenu_doc.Name = "ctxmenubar";
            this.ctxmenu_doc.Size = new System.Drawing.Size(125, 48);
            // 
            // MnuDebugCanvas
            // 
            this.MnuDebugCanvas.Name = "MnuDebugCanvas";
            this.MnuDebugCanvas.Size = new System.Drawing.Size(124, 22);
            this.MnuDebugCanvas.Text = "调试画布";
            this.MnuDebugCanvas.Click += new System.EventHandler(this.MnuDebugCanvas_Click);
            // 
            // MnuDebugGraph
            // 
            this.MnuDebugGraph.Name = "MnuDebugGraph";
            this.MnuDebugGraph.Size = new System.Drawing.Size(124, 22);
            this.MnuDebugGraph.Text = "调试图";
            this.MnuDebugGraph.Click += new System.EventHandler(this.MnuDebugGraph_Click);
            // 
            // ctxmenu_canvas
            // 
            this.ctxmenu_canvas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuDeleteSelected,
            this.MnuDeleteAll});
            this.ctxmenu_canvas.Name = "ctxcanvasmenu";
            this.ctxmenu_canvas.Size = new System.Drawing.Size(153, 70);
            // 
            // MnuDeleteSelected
            // 
            this.MnuDeleteSelected.Name = "MnuDeleteSelected";
            this.MnuDeleteSelected.Size = new System.Drawing.Size(152, 22);
            this.MnuDeleteSelected.Text = "删除选定图元";
            this.MnuDeleteSelected.Click += new System.EventHandler(this.MnuDeleteSelected_Click);
            // 
            // MnuDeleteAll
            // 
            this.MnuDeleteAll.Name = "MnuDeleteAll";
            this.MnuDeleteAll.Size = new System.Drawing.Size(152, 22);
            this.MnuDeleteAll.Text = "删除所有图元";
            this.MnuDeleteAll.Click += new System.EventHandler(this.MnuDeleteAll_Click);
            // 
            // FrmDocument
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.DockAreas = ((xDockPanel.DockAreas)((xDockPanel.DockAreas.Float | xDockPanel.DockAreas.Document)));
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmDocument";
            this.SizeChanged += new System.EventHandler(this.FrmDocument_SizeChanged);
            this.ctxmenu_doc.ResumeLayout(false);
            this.ctxmenu_canvas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ctxmenu_doc;
        private System.Windows.Forms.ToolStripMenuItem MnuDebugCanvas;
        private System.Windows.Forms.ToolStripMenuItem MnuDebugGraph;
        private System.Windows.Forms.ContextMenuStrip ctxmenu_canvas;
        private System.Windows.Forms.ToolStripMenuItem MnuDeleteSelected;
        private System.Windows.Forms.ToolStripMenuItem MnuDeleteAll;

    }
}