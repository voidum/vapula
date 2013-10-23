namespace TCM.Model.Designer
{
    partial class FrmLog
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
            this.LsvLog = new System.Windows.Forms.ListView();
            this.ColhLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(344, 25);
            this.toolbar.TabIndex = 1;
            // 
            // LsvLog
            // 
            this.LsvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhLevel,
            this.ColhTime,
            this.ColhContent});
            this.LsvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvLog.FullRowSelect = true;
            this.LsvLog.Location = new System.Drawing.Point(0, 25);
            this.LsvLog.Name = "LsvLog";
            this.LsvLog.Size = new System.Drawing.Size(344, 237);
            this.LsvLog.TabIndex = 2;
            this.LsvLog.UseCompatibleStateImageBehavior = false;
            this.LsvLog.View = System.Windows.Forms.View.Details;
            // 
            // ColhLevel
            // 
            this.ColhLevel.Text = "级别";
            // 
            // ColhTime
            // 
            this.ColhTime.Text = "时间";
            this.ColhTime.Width = 120;
            // 
            // ColhContent
            // 
            this.ColhContent.Text = "内容";
            this.ColhContent.Width = 180;
            // 
            // FrmLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 262);
            this.Controls.Add(this.LsvLog);
            this.Controls.Add(this.toolbar);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmLog";
            this.Text = "日志";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ListView LsvLog;
        private System.Windows.Forms.ColumnHeader ColhLevel;
        private System.Windows.Forms.ColumnHeader ColhContent;
        private System.Windows.Forms.ColumnHeader ColhTime;
    }
}