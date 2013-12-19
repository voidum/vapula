namespace Vapula.Designer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ctxmenu_doc.SuspendLayout();
            this.ctxmenu_canvas.SuspendLayout();
            this.tabControl1.SuspendLayout();
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
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 95);
            this.panel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 95);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(516, 251);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(508, 225);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FrmDocument
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(516, 346);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.DockAreas = ((xDockPanel.DockAreas)((xDockPanel.DockAreas.Float | xDockPanel.DockAreas.Document)));
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmDocument";
            this.SizeChanged += new System.EventHandler(this.FrmDocument_SizeChanged);
            this.ctxmenu_doc.ResumeLayout(false);
            this.ctxmenu_canvas.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ctxmenu_doc;
        private System.Windows.Forms.ToolStripMenuItem MnuDebugCanvas;
        private System.Windows.Forms.ToolStripMenuItem MnuDebugGraph;
        private System.Windows.Forms.ContextMenuStrip ctxmenu_canvas;
        private System.Windows.Forms.ToolStripMenuItem MnuDeleteSelected;
        private System.Windows.Forms.ToolStripMenuItem MnuDeleteAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;

    }
}