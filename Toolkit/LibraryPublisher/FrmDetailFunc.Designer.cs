namespace TCM.Toolkit
{
    partial class FrmDetailFunc
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
            this.TbxId = new System.Windows.Forms.TextBox();
            this.BtCancel = new System.Windows.Forms.Button();
            this.BtOK = new System.Windows.Forms.Button();
            this.TbxDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TbxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TbxId
            // 
            this.TbxId.Location = new System.Drawing.Point(58, 12);
            this.TbxId.MaxLength = 500;
            this.TbxId.Name = "TbxId";
            this.TbxId.ReadOnly = true;
            this.TbxId.Size = new System.Drawing.Size(194, 21);
            this.TbxId.TabIndex = 4;
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(172, 170);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 26);
            this.BtCancel.TabIndex = 1;
            this.BtCancel.Text = "取消";
            this.BtCancel.UseVisualStyleBackColor = true;
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(86, 170);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(80, 26);
            this.BtOK.TabIndex = 0;
            this.BtOK.Text = "确认";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // TbxDescription
            // 
            this.TbxDescription.Location = new System.Drawing.Point(58, 66);
            this.TbxDescription.MaxLength = 2000;
            this.TbxDescription.Multiline = true;
            this.TbxDescription.Name = "TbxDescription";
            this.TbxDescription.Size = new System.Drawing.Size(194, 98);
            this.TbxDescription.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "描述：";
            // 
            // TbxName
            // 
            this.TbxName.Location = new System.Drawing.Point(58, 39);
            this.TbxName.MaxLength = 500;
            this.TbxName.Name = "TbxName";
            this.TbxName.Size = new System.Drawing.Size(194, 21);
            this.TbxName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "标识：";
            // 
            // FrmDetailFunc
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(264, 208);
            this.ControlBox = false;
            this.Controls.Add(this.TbxId);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOK);
            this.Controls.Add(this.TbxDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TbxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmDetailFunc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "功能属性";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TbxId;
        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.Button BtOK;
        private System.Windows.Forms.TextBox TbxDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TbxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}