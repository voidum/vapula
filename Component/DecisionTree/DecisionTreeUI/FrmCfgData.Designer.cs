namespace DecisionTreeUI
{
    partial class FrmCfgData
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
            this.components = new System.ComponentModel.Container();
            this.TbxOutDir = new System.Windows.Forms.TextBox();
            this.Lbl2 = new System.Windows.Forms.Label();
            this.Lbl3 = new System.Windows.Forms.Label();
            this.CobxOutFmt = new System.Windows.Forms.ComboBox();
            this.BtBrowse = new System.Windows.Forms.Button();
            this.GrpOut = new System.Windows.Forms.GroupBox();
            this.TbxOutName = new System.Windows.Forms.TextBox();
            this.LsvInput = new System.Windows.Forms.ListView();
            this.ColhVar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhBand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lbl1 = new System.Windows.Forms.Label();
            this.dlgfolder = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgopen = new System.Windows.Forms.OpenFileDialog();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.BtCancel = new System.Windows.Forms.Button();
            this.BtOK = new System.Windows.Forms.Button();
            this.GrpOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // TbxOutDir
            // 
            this.TbxOutDir.Location = new System.Drawing.Point(58, 20);
            this.TbxOutDir.Name = "TbxOutDir";
            this.TbxOutDir.ReadOnly = true;
            this.TbxOutDir.Size = new System.Drawing.Size(230, 21);
            this.TbxOutDir.TabIndex = 0;
            // 
            // Lbl2
            // 
            this.Lbl2.Location = new System.Drawing.Point(8, 20);
            this.Lbl2.Name = "Lbl2";
            this.Lbl2.Size = new System.Drawing.Size(44, 21);
            this.Lbl2.TabIndex = 1;
            this.Lbl2.Text = "Dir:";
            this.Lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Lbl3
            // 
            this.Lbl3.Location = new System.Drawing.Point(6, 48);
            this.Lbl3.Name = "Lbl3";
            this.Lbl3.Size = new System.Drawing.Size(46, 20);
            this.Lbl3.TabIndex = 2;
            this.Lbl3.Text = "File:";
            this.Lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CobxOutFmt
            // 
            this.CobxOutFmt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CobxOutFmt.FormattingEnabled = true;
            this.CobxOutFmt.Items.AddRange(new object[] {
            "TIFF",
            "ENVI"});
            this.CobxOutFmt.Location = new System.Drawing.Point(240, 48);
            this.CobxOutFmt.Name = "CobxOutFmt";
            this.CobxOutFmt.Size = new System.Drawing.Size(114, 20);
            this.CobxOutFmt.TabIndex = 3;
            // 
            // BtBrowse
            // 
            this.BtBrowse.Location = new System.Drawing.Point(294, 17);
            this.BtBrowse.Name = "BtBrowse";
            this.BtBrowse.Size = new System.Drawing.Size(60, 25);
            this.BtBrowse.TabIndex = 4;
            this.BtBrowse.Text = "Browse...";
            this.BtBrowse.UseVisualStyleBackColor = true;
            this.BtBrowse.Click += new System.EventHandler(this.BtBrowse_Click);
            // 
            // GrpOut
            // 
            this.GrpOut.Controls.Add(this.TbxOutName);
            this.GrpOut.Controls.Add(this.TbxOutDir);
            this.GrpOut.Controls.Add(this.Lbl2);
            this.GrpOut.Controls.Add(this.BtBrowse);
            this.GrpOut.Controls.Add(this.Lbl3);
            this.GrpOut.Controls.Add(this.CobxOutFmt);
            this.GrpOut.Location = new System.Drawing.Point(12, 232);
            this.GrpOut.Name = "GrpOut";
            this.GrpOut.Size = new System.Drawing.Size(360, 80);
            this.GrpOut.TabIndex = 6;
            this.GrpOut.TabStop = false;
            this.GrpOut.Text = "Output";
            // 
            // TbxOutName
            // 
            this.TbxOutName.Location = new System.Drawing.Point(58, 48);
            this.TbxOutName.MaxLength = 100;
            this.TbxOutName.Name = "TbxOutName";
            this.TbxOutName.Size = new System.Drawing.Size(176, 21);
            this.TbxOutName.TabIndex = 6;
            // 
            // LsvInput
            // 
            this.LsvInput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhVar,
            this.ColhSource,
            this.ColhBand});
            this.LsvInput.FullRowSelect = true;
            this.LsvInput.Location = new System.Drawing.Point(12, 24);
            this.LsvInput.MultiSelect = false;
            this.LsvInput.Name = "LsvInput";
            this.LsvInput.Size = new System.Drawing.Size(360, 202);
            this.LsvInput.TabIndex = 7;
            this.tooltip.SetToolTip(this.LsvInput, "单击数据项以编辑值");
            this.LsvInput.UseCompatibleStateImageBehavior = false;
            this.LsvInput.View = System.Windows.Forms.View.Details;
            this.LsvInput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LsvInput_MouseUp);
            // 
            // ColhVar
            // 
            this.ColhVar.Text = "Variable";
            this.ColhVar.Width = 120;
            // 
            // ColhSource
            // 
            this.ColhSource.Text = "Data source";
            this.ColhSource.Width = 160;
            // 
            // ColhBand
            // 
            this.ColhBand.Text = "Band";
            this.ColhBand.Width = 65;
            // 
            // Lbl1
            // 
            this.Lbl1.AutoSize = true;
            this.Lbl1.Location = new System.Drawing.Point(16, 9);
            this.Lbl1.Name = "Lbl1";
            this.Lbl1.Size = new System.Drawing.Size(41, 12);
            this.Lbl1.TabIndex = 8;
            this.Lbl1.Text = "Input:";
            // 
            // dlgfolder
            // 
            this.dlgfolder.Description = "Please choose output directory.";
            // 
            // dlgopen
            // 
            this.dlgopen.Filter = "Any File|*.*";
            this.dlgopen.Title = "选择数据源";
            // 
            // BtCancel
            // 
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(292, 318);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 26);
            this.BtCancel.TabIndex = 9;
            this.BtCancel.Text = "Cancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            // 
            // BtOK
            // 
            this.BtOK.Location = new System.Drawing.Point(206, 318);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(80, 26);
            this.BtOK.TabIndex = 10;
            this.BtOK.Text = "Confirm";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // FrmCfgData
            // 
            this.AcceptButton = this.BtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(384, 354);
            this.Controls.Add(this.BtOK);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.Lbl1);
            this.Controls.Add(this.LsvInput);
            this.Controls.Add(this.GrpOut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmCfgData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config Data Source";
            this.Load += new System.EventHandler(this.FrmCfgData_Load);
            this.GrpOut.ResumeLayout(false);
            this.GrpOut.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TbxOutDir;
        private System.Windows.Forms.Label Lbl2;
        private System.Windows.Forms.Label Lbl3;
        private System.Windows.Forms.ComboBox CobxOutFmt;
        private System.Windows.Forms.Button BtBrowse;
        private System.Windows.Forms.GroupBox GrpOut;
        private System.Windows.Forms.Label Lbl1;
        private System.Windows.Forms.ListView LsvInput;
        private System.Windows.Forms.ColumnHeader ColhVar;
        private System.Windows.Forms.ColumnHeader ColhSource;
        private System.Windows.Forms.ColumnHeader ColhBand;
        private System.Windows.Forms.FolderBrowserDialog dlgfolder;
        private System.Windows.Forms.TextBox TbxOutName;
        private System.Windows.Forms.OpenFileDialog dlgopen;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.Button BtOK;
    }
}