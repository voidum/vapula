namespace Vapula.Toolkit
{
    partial class FrmParameter
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
            this.label4 = new System.Windows.Forms.Label();
            this.CobxDataType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Grp1 = new System.Windows.Forms.GroupBox();
            this.CobxParamMode = new System.Windows.Forms.ComboBox();
            this.Grp2 = new System.Windows.Forms.GroupBox();
            this.Grp1.SuspendLayout();
            this.Grp2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TbxId
            // 
            this.TbxId.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TbxId.Location = new System.Drawing.Point(65, 20);
            this.TbxId.MaxLength = 500;
            this.TbxId.Name = "TbxId";
            this.TbxId.ReadOnly = true;
            this.TbxId.Size = new System.Drawing.Size(275, 23);
            this.TbxId.TabIndex = 7;
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(282, 270);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(90, 30);
            this.BtCancel.TabIndex = 1;
            this.BtCancel.Text = "取消";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(186, 270);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(90, 30);
            this.BtOK.TabIndex = 0;
            this.BtOK.Text = "确认";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // TbxDescription
            // 
            this.TbxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbxDescription.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TbxDescription.Location = new System.Drawing.Point(65, 49);
            this.TbxDescription.MaxLength = 2000;
            this.TbxDescription.Multiline = true;
            this.TbxDescription.Name = "TbxDescription";
            this.TbxDescription.Size = new System.Drawing.Size(275, 50);
            this.TbxDescription.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "描述：";
            // 
            // TbxName
            // 
            this.TbxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbxName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TbxName.Location = new System.Drawing.Point(65, 20);
            this.TbxName.MaxLength = 500;
            this.TbxName.Name = "TbxName";
            this.TbxName.Size = new System.Drawing.Size(275, 23);
            this.TbxName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "标识：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "模式：";
            // 
            // CobxDataType
            // 
            this.CobxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CobxDataType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CobxDataType.FormattingEnabled = true;
            this.CobxDataType.ItemHeight = 17;
            this.CobxDataType.Location = new System.Drawing.Point(65, 80);
            this.CobxDataType.Name = "CobxDataType";
            this.CobxDataType.Size = new System.Drawing.Size(275, 25);
            this.CobxDataType.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "类型：";
            // 
            // Grp1
            // 
            this.Grp1.Controls.Add(this.CobxParamMode);
            this.Grp1.Controls.Add(this.TbxId);
            this.Grp1.Controls.Add(this.label6);
            this.Grp1.Controls.Add(this.label1);
            this.Grp1.Controls.Add(this.CobxDataType);
            this.Grp1.Controls.Add(this.label4);
            this.Grp1.Location = new System.Drawing.Point(12, 12);
            this.Grp1.Name = "Grp1";
            this.Grp1.Size = new System.Drawing.Size(360, 120);
            this.Grp1.TabIndex = 14;
            this.Grp1.TabStop = false;
            this.Grp1.Text = "基本";
            // 
            // CobxParamMode
            // 
            this.CobxParamMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CobxParamMode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CobxParamMode.FormattingEnabled = true;
            this.CobxParamMode.ItemHeight = 17;
            this.CobxParamMode.Items.AddRange(new object[] {
            "指针",
            "8位整数",
            "16位整数",
            "32位整数",
            "64位整数",
            "8位无符号整数",
            "16位无符号整数",
            "32位无符号整数",
            "64位无符号整数",
            "32位浮点数",
            "64位浮点数",
            "布尔变量",
            "多字节字符串",
            "宽字节字符串"});
            this.CobxParamMode.Location = new System.Drawing.Point(65, 49);
            this.CobxParamMode.Name = "CobxParamMode";
            this.CobxParamMode.Size = new System.Drawing.Size(275, 25);
            this.CobxParamMode.TabIndex = 14;
            // 
            // Grp2
            // 
            this.Grp2.Controls.Add(this.TbxName);
            this.Grp2.Controls.Add(this.label2);
            this.Grp2.Controls.Add(this.TbxDescription);
            this.Grp2.Controls.Add(this.label5);
            this.Grp2.Location = new System.Drawing.Point(12, 138);
            this.Grp2.Name = "Grp2";
            this.Grp2.Size = new System.Drawing.Size(360, 115);
            this.Grp2.TabIndex = 15;
            this.Grp2.TabStop = false;
            this.Grp2.Text = "标签";
            // 
            // FrmParameter
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(384, 312);
            this.ControlBox = false;
            this.Controls.Add(this.Grp2);
            this.Controls.Add(this.Grp1);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOK);
            this.Name = "FrmParameter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "参数属性";
            this.Grp1.ResumeLayout(false);
            this.Grp1.PerformLayout();
            this.Grp2.ResumeLayout(false);
            this.Grp2.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CobxDataType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox Grp1;
        private System.Windows.Forms.ComboBox CobxParamMode;
        private System.Windows.Forms.GroupBox Grp2;
    }
}