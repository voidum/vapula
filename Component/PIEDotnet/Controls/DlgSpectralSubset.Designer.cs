namespace PIE.Controls
{
    partial class DlgSpectralSubset
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
            this.LsbBands = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TbxMax = new System.Windows.Forms.TextBox();
            this.TbxMin = new System.Windows.Forms.TextBox();
            this.LblStat = new System.Windows.Forms.Label();
            this.ChbxSelectAll = new System.Windows.Forms.CheckBox();
            this.BtAddRange = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtCancel = new System.Windows.Forms.Button();
            this.BtOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LsbBands
            // 
            this.LsbBands.CheckOnClick = true;
            this.LsbBands.HorizontalScrollbar = true;
            this.LsbBands.Location = new System.Drawing.Point(12, 31);
            this.LsbBands.MultiColumn = true;
            this.LsbBands.Name = "LsbBands";
            this.LsbBands.Size = new System.Drawing.Size(250, 100);
            this.LsbBands.TabIndex = 20;
            this.LsbBands.ThreeDCheckBoxes = true;
            this.LsbBands.SelectedIndexChanged += new System.EventHandler(this.LsbBands_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "-";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TbxMax
            // 
            this.TbxMax.Location = new System.Drawing.Point(52, 159);
            this.TbxMax.MaxLength = 10;
            this.TbxMax.Name = "TbxMax";
            this.TbxMax.Size = new System.Drawing.Size(25, 21);
            this.TbxMax.TabIndex = 18;
            this.TbxMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbxMin
            // 
            this.TbxMin.Location = new System.Drawing.Point(12, 159);
            this.TbxMin.MaxLength = 10;
            this.TbxMin.Name = "TbxMin";
            this.TbxMin.Size = new System.Drawing.Size(25, 21);
            this.TbxMin.TabIndex = 17;
            this.TbxMin.Text = "1";
            this.TbxMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LblStat
            // 
            this.LblStat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblStat.Location = new System.Drawing.Point(12, 130);
            this.LblStat.Name = "LblStat";
            this.LblStat.Size = new System.Drawing.Size(250, 23);
            this.LblStat.TabIndex = 16;
            this.LblStat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChbxSelectAll
            // 
            this.ChbxSelectAll.AutoSize = true;
            this.ChbxSelectAll.Location = new System.Drawing.Point(172, 163);
            this.ChbxSelectAll.Name = "ChbxSelectAll";
            this.ChbxSelectAll.Size = new System.Drawing.Size(90, 16);
            this.ChbxSelectAll.TabIndex = 15;
            this.ChbxSelectAll.Text = "全选/全不选";
            this.ChbxSelectAll.UseVisualStyleBackColor = true;
            this.ChbxSelectAll.CheckedChanged += new System.EventHandler(this.ChbxSelectAll_CheckedChanged);
            // 
            // BtAddRange
            // 
            this.BtAddRange.Location = new System.Drawing.Point(81, 157);
            this.BtAddRange.Name = "BtAddRange";
            this.BtAddRange.Size = new System.Drawing.Size(75, 25);
            this.BtAddRange.TabIndex = 14;
            this.BtAddRange.Text = "批量选取";
            this.BtAddRange.UseVisualStyleBackColor = true;
            this.BtAddRange.Click += new System.EventHandler(this.BtAddRange_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "可用波段：";
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(182, 200);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 26);
            this.BtCancel.TabIndex = 21;
            this.BtCancel.Text = "取消";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(96, 200);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(80, 26);
            this.BtOK.TabIndex = 22;
            this.BtOK.Text = "确认";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // DlgSpectralSubset
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(274, 237);
            this.ControlBox = false;
            this.Controls.Add(this.BtOK);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.LsbBands);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbxMax);
            this.Controls.Add(this.TbxMin);
            this.Controls.Add(this.LblStat);
            this.Controls.Add(this.ChbxSelectAll);
            this.Controls.Add(this.BtAddRange);
            this.Controls.Add(this.label1);
            this.Name = "DlgSpectralSubset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选取光谱子集";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox LsbBands;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbxMax;
        private System.Windows.Forms.TextBox TbxMin;
        private System.Windows.Forms.Label LblStat;
        private System.Windows.Forms.CheckBox ChbxSelectAll;
        private System.Windows.Forms.Button BtAddRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.Button BtOK;
    }
}