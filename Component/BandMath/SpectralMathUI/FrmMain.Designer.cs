namespace SpectralMathUI
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
            this.BtExecute = new System.Windows.Forms.Button();
            this.TbxExpr = new System.Windows.Forms.TextBox();
            this.BtCfgData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtExecute
            // 
            this.BtExecute.Location = new System.Drawing.Point(142, 55);
            this.BtExecute.Name = "BtExecute";
            this.BtExecute.Size = new System.Drawing.Size(90, 25);
            this.BtExecute.TabIndex = 8;
            this.BtExecute.Text = "执行运算";
            this.BtExecute.UseVisualStyleBackColor = true;
            // 
            // TbxExpr
            // 
            this.TbxExpr.Location = new System.Drawing.Point(12, 24);
            this.TbxExpr.Name = "TbxExpr";
            this.TbxExpr.Size = new System.Drawing.Size(220, 21);
            this.TbxExpr.TabIndex = 7;
            // 
            // BtCfgData
            // 
            this.BtCfgData.Location = new System.Drawing.Point(12, 55);
            this.BtCfgData.Name = "BtCfgData";
            this.BtCfgData.Size = new System.Drawing.Size(90, 25);
            this.BtCfgData.TabIndex = 6;
            this.BtCfgData.Text = "配置数据...";
            this.BtCfgData.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "表达式：";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 92);
            this.Controls.Add(this.BtExecute);
            this.Controls.Add(this.TbxExpr);
            this.Controls.Add(this.BtCfgData);
            this.Controls.Add(this.label1);
            this.Name = "FrmMain";
            this.Text = "波谱运算";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtExecute;
        private System.Windows.Forms.TextBox TbxExpr;
        private System.Windows.Forms.Button BtCfgData;
        private System.Windows.Forms.Label label1;
    }
}

