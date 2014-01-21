namespace Vapula.Toolkit
{
    partial class FrmLibrary
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
            this.Grp1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CobxRuntime = new System.Windows.Forms.ComboBox();
            this.TbxId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Grp2 = new System.Windows.Forms.GroupBox();
            this.TbxDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TbxPublisher = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TbxVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TbxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Grp1.SuspendLayout();
            this.Grp2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(186, 295);
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
            this.BtCancel.Location = new System.Drawing.Point(282, 295);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(90, 30);
            this.BtCancel.TabIndex = 1;
            this.BtCancel.Text = "取消";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // Grp1
            // 
            this.Grp1.Controls.Add(this.label6);
            this.Grp1.Controls.Add(this.CobxRuntime);
            this.Grp1.Controls.Add(this.TbxId);
            this.Grp1.Controls.Add(this.label1);
            this.Grp1.Location = new System.Drawing.Point(12, 12);
            this.Grp1.Name = "Grp1";
            this.Grp1.Size = new System.Drawing.Size(360, 90);
            this.Grp1.TabIndex = 14;
            this.Grp1.TabStop = false;
            this.Grp1.Text = "基本";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "环境：";
            // 
            // CobxRuntime
            // 
            this.CobxRuntime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CobxRuntime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CobxRuntime.FormattingEnabled = true;
            this.CobxRuntime.Location = new System.Drawing.Point(65, 50);
            this.CobxRuntime.Name = "CobxRuntime";
            this.CobxRuntime.Size = new System.Drawing.Size(275, 25);
            this.CobxRuntime.TabIndex = 16;
            // 
            // TbxId
            // 
            this.TbxId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbxId.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.TbxId.Location = new System.Drawing.Point(65, 20);
            this.TbxId.MaxLength = 500;
            this.TbxId.Name = "TbxId";
            this.TbxId.ReadOnly = true;
            this.TbxId.Size = new System.Drawing.Size(275, 23);
            this.TbxId.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "标识：";
            // 
            // Grp2
            // 
            this.Grp2.Controls.Add(this.TbxDescription);
            this.Grp2.Controls.Add(this.label5);
            this.Grp2.Controls.Add(this.TbxPublisher);
            this.Grp2.Controls.Add(this.label4);
            this.Grp2.Controls.Add(this.TbxVersion);
            this.Grp2.Controls.Add(this.label3);
            this.Grp2.Controls.Add(this.TbxName);
            this.Grp2.Controls.Add(this.label2);
            this.Grp2.Location = new System.Drawing.Point(12, 108);
            this.Grp2.Name = "Grp2";
            this.Grp2.Size = new System.Drawing.Size(360, 175);
            this.Grp2.TabIndex = 15;
            this.Grp2.TabStop = false;
            this.Grp2.Text = "标签";
            // 
            // TbxDescription
            // 
            this.TbxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbxDescription.Location = new System.Drawing.Point(65, 110);
            this.TbxDescription.MaxLength = 2000;
            this.TbxDescription.Multiline = true;
            this.TbxDescription.Name = "TbxDescription";
            this.TbxDescription.Size = new System.Drawing.Size(275, 50);
            this.TbxDescription.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "描述：";
            // 
            // TbxPublisher
            // 
            this.TbxPublisher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbxPublisher.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TbxPublisher.Location = new System.Drawing.Point(65, 81);
            this.TbxPublisher.MaxLength = 500;
            this.TbxPublisher.Name = "TbxPublisher";
            this.TbxPublisher.Size = new System.Drawing.Size(275, 23);
            this.TbxPublisher.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "发布：";
            // 
            // TbxVersion
            // 
            this.TbxVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbxVersion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TbxVersion.Location = new System.Drawing.Point(65, 52);
            this.TbxVersion.MaxLength = 500;
            this.TbxVersion.Name = "TbxVersion";
            this.TbxVersion.Size = new System.Drawing.Size(275, 23);
            this.TbxVersion.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "版本：";
            // 
            // TbxName
            // 
            this.TbxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbxName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TbxName.Location = new System.Drawing.Point(65, 23);
            this.TbxName.MaxLength = 500;
            this.TbxName.Name = "TbxName";
            this.TbxName.Size = new System.Drawing.Size(275, 23);
            this.TbxName.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "名称：";
            // 
            // FrmLibrary
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(384, 337);
            this.ControlBox = false;
            this.Controls.Add(this.Grp2);
            this.Controls.Add(this.Grp1);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOK);
            this.Name = "FrmLibrary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "组件属性";
            this.Grp1.ResumeLayout(false);
            this.Grp1.PerformLayout();
            this.Grp2.ResumeLayout(false);
            this.Grp2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtOK;
        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.GroupBox Grp1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CobxRuntime;
        private System.Windows.Forms.TextBox TbxId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox Grp2;
        private System.Windows.Forms.TextBox TbxDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TbxPublisher;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TbxVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbxName;
        private System.Windows.Forms.Label label2;
    }
}