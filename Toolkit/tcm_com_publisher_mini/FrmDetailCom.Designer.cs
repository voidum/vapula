﻿namespace TCM.ComPubMini
{
    partial class FrmDetailCom
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
            this.TbxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TbxVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TbxPublisher = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TbxDescription = new System.Windows.Forms.TextBox();
            this.BtOK = new System.Windows.Forms.Button();
            this.BtCancel = new System.Windows.Forms.Button();
            this.TbxId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "标识：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "名称：";
            // 
            // TbxName
            // 
            this.TbxName.Location = new System.Drawing.Point(58, 39);
            this.TbxName.MaxLength = 500;
            this.TbxName.Name = "TbxName";
            this.TbxName.Size = new System.Drawing.Size(194, 21);
            this.TbxName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "版本：";
            // 
            // TbxVersion
            // 
            this.TbxVersion.Location = new System.Drawing.Point(58, 66);
            this.TbxVersion.MaxLength = 500;
            this.TbxVersion.Name = "TbxVersion";
            this.TbxVersion.Size = new System.Drawing.Size(194, 21);
            this.TbxVersion.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "发布：";
            // 
            // TbxPublisher
            // 
            this.TbxPublisher.Location = new System.Drawing.Point(58, 93);
            this.TbxPublisher.MaxLength = 500;
            this.TbxPublisher.Name = "TbxPublisher";
            this.TbxPublisher.Size = new System.Drawing.Size(194, 21);
            this.TbxPublisher.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "描述：";
            // 
            // TbxDescription
            // 
            this.TbxDescription.Location = new System.Drawing.Point(58, 120);
            this.TbxDescription.MaxLength = 2000;
            this.TbxDescription.Multiline = true;
            this.TbxDescription.Name = "TbxDescription";
            this.TbxDescription.Size = new System.Drawing.Size(194, 98);
            this.TbxDescription.TabIndex = 5;
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(86, 224);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(80, 26);
            this.BtOK.TabIndex = 0;
            this.BtOK.Text = "确认";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(172, 224);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 26);
            this.BtCancel.TabIndex = 1;
            this.BtCancel.Text = "取消";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // TbxId
            // 
            this.TbxId.Location = new System.Drawing.Point(58, 12);
            this.TbxId.MaxLength = 500;
            this.TbxId.Name = "TbxId";
            this.TbxId.ReadOnly = true;
            this.TbxId.Size = new System.Drawing.Size(194, 21);
            this.TbxId.TabIndex = 6;
            // 
            // FrmDetailCom
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(264, 262);
            this.ControlBox = false;
            this.Controls.Add(this.TbxId);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOK);
            this.Controls.Add(this.TbxDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TbxPublisher);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TbxVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TbxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmDetailCom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "组件属性";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbxName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbxVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TbxPublisher;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TbxDescription;
        private System.Windows.Forms.Button BtOK;
        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.TextBox TbxId;
    }
}