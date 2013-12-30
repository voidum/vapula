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
            this.CtrlTab = new System.Windows.Forms.TabControl();
            this.TabGraph = new System.Windows.Forms.TabPage();
            this.TabData = new System.Windows.Forms.TabPage();
            this.TabProperty = new System.Windows.Forms.TabPage();
            this.ctxmenu_doc.SuspendLayout();
            this.CtrlTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxmenu_doc
            // 
            this.ctxmenu_doc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuDebugCanvas,
            this.MnuDebugGraph});
            this.ctxmenu_doc.Name = "ctxmenubar";
            this.ctxmenu_doc.Size = new System.Drawing.Size(123, 48);
            // 
            // MnuDebugCanvas
            // 
            this.MnuDebugCanvas.Name = "MnuDebugCanvas";
            this.MnuDebugCanvas.Size = new System.Drawing.Size(122, 22);
            this.MnuDebugCanvas.Text = "调试画布";
            this.MnuDebugCanvas.Click += new System.EventHandler(this.MnuDebugCanvas_Click);
            // 
            // MnuDebugGraph
            // 
            this.MnuDebugGraph.Name = "MnuDebugGraph";
            this.MnuDebugGraph.Size = new System.Drawing.Size(122, 22);
            this.MnuDebugGraph.Text = "调试图";
            this.MnuDebugGraph.Click += new System.EventHandler(this.MnuDebugGraph_Click);
            // 
            // CtrlTab
            // 
            this.CtrlTab.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.CtrlTab.Controls.Add(this.TabGraph);
            this.CtrlTab.Controls.Add(this.TabData);
            this.CtrlTab.Controls.Add(this.TabProperty);
            this.CtrlTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CtrlTab.Location = new System.Drawing.Point(0, 0);
            this.CtrlTab.Name = "CtrlTab";
            this.CtrlTab.SelectedIndex = 0;
            this.CtrlTab.Size = new System.Drawing.Size(516, 346);
            this.CtrlTab.TabIndex = 3;
            this.CtrlTab.SelectedIndexChanged += new System.EventHandler(this.CtrlTab_SelectedIndexChanged);
            // 
            // TabGraph
            // 
            this.TabGraph.BackColor = System.Drawing.Color.Transparent;
            this.TabGraph.Location = new System.Drawing.Point(4, 4);
            this.TabGraph.Name = "TabGraph";
            this.TabGraph.Padding = new System.Windows.Forms.Padding(3);
            this.TabGraph.Size = new System.Drawing.Size(508, 320);
            this.TabGraph.TabIndex = 0;
            this.TabGraph.Text = "模型图";
            // 
            // TabData
            // 
            this.TabData.Location = new System.Drawing.Point(4, 4);
            this.TabData.Name = "TabData";
            this.TabData.Padding = new System.Windows.Forms.Padding(3);
            this.TabData.Size = new System.Drawing.Size(508, 320);
            this.TabData.TabIndex = 1;
            this.TabData.Text = "数据";
            this.TabData.UseVisualStyleBackColor = true;
            // 
            // TabProperty
            // 
            this.TabProperty.Location = new System.Drawing.Point(4, 4);
            this.TabProperty.Name = "TabProperty";
            this.TabProperty.Padding = new System.Windows.Forms.Padding(3);
            this.TabProperty.Size = new System.Drawing.Size(508, 320);
            this.TabProperty.TabIndex = 2;
            this.TabProperty.Text = "属性";
            this.TabProperty.UseVisualStyleBackColor = true;
            // 
            // FrmDocument
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(516, 346);
            this.Controls.Add(this.CtrlTab);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmDocument";
            this.ctxmenu_doc.ResumeLayout(false);
            this.CtrlTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ctxmenu_doc;
        private System.Windows.Forms.ToolStripMenuItem MnuDebugCanvas;
        private System.Windows.Forms.ToolStripMenuItem MnuDebugGraph;
        private System.Windows.Forms.TabPage TabGraph;
        private System.Windows.Forms.TabPage TabData;
        private System.Windows.Forms.TabControl CtrlTab;
        private System.Windows.Forms.TabPage TabProperty;

    }
}