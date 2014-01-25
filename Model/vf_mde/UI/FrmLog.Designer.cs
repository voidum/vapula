namespace Vapula.MDE
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
            this.BtClear = new System.Windows.Forms.ToolStripButton();
            this.BtSave = new System.Windows.Forms.ToolStripButton();
            this.LsvLog = new System.Windows.Forms.ListView();
            this.ColhLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtClear,
            this.BtSave});
            this.toolbar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(24, 262);
            this.toolbar.TabIndex = 1;
            // 
            // BtClear
            // 
            this.BtClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtClear.Image = global::Vapula.MDE.Properties.Resources.broom_s;
            this.BtClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtClear.Name = "BtClear";
            this.BtClear.Size = new System.Drawing.Size(21, 20);
            this.BtClear.Text = "清理";
            this.BtClear.Click += new System.EventHandler(this.BtClear_Click);
            // 
            // BtSave
            // 
            this.BtSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtSave.Image = global::Vapula.MDE.Properties.Resources.disk_s;
            this.BtSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtSave.Name = "BtSave";
            this.BtSave.Size = new System.Drawing.Size(21, 20);
            this.BtSave.Text = "保存";
            // 
            // LsvLog
            // 
            this.LsvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhLevel,
            this.ColhTime,
            this.ColhContent});
            this.LsvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvLog.FullRowSelect = true;
            this.LsvLog.GridLines = true;
            this.LsvLog.Location = new System.Drawing.Point(24, 0);
            this.LsvLog.Name = "LsvLog";
            this.LsvLog.Size = new System.Drawing.Size(320, 262);
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
            this.ColhTime.Width = 150;
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
            this.Resize += new System.EventHandler(this.FrmLog_Resize);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ListView LsvLog;
        private System.Windows.Forms.ColumnHeader ColhLevel;
        private System.Windows.Forms.ColumnHeader ColhContent;
        private System.Windows.Forms.ColumnHeader ColhTime;
        private System.Windows.Forms.ToolStripButton BtSave;
        private System.Windows.Forms.ToolStripButton BtClear;
    }
}