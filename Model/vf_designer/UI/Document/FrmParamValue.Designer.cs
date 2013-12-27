namespace Vapula.Designer
{
    partial class FrmParamValue
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
            this.BtOK = new System.Windows.Forms.Button();
            this.BtCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TbxValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(186, 162);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(90, 30);
            this.BtOK.TabIndex = 0;
            this.BtOK.Text = "确认";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(282, 162);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(90, 30);
            this.BtCancel.TabIndex = 1;
            this.BtCancel.Text = "取消";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "参数值：";
            // 
            // TbxValue
            // 
            this.TbxValue.AcceptsReturn = true;
            this.TbxValue.AcceptsTab = true;
            this.TbxValue.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.TbxValue.Location = new System.Drawing.Point(12, 40);
            this.TbxValue.Multiline = true;
            this.TbxValue.Name = "TbxValue";
            this.TbxValue.Size = new System.Drawing.Size(360, 96);
            this.TbxValue.TabIndex = 4;
            this.TbxValue.WordWrap = false;
            // 
            // FrmParamValue
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(384, 204);
            this.ControlBox = false;
            this.Controls.Add(this.TbxValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(400, 240);
            this.MinimumSize = new System.Drawing.Size(400, 240);
            this.Name = "FrmParamValue";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置参数值";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtOK;
        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbxValue;
    }
}