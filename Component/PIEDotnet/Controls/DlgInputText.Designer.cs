namespace PIE.Controls
{
    partial class DlgInputText
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
            this.BtCancel = new System.Windows.Forms.Button();
            this.BtOK = new System.Windows.Forms.Button();
            this.TbxValue = new System.Windows.Forms.TextBox();
            this.LblText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(192, 59);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 26);
            this.BtCancel.TabIndex = 13;
            this.BtCancel.Text = "Cancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(106, 59);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(80, 26);
            this.BtOK.TabIndex = 12;
            this.BtOK.Text = "Confirm";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // TbxValue
            // 
            this.TbxValue.Location = new System.Drawing.Point(12, 30);
            this.TbxValue.Name = "TbxValue";
            this.TbxValue.Size = new System.Drawing.Size(260, 21);
            this.TbxValue.TabIndex = 11;
            // 
            // LblText
            // 
            this.LblText.AutoSize = true;
            this.LblText.Location = new System.Drawing.Point(15, 12);
            this.LblText.Name = "LblText";
            this.LblText.Size = new System.Drawing.Size(113, 12);
            this.LblText.TabIndex = 14;
            this.LblText.Text = "请输入要求的内容：";
            // 
            // DlgInputText
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(284, 97);
            this.ControlBox = false;
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOK);
            this.Controls.Add(this.TbxValue);
            this.Controls.Add(this.LblText);
            this.Name = "DlgInputText";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "询问";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.Button BtOK;
        private System.Windows.Forms.TextBox TbxValue;
        private System.Windows.Forms.Label LblText;
    }
}