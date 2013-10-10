namespace PIE.Controls
{
    partial class DlgDataDef
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtOK = new System.Windows.Forms.Button();
            this.BtCancel = new System.Windows.Forms.Button();
            this.CtrlDataVar = new Controls.UCDataVar();
            this.CtrlOutput = new Controls.UCOutput();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "输入数据：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "输出数据：";
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(291, 391);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(80, 26);
            this.BtOK.TabIndex = 4;
            this.BtOK.Text = "确定";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(377, 391);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 26);
            this.BtCancel.TabIndex = 5;
            this.BtCancel.Text = "取消";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // CtrlDataVar
            // 
            this.CtrlDataVar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CtrlDataVar.Location = new System.Drawing.Point(12, 27);
            this.CtrlDataVar.Name = "CtrlDataVar";
            this.CtrlDataVar.Size = new System.Drawing.Size(445, 250);
            this.CtrlDataVar.TabIndex = 1;
            // 
            // CtrlOutput
            // 
            this.CtrlOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CtrlOutput.Location = new System.Drawing.Point(12, 310);
            this.CtrlOutput.Name = "CtrlOutput";
            this.CtrlOutput.Size = new System.Drawing.Size(445, 65);
            this.CtrlOutput.TabIndex = 0;
            // 
            // FrmCfgData
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(469, 429);
            this.ControlBox = false;
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CtrlDataVar);
            this.Controls.Add(this.CtrlOutput);
            this.Name = "FrmCfgData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配置数据";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCOutput CtrlOutput;
        private UCDataVar CtrlDataVar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtOK;
        private System.Windows.Forms.Button BtCancel;
    }
}