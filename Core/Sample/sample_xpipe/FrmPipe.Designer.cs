namespace sample_xpipe
{
    partial class FrmPipe
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
            this.ChbxAsServer = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TbxPipeId = new System.Windows.Forms.TextBox();
            this.BtStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChbxAsServer
            // 
            this.ChbxAsServer.AutoSize = true;
            this.ChbxAsServer.Location = new System.Drawing.Point(12, 12);
            this.ChbxAsServer.Name = "ChbxAsServer";
            this.ChbxAsServer.Size = new System.Drawing.Size(78, 16);
            this.ChbxAsServer.TabIndex = 0;
            this.ChbxAsServer.Text = "As Server";
            this.ChbxAsServer.UseVisualStyleBackColor = true;
            this.ChbxAsServer.CheckedChanged += new System.EventHandler(this.ChbxAsServer_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server ID:";
            // 
            // TbxPipeId
            // 
            this.TbxPipeId.Location = new System.Drawing.Point(83, 36);
            this.TbxPipeId.Name = "TbxPipeId";
            this.TbxPipeId.Size = new System.Drawing.Size(249, 21);
            this.TbxPipeId.TabIndex = 2;
            // 
            // BtStart
            // 
            this.BtStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BtStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtStart.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BtStart.Location = new System.Drawing.Point(12, 63);
            this.BtStart.Name = "BtStart";
            this.BtStart.Size = new System.Drawing.Size(320, 39);
            this.BtStart.TabIndex = 3;
            this.BtStart.Text = "START";
            this.BtStart.UseVisualStyleBackColor = true;
            this.BtStart.Click += new System.EventHandler(this.BtStart_Click);
            // 
            // FrmPipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 114);
            this.Controls.Add(this.BtStart);
            this.Controls.Add(this.TbxPipeId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChbxAsServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmPipe";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pipe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ChbxAsServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbxPipeId;
        private System.Windows.Forms.Button BtStart;

    }
}