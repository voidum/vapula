namespace PIE.Controls
{
    partial class DlgDataBand
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
            this.TbxFile = new System.Windows.Forms.TextBox();
            this.BtBrowse = new System.Windows.Forms.Button();
            this.BtOK = new System.Windows.Forms.Button();
            this.BtCancel = new System.Windows.Forms.Button();
            this.dlgfile = new System.Windows.Forms.OpenFileDialog();
            this.BtSelectSpectralSubset = new System.Windows.Forms.Button();
            this.BtSelectSpatialSubset = new System.Windows.Forms.Button();
            this.LblSpec = new System.Windows.Forms.Label();
            this.LblSpat = new System.Windows.Forms.Label();
            this.BtSetHeader = new System.Windows.Forms.Button();
            this.BtViewSpectral = new System.Windows.Forms.Button();
            this.ChbxAsSpectral = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件名：";
            // 
            // TbxFile
            // 
            this.TbxFile.Location = new System.Drawing.Point(59, 9);
            this.TbxFile.Name = "TbxFile";
            this.TbxFile.ReadOnly = true;
            this.TbxFile.Size = new System.Drawing.Size(207, 21);
            this.TbxFile.TabIndex = 1;
            // 
            // BtBrowse
            // 
            this.BtBrowse.Location = new System.Drawing.Point(272, 7);
            this.BtBrowse.Name = "BtBrowse";
            this.BtBrowse.Size = new System.Drawing.Size(75, 25);
            this.BtBrowse.TabIndex = 2;
            this.BtBrowse.Text = "浏览...";
            this.BtBrowse.UseVisualStyleBackColor = true;
            this.BtBrowse.Click += new System.EventHandler(this.BtBrowse_Click);
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(181, 144);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(80, 26);
            this.BtOK.TabIndex = 8;
            this.BtOK.Text = "确定";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(267, 144);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 26);
            this.BtCancel.TabIndex = 9;
            this.BtCancel.Text = "取消";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // dlgfile
            // 
            this.dlgfile.Filter = "所有数据|*.*";
            // 
            // BtSelectSpectralSubset
            // 
            this.BtSelectSpectralSubset.Location = new System.Drawing.Point(13, 71);
            this.BtSelectSpectralSubset.Name = "BtSelectSpectralSubset";
            this.BtSelectSpectralSubset.Size = new System.Drawing.Size(110, 26);
            this.BtSelectSpectralSubset.TabIndex = 15;
            this.BtSelectSpectralSubset.Text = "选取光谱子集...";
            this.BtSelectSpectralSubset.UseVisualStyleBackColor = true;
            this.BtSelectSpectralSubset.Click += new System.EventHandler(this.BtSelectSpectralSubset_Click);
            // 
            // BtSelectSpatialSubset
            // 
            this.BtSelectSpatialSubset.Location = new System.Drawing.Point(13, 103);
            this.BtSelectSpatialSubset.Name = "BtSelectSpatialSubset";
            this.BtSelectSpatialSubset.Size = new System.Drawing.Size(110, 26);
            this.BtSelectSpatialSubset.TabIndex = 16;
            this.BtSelectSpatialSubset.Text = "选取空间子集...";
            this.BtSelectSpatialSubset.UseVisualStyleBackColor = true;
            this.BtSelectSpatialSubset.Click += new System.EventHandler(this.BtSelectSpatialSubset_Click);
            // 
            // LblSpec
            // 
            this.LblSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblSpec.Location = new System.Drawing.Point(129, 71);
            this.LblSpec.Name = "LblSpec";
            this.LblSpec.Size = new System.Drawing.Size(217, 26);
            this.LblSpec.TabIndex = 17;
            this.LblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblSpat
            // 
            this.LblSpat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblSpat.Location = new System.Drawing.Point(129, 103);
            this.LblSpat.Name = "LblSpat";
            this.LblSpat.Size = new System.Drawing.Size(217, 26);
            this.LblSpat.TabIndex = 18;
            this.LblSpat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtSetHeader
            // 
            this.BtSetHeader.Location = new System.Drawing.Point(238, 38);
            this.BtSetHeader.Name = "BtSetHeader";
            this.BtSetHeader.Size = new System.Drawing.Size(109, 26);
            this.BtSetHeader.TabIndex = 24;
            this.BtSetHeader.Text = "设置数据头...";
            this.BtSetHeader.UseVisualStyleBackColor = true;
            // 
            // BtViewSpectral
            // 
            this.BtViewSpectral.Location = new System.Drawing.Point(152, 38);
            this.BtViewSpectral.Name = "BtViewSpectral";
            this.BtViewSpectral.Size = new System.Drawing.Size(80, 26);
            this.BtViewSpectral.TabIndex = 23;
            this.BtViewSpectral.Text = "查看光谱";
            this.BtViewSpectral.UseVisualStyleBackColor = true;
            // 
            // ChbxAsSpectral
            // 
            this.ChbxAsSpectral.AutoSize = true;
            this.ChbxAsSpectral.Location = new System.Drawing.Point(14, 44);
            this.ChbxAsSpectral.Name = "ChbxAsSpectral";
            this.ChbxAsSpectral.Size = new System.Drawing.Size(132, 16);
            this.ChbxAsSpectral.TabIndex = 22;
            this.ChbxAsSpectral.Text = "以光谱形式组织数据";
            this.ChbxAsSpectral.UseVisualStyleBackColor = true;
            this.ChbxAsSpectral.CheckedChanged += new System.EventHandler(this.ChbxAsSpectral_CheckedChanged);
            // 
            // DlgDataBand
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(359, 182);
            this.ControlBox = false;
            this.Controls.Add(this.BtSetHeader);
            this.Controls.Add(this.BtViewSpectral);
            this.Controls.Add(this.ChbxAsSpectral);
            this.Controls.Add(this.LblSpat);
            this.Controls.Add(this.LblSpec);
            this.Controls.Add(this.BtSelectSpatialSubset);
            this.Controls.Add(this.BtSelectSpectralSubset);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOK);
            this.Controls.Add(this.BtBrowse);
            this.Controls.Add(this.TbxFile);
            this.Controls.Add(this.label1);
            this.Name = "DlgDataBand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "映射数据源";
            this.Load += new System.EventHandler(this.FrmMappingDataVar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbxFile;
        private System.Windows.Forms.Button BtBrowse;
        private System.Windows.Forms.Button BtOK;
        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.OpenFileDialog dlgfile;
        private System.Windows.Forms.Button BtSelectSpectralSubset;
        private System.Windows.Forms.Button BtSelectSpatialSubset;
        private System.Windows.Forms.Label LblSpec;
        private System.Windows.Forms.Label LblSpat;
        private System.Windows.Forms.Button BtSetHeader;
        private System.Windows.Forms.Button BtViewSpectral;
        private System.Windows.Forms.CheckBox ChbxAsSpectral;
    }
}