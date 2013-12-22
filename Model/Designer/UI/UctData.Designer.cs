namespace Vapula.Designer.UI
{
    partial class UctData
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
            this.LsvTarget = new System.Windows.Forms.ListView();
            this.LblTarget = new System.Windows.Forms.Label();
            this.LsvSource = new System.Windows.Forms.ListView();
            this.LblSource = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LsvTarget
            // 
            this.LsvTarget.Location = new System.Drawing.Point(23, 39);
            this.LsvTarget.Name = "LsvTarget";
            this.LsvTarget.Size = new System.Drawing.Size(300, 338);
            this.LsvTarget.TabIndex = 11;
            this.LsvTarget.UseCompatibleStateImageBehavior = false;
            // 
            // LblTarget
            // 
            this.LblTarget.Location = new System.Drawing.Point(23, 20);
            this.LblTarget.Name = "LblTarget";
            this.LblTarget.Size = new System.Drawing.Size(100, 16);
            this.LblTarget.TabIndex = 10;
            this.LblTarget.Text = "待定参数";
            // 
            // LsvSource
            // 
            this.LsvSource.Location = new System.Drawing.Point(329, 39);
            this.LsvSource.Name = "LsvSource";
            this.LsvSource.Size = new System.Drawing.Size(300, 338);
            this.LsvSource.TabIndex = 13;
            this.LsvSource.UseCompatibleStateImageBehavior = false;
            // 
            // LblSource
            // 
            this.LblSource.Location = new System.Drawing.Point(327, 20);
            this.LblSource.Name = "LblSource";
            this.LblSource.Size = new System.Drawing.Size(100, 16);
            this.LblSource.TabIndex = 12;
            this.LblSource.Text = "可用参数";
            // 
            // UctData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LsvSource);
            this.Controls.Add(this.LblSource);
            this.Controls.Add(this.LsvTarget);
            this.Controls.Add(this.LblTarget);
            this.Name = "UctData";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(650, 400);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LsvTarget;
        private System.Windows.Forms.Label LblTarget;
        private System.Windows.Forms.ListView LsvSource;
        private System.Windows.Forms.Label LblSource;



    }
}
