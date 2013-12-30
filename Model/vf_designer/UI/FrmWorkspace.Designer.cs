namespace Vapula.Designer
{
    partial class FrmWorkspace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWorkspace));
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.BtAdd = new System.Windows.Forms.ToolStripButton();
            this.BtRemove = new System.Windows.Forms.ToolStripButton();
            this.split = new System.Windows.Forms.ToolStripSeparator();
            this.BtDesign = new System.Windows.Forms.ToolStripButton();
            this.TrvModel = new System.Windows.Forms.TreeView();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtAdd,
            this.BtRemove,
            this.split,
            this.BtDesign});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(284, 25);
            this.toolbar.TabIndex = 1;
            // 
            // BtAdd
            // 
            this.BtAdd.Image = ((System.Drawing.Image)(resources.GetObject("BtAdd.Image")));
            this.BtAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtAdd.Name = "BtAdd";
            this.BtAdd.Size = new System.Drawing.Size(85, 22);
            this.BtAdd.Text = "添加模型...";
            // 
            // BtRemove
            // 
            this.BtRemove.Image = ((System.Drawing.Image)(resources.GetObject("BtRemove.Image")));
            this.BtRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtRemove.Name = "BtRemove";
            this.BtRemove.Size = new System.Drawing.Size(76, 22);
            this.BtRemove.Text = "移除模型";
            // 
            // split
            // 
            this.split.Name = "split";
            this.split.Size = new System.Drawing.Size(6, 25);
            // 
            // BtDesign
            // 
            this.BtDesign.Image = ((System.Drawing.Image)(resources.GetObject("BtDesign.Image")));
            this.BtDesign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtDesign.Name = "BtDesign";
            this.BtDesign.Size = new System.Drawing.Size(64, 22);
            this.BtDesign.Text = "设计器";
            // 
            // TrvModel
            // 
            this.TrvModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrvModel.Location = new System.Drawing.Point(0, 25);
            this.TrvModel.Name = "TrvModel";
            this.TrvModel.Size = new System.Drawing.Size(284, 237);
            this.TrvModel.TabIndex = 2;
            // 
            // FrmWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.TrvModel);
            this.Controls.Add(this.toolbar);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmWorkspace";
            this.Text = "工作空间";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmWorkspace_FormClosing);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.TreeView TrvModel;
        private System.Windows.Forms.ToolStripButton BtAdd;
        private System.Windows.Forms.ToolStripButton BtRemove;
        private System.Windows.Forms.ToolStripSeparator split;
        private System.Windows.Forms.ToolStripButton BtDesign;
    }
}