namespace PIE.Controls
{
    partial class UCOutput
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dlgfolder = new System.Windows.Forms.FolderBrowserDialog();
            this.TbxDirectory = new System.Windows.Forms.TextBox();
            this.TbxFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtBrowse = new System.Windows.Forms.Button();
            this.CobxFormat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出目录：";
            // 
            // dlgfolder
            // 
            this.dlgfolder.Description = "选择数据输出的目录。";
            // 
            // TbxDirectory
            // 
            this.TbxDirectory.Location = new System.Drawing.Point(68, 7);
            this.TbxDirectory.Name = "TbxDirectory";
            this.TbxDirectory.ReadOnly = true;
            this.TbxDirectory.Size = new System.Drawing.Size(240, 21);
            this.TbxDirectory.TabIndex = 1;
            // 
            // TbxFile
            // 
            this.TbxFile.Location = new System.Drawing.Point(68, 37);
            this.TbxFile.Name = "TbxFile";
            this.TbxFile.Size = new System.Drawing.Size(200, 21);
            this.TbxFile.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "输出文件：";
            // 
            // BtBrowse
            // 
            this.BtBrowse.Location = new System.Drawing.Point(314, 4);
            this.BtBrowse.Name = "BtBrowse";
            this.BtBrowse.Size = new System.Drawing.Size(79, 25);
            this.BtBrowse.TabIndex = 4;
            this.BtBrowse.Text = "浏览...";
            this.BtBrowse.UseVisualStyleBackColor = true;
            this.BtBrowse.Click += new System.EventHandler(this.BtBrowse_Click);
            // 
            // CobxFormat
            // 
            this.CobxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CobxFormat.FormattingEnabled = true;
            this.CobxFormat.Items.AddRange(new object[] {
            "GeoTIFF",
            "ENVI"});
            this.CobxFormat.Location = new System.Drawing.Point(274, 36);
            this.CobxFormat.Name = "CobxFormat";
            this.CobxFormat.Size = new System.Drawing.Size(119, 20);
            this.CobxFormat.TabIndex = 5;
            // 
            // UCConfigOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CobxFormat);
            this.Controls.Add(this.BtBrowse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbxFile);
            this.Controls.Add(this.TbxDirectory);
            this.Controls.Add(this.label1);
            this.Name = "UCConfigOutput";
            this.Size = new System.Drawing.Size(400, 65);
            this.Resize += new System.EventHandler(this.UCConfigOutput_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog dlgfolder;
        private System.Windows.Forms.TextBox TbxDirectory;
        private System.Windows.Forms.TextBox TbxFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtBrowse;
        private System.Windows.Forms.ComboBox CobxFormat;
        private System.Windows.Forms.Label label1;
    }
}
