namespace TCM.Model.Designer
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
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.LsvTools = new TCM.Model.Designer.GroupListView();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(254, 25);
            this.toolbar.TabIndex = 0;
            // 
            // LsvTools
            // 
            this.LsvTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvTools.Location = new System.Drawing.Point(0, 25);
            this.LsvTools.Name = "LsvTools";
            this.LsvTools.Size = new System.Drawing.Size(254, 387);
            this.LsvTools.TabIndex = 1;
            this.LsvTools.UseCompatibleStateImageBehavior = false;
            // 
            // FrmToolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 412);
            this.Controls.Add(this.LsvTools);
            this.Controls.Add(this.toolbar);
            this.DockAreas = ((xDockPanel.DockAreas)(((((xDockPanel.DockAreas.Float | xDockPanel.DockAreas.Left) 
            | xDockPanel.DockAreas.Right) 
            | xDockPanel.DockAreas.Top) 
            | xDockPanel.DockAreas.Bottom)));
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmToolbox";
            this.Text = "组件箱";
            this.Load += new System.EventHandler(this.FrmToolbox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolbar;
        private GroupListView LsvTools;
    }
}