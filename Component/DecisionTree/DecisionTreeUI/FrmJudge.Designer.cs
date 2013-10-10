namespace DecisionTreeUI
{
    partial class FrmJudge
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
            this.Lbl1 = new System.Windows.Forms.Label();
            this.TbxName = new System.Windows.Forms.TextBox();
            this.Lbl2 = new System.Windows.Forms.Label();
            this.TbxCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(228, 106);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 26);
            this.BtCancel.TabIndex = 0;
            this.BtCancel.Text = "Cancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(142, 106);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(80, 26);
            this.BtOK.TabIndex = 1;
            this.BtOK.Text = "Confirm";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // Lbl1
            // 
            this.Lbl1.Location = new System.Drawing.Point(8, 11);
            this.Lbl1.Name = "Lbl1";
            this.Lbl1.Size = new System.Drawing.Size(60, 21);
            this.Lbl1.TabIndex = 2;
            this.Lbl1.Text = "Name:";
            this.Lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TbxName
            // 
            this.TbxName.Location = new System.Drawing.Point(74, 12);
            this.TbxName.Name = "TbxName";
            this.TbxName.Size = new System.Drawing.Size(234, 21);
            this.TbxName.TabIndex = 3;
            // 
            // Lbl2
            // 
            this.Lbl2.Location = new System.Drawing.Point(8, 40);
            this.Lbl2.Name = "Lbl2";
            this.Lbl2.Size = new System.Drawing.Size(60, 17);
            this.Lbl2.TabIndex = 4;
            this.Lbl2.Text = "Expr:";
            this.Lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TbxCode
            // 
            this.TbxCode.Location = new System.Drawing.Point(74, 39);
            this.TbxCode.Multiline = true;
            this.TbxCode.Name = "TbxCode";
            this.TbxCode.Size = new System.Drawing.Size(234, 61);
            this.TbxCode.TabIndex = 5;
            // 
            // FrmJudge
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(320, 144);
            this.Controls.Add(this.TbxCode);
            this.Controls.Add(this.Lbl2);
            this.Controls.Add(this.TbxName);
            this.Controls.Add(this.Lbl1);
            this.Controls.Add(this.BtOK);
            this.Controls.Add(this.BtCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmJudge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Judge Property";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.Button BtOK;
        private System.Windows.Forms.Label Lbl1;
        private System.Windows.Forms.TextBox TbxName;
        private System.Windows.Forms.Label Lbl2;
        private System.Windows.Forms.TextBox TbxCode;
    }
}