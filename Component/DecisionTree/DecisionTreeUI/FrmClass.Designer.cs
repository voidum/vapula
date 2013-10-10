namespace DecisionTreeUI
{
    partial class FrmClass
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
            this.Lbl1 = new System.Windows.Forms.Label();
            this.Lbl2 = new System.Windows.Forms.Label();
            this.TbxName = new System.Windows.Forms.TextBox();
            this.Lbl3 = new System.Windows.Forms.Label();
            this.LblColor = new System.Windows.Forms.Label();
            this.LblId = new System.Windows.Forms.Label();
            this.dlgcolor = new System.Windows.Forms.ColorDialog();
            this.BtOK = new System.Windows.Forms.Button();
            this.BtCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lbl1
            // 
            this.Lbl1.Location = new System.Drawing.Point(24, 9);
            this.Lbl1.Name = "Lbl1";
            this.Lbl1.Size = new System.Drawing.Size(48, 12);
            this.Lbl1.TabIndex = 0;
            this.Lbl1.Text = "Index:";
            this.Lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Lbl2
            // 
            this.Lbl2.Location = new System.Drawing.Point(24, 32);
            this.Lbl2.Name = "Lbl2";
            this.Lbl2.Size = new System.Drawing.Size(48, 21);
            this.Lbl2.TabIndex = 1;
            this.Lbl2.Text = "Name:";
            this.Lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TbxName
            // 
            this.TbxName.Location = new System.Drawing.Point(78, 32);
            this.TbxName.Name = "TbxName";
            this.TbxName.Size = new System.Drawing.Size(174, 21);
            this.TbxName.TabIndex = 2;
            // 
            // Lbl3
            // 
            this.Lbl3.Location = new System.Drawing.Point(22, 59);
            this.Lbl3.Name = "Lbl3";
            this.Lbl3.Size = new System.Drawing.Size(50, 20);
            this.Lbl3.TabIndex = 4;
            this.Lbl3.Text = "Color:";
            this.Lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblColor
            // 
            this.LblColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblColor.Location = new System.Drawing.Point(78, 59);
            this.LblColor.Name = "LblColor";
            this.LblColor.Size = new System.Drawing.Size(174, 20);
            this.LblColor.TabIndex = 5;
            this.LblColor.Click += new System.EventHandler(this.LblColor_Click);
            // 
            // LblId
            // 
            this.LblId.AutoSize = true;
            this.LblId.Location = new System.Drawing.Point(75, 9);
            this.LblId.Name = "LblId";
            this.LblId.Size = new System.Drawing.Size(65, 12);
            this.LblId.TabIndex = 6;
            this.LblId.Text = "(No value)";
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(86, 89);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(80, 26);
            this.BtOK.TabIndex = 7;
            this.BtOK.Text = "Confirm";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(172, 89);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 26);
            this.BtCancel.TabIndex = 8;
            this.BtCancel.Text = "Cancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            // 
            // FrmClass
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(264, 124);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOK);
            this.Controls.Add(this.LblId);
            this.Controls.Add(this.LblColor);
            this.Controls.Add(this.Lbl3);
            this.Controls.Add(this.TbxName);
            this.Controls.Add(this.Lbl2);
            this.Controls.Add(this.Lbl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Class Property";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmClass_FormClosing);
            this.Load += new System.EventHandler(this.FrmClass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl1;
        private System.Windows.Forms.Label Lbl2;
        private System.Windows.Forms.TextBox TbxName;
        private System.Windows.Forms.Label Lbl3;
        private System.Windows.Forms.Label LblColor;
        private System.Windows.Forms.Label LblId;
        private System.Windows.Forms.ColorDialog dlgcolor;
        private System.Windows.Forms.Button BtOK;
        private System.Windows.Forms.Button BtCancel;
    }
}