namespace TCM.ComPublisher
{
    partial class FrmSetPublish
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
            this.BtBrowseBin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TbxBin = new System.Windows.Forms.TextBox();
            this.GrpTarget = new System.Windows.Forms.GroupBox();
            this.BtBrowseRes = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TbxRes = new System.Windows.Forms.TextBox();
            this.TbxLst = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtBrowseLst = new System.Windows.Forms.Button();
            this.BtBrowseCfg = new System.Windows.Forms.Button();
            this.TbxCfg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtApply = new System.Windows.Forms.Button();
            this.ChbxAutoOverwrite = new System.Windows.Forms.CheckBox();
            this.DlgFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.GrpTarget.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtBrowseBin
            // 
            this.BtBrowseBin.Location = new System.Drawing.Point(360, 19);
            this.BtBrowseBin.Name = "BtBrowseBin";
            this.BtBrowseBin.Size = new System.Drawing.Size(36, 24);
            this.BtBrowseBin.TabIndex = 0;
            this.BtBrowseBin.Tag = "bin";
            this.BtBrowseBin.Text = "...";
            this.BtBrowseBin.UseVisualStyleBackColor = true;
            this.BtBrowseBin.Click += new System.EventHandler(this.BtBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "二进制文件：";
            // 
            // TbxBin
            // 
            this.TbxBin.BackColor = System.Drawing.Color.White;
            this.TbxBin.Location = new System.Drawing.Point(104, 20);
            this.TbxBin.Name = "TbxBin";
            this.TbxBin.ReadOnly = true;
            this.TbxBin.Size = new System.Drawing.Size(250, 21);
            this.TbxBin.TabIndex = 2;
            // 
            // GrpTarget
            // 
            this.GrpTarget.Controls.Add(this.BtBrowseRes);
            this.GrpTarget.Controls.Add(this.label4);
            this.GrpTarget.Controls.Add(this.TbxRes);
            this.GrpTarget.Controls.Add(this.TbxLst);
            this.GrpTarget.Controls.Add(this.label3);
            this.GrpTarget.Controls.Add(this.BtBrowseLst);
            this.GrpTarget.Controls.Add(this.BtBrowseCfg);
            this.GrpTarget.Controls.Add(this.TbxCfg);
            this.GrpTarget.Controls.Add(this.label2);
            this.GrpTarget.Controls.Add(this.label1);
            this.GrpTarget.Controls.Add(this.BtBrowseBin);
            this.GrpTarget.Controls.Add(this.TbxBin);
            this.GrpTarget.Location = new System.Drawing.Point(12, 12);
            this.GrpTarget.Name = "GrpTarget";
            this.GrpTarget.Size = new System.Drawing.Size(405, 135);
            this.GrpTarget.TabIndex = 3;
            this.GrpTarget.TabStop = false;
            this.GrpTarget.Text = "目标位置";
            // 
            // BtBrowseRes
            // 
            this.BtBrowseRes.Location = new System.Drawing.Point(360, 99);
            this.BtBrowseRes.Name = "BtBrowseRes";
            this.BtBrowseRes.Size = new System.Drawing.Size(36, 24);
            this.BtBrowseRes.TabIndex = 11;
            this.BtBrowseRes.Tag = "res";
            this.BtBrowseRes.Text = "...";
            this.BtBrowseRes.UseVisualStyleBackColor = true;
            this.BtBrowseRes.Click += new System.EventHandler(this.BtBrowse_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "资源文件：";
            // 
            // TbxRes
            // 
            this.TbxRes.BackColor = System.Drawing.Color.White;
            this.TbxRes.Location = new System.Drawing.Point(104, 101);
            this.TbxRes.Name = "TbxRes";
            this.TbxRes.ReadOnly = true;
            this.TbxRes.Size = new System.Drawing.Size(250, 21);
            this.TbxRes.TabIndex = 9;
            // 
            // TbxLst
            // 
            this.TbxLst.BackColor = System.Drawing.Color.White;
            this.TbxLst.Location = new System.Drawing.Point(104, 74);
            this.TbxLst.Name = "TbxLst";
            this.TbxLst.ReadOnly = true;
            this.TbxLst.Size = new System.Drawing.Size(250, 21);
            this.TbxLst.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "组件清单：";
            // 
            // BtBrowseLst
            // 
            this.BtBrowseLst.Location = new System.Drawing.Point(360, 72);
            this.BtBrowseLst.Name = "BtBrowseLst";
            this.BtBrowseLst.Size = new System.Drawing.Size(36, 24);
            this.BtBrowseLst.TabIndex = 6;
            this.BtBrowseLst.Tag = "lst";
            this.BtBrowseLst.Text = "...";
            this.BtBrowseLst.UseVisualStyleBackColor = true;
            this.BtBrowseLst.Click += new System.EventHandler(this.BtBrowse_Click);
            // 
            // BtBrowseCfg
            // 
            this.BtBrowseCfg.Location = new System.Drawing.Point(360, 46);
            this.BtBrowseCfg.Name = "BtBrowseCfg";
            this.BtBrowseCfg.Size = new System.Drawing.Size(36, 24);
            this.BtBrowseCfg.TabIndex = 5;
            this.BtBrowseCfg.Tag = "cfg";
            this.BtBrowseCfg.Text = "...";
            this.BtBrowseCfg.UseVisualStyleBackColor = true;
            this.BtBrowseCfg.Click += new System.EventHandler(this.BtBrowse_Click);
            // 
            // TbxCfg
            // 
            this.TbxCfg.BackColor = System.Drawing.Color.White;
            this.TbxCfg.Location = new System.Drawing.Point(104, 47);
            this.TbxCfg.Name = "TbxCfg";
            this.TbxCfg.ReadOnly = true;
            this.TbxCfg.Size = new System.Drawing.Size(250, 21);
            this.TbxCfg.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "组件配置文件：";
            // 
            // BtApply
            // 
            this.BtApply.Location = new System.Drawing.Point(297, 153);
            this.BtApply.Name = "BtApply";
            this.BtApply.Size = new System.Drawing.Size(120, 30);
            this.BtApply.TabIndex = 4;
            this.BtApply.Text = "应用";
            this.BtApply.UseVisualStyleBackColor = true;
            this.BtApply.Click += new System.EventHandler(this.BtApply_Click);
            // 
            // ChbxAutoOverwrite
            // 
            this.ChbxAutoOverwrite.AutoSize = true;
            this.ChbxAutoOverwrite.Location = new System.Drawing.Point(12, 161);
            this.ChbxAutoOverwrite.Name = "ChbxAutoOverwrite";
            this.ChbxAutoOverwrite.Size = new System.Drawing.Size(102, 16);
            this.ChbxAutoOverwrite.TabIndex = 5;
            this.ChbxAutoOverwrite.Text = "自动覆盖/跳过";
            this.ChbxAutoOverwrite.UseVisualStyleBackColor = true;
            // 
            // FrmSetPublish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 199);
            this.Controls.Add(this.ChbxAutoOverwrite);
            this.Controls.Add(this.BtApply);
            this.Controls.Add(this.GrpTarget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(445, 235);
            this.MinimumSize = new System.Drawing.Size(445, 235);
            this.Name = "FrmSetPublish";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发布设置";
            this.GrpTarget.ResumeLayout(false);
            this.GrpTarget.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtBrowseBin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbxBin;
        private System.Windows.Forms.GroupBox GrpTarget;
        private System.Windows.Forms.TextBox TbxLst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtBrowseLst;
        private System.Windows.Forms.Button BtBrowseCfg;
        private System.Windows.Forms.TextBox TbxCfg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtBrowseRes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TbxRes;
        private System.Windows.Forms.Button BtApply;
        private System.Windows.Forms.CheckBox ChbxAutoOverwrite;
        private System.Windows.Forms.FolderBrowserDialog DlgFolder;
    }
}