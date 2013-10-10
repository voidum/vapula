namespace Model
{
    partial class FrmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.canvasGraph1 = new TCM.Model.Designer.CanvasGraph();
            this.SuspendLayout();
            // 
            // canvasGraph1
            // 
            this.canvasGraph1.AllowDrop = true;
            this.canvasGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasGraph1.IfShowGrid = true;
            this.canvasGraph1.IfShowStatus = true;
            this.canvasGraph1.Location = new System.Drawing.Point(0, 0);
            this.canvasGraph1.Name = "canvasGraph1";
            this.canvasGraph1.Size = new System.Drawing.Size(584, 362);
            this.canvasGraph1.TabIndex = 0;
            this.canvasGraph1.WorkSize = new System.Drawing.Size(600, 400);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.canvasGraph1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model Designer";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TCM.Model.Designer.CanvasGraph canvasGraph1;
    }
}

