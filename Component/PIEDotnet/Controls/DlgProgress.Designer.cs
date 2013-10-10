namespace PIE.Controls
{
    partial class DlgProgress
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
            this.LblDescription = new System.Windows.Forms.Label();
            this.progbar = new System.Windows.Forms.ProgressBar();
            this.BtRunning = new System.Windows.Forms.Button();
            this.LblProgValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblDescription
            // 
            this.LblDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblDescription.Location = new System.Drawing.Point(12, 9);
            this.LblDescription.Name = "LblDescription";
            this.LblDescription.Size = new System.Drawing.Size(296, 70);
            this.LblDescription.TabIndex = 1;
            // 
            // progbar
            // 
            this.progbar.Location = new System.Drawing.Point(88, 84);
            this.progbar.Name = "progbar";
            this.progbar.Size = new System.Drawing.Size(160, 18);
            this.progbar.TabIndex = 2;
            // 
            // BtRunning
            // 
            this.BtRunning.Location = new System.Drawing.Point(12, 82);
            this.BtRunning.Name = "BtRunning";
            this.BtRunning.Size = new System.Drawing.Size(70, 22);
            this.BtRunning.TabIndex = 3;
            this.BtRunning.Text = "暂停/恢复";
            this.BtRunning.UseVisualStyleBackColor = true;
            this.BtRunning.Click += new System.EventHandler(this.BtRunning_Click);
            // 
            // LblProgValue
            // 
            this.LblProgValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblProgValue.Location = new System.Drawing.Point(254, 84);
            this.LblProgValue.Name = "LblProgValue";
            this.LblProgValue.Size = new System.Drawing.Size(54, 20);
            this.LblProgValue.TabIndex = 4;
            this.LblProgValue.Text = "100.00%";
            this.LblProgValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 112);
            this.Controls.Add(this.LblProgValue);
            this.Controls.Add(this.BtRunning);
            this.Controls.Add(this.progbar);
            this.Controls.Add(this.LblDescription);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(336, 150);
            this.MinimumSize = new System.Drawing.Size(336, 150);
            this.Name = "FrmProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "任务进度";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProgress_FormClosing);
            this.Shown += new System.EventHandler(this.FrmProgress_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblDescription;
        private System.Windows.Forms.ProgressBar progbar;
        private System.Windows.Forms.Button BtRunning;
        private System.Windows.Forms.Label LblProgValue;



    }
}