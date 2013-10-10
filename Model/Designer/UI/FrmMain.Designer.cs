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
            this.canvas = new TCM.Model.Designer.CanvasGraph();
            this.ribbon1 = new RibbonLib.Ribbon();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.AllowDrop = true;
            this.canvas.IfShowGrid = true;
            this.canvas.IfShowStatus = true;
            this.canvas.Location = new System.Drawing.Point(0, 105);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(584, 397);
            this.canvas.TabIndex = 0;
            this.canvas.WorkSize = new System.Drawing.Size(600, 400);
            // 
            // ribbon1
            // 
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            this.ribbon1.ResourceName = null;
            this.ribbon1.ShortcutTableResourceName = null;
            this.ribbon1.Size = new System.Drawing.Size(584, 99);
            this.ribbon1.TabIndex = 1;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.ribbon1);
            this.Controls.Add(this.canvas);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model Designer";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TCM.Model.Designer.CanvasGraph canvas;
        private RibbonLib.Ribbon ribbon1;
    }
}

