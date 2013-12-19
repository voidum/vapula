namespace Vapula.Toolkit.TaskHelper
{
    partial class FrmTags
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
            this.LsvTags = new System.Windows.Forms.ListView();
            this.BtAdd = new System.Windows.Forms.Button();
            this.BtRemove = new System.Windows.Forms.Button();
            this.BtRemoveAll = new System.Windows.Forms.Button();
            this.ColhKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // LsvTags
            // 
            this.LsvTags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhKey,
            this.ColhValue});
            this.LsvTags.FullRowSelect = true;
            this.LsvTags.GridLines = true;
            this.LsvTags.Location = new System.Drawing.Point(12, 12);
            this.LsvTags.Name = "LsvTags";
            this.LsvTags.Size = new System.Drawing.Size(320, 284);
            this.LsvTags.TabIndex = 1;
            this.LsvTags.UseCompatibleStateImageBehavior = false;
            this.LsvTags.View = System.Windows.Forms.View.Details;
            // 
            // BtAdd
            // 
            this.BtAdd.Location = new System.Drawing.Point(12, 302);
            this.BtAdd.Name = "BtAdd";
            this.BtAdd.Size = new System.Drawing.Size(80, 25);
            this.BtAdd.TabIndex = 2;
            this.BtAdd.Text = "添加";
            this.BtAdd.UseVisualStyleBackColor = true;
            // 
            // BtRemove
            // 
            this.BtRemove.Location = new System.Drawing.Point(98, 302);
            this.BtRemove.Name = "BtRemove";
            this.BtRemove.Size = new System.Drawing.Size(80, 25);
            this.BtRemove.TabIndex = 3;
            this.BtRemove.Text = "移除";
            this.BtRemove.UseVisualStyleBackColor = true;
            // 
            // BtRemoveAll
            // 
            this.BtRemoveAll.Location = new System.Drawing.Point(252, 302);
            this.BtRemoveAll.Name = "BtRemoveAll";
            this.BtRemoveAll.Size = new System.Drawing.Size(80, 25);
            this.BtRemoveAll.TabIndex = 4;
            this.BtRemoveAll.Text = "移除全部";
            this.BtRemoveAll.UseVisualStyleBackColor = true;
            // 
            // ColhKey
            // 
            this.ColhKey.Text = "键";
            this.ColhKey.Width = 100;
            // 
            // ColhValue
            // 
            this.ColhValue.Text = "值";
            this.ColhValue.Width = 200;
            // 
            // FrmTags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 339);
            this.Controls.Add(this.BtRemoveAll);
            this.Controls.Add(this.BtRemove);
            this.Controls.Add(this.BtAdd);
            this.Controls.Add(this.LsvTags);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmTags";
            this.Text = "附加标签...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LsvTags;
        private System.Windows.Forms.Button BtAdd;
        private System.Windows.Forms.Button BtRemove;
        private System.Windows.Forms.Button BtRemoveAll;
        private System.Windows.Forms.ColumnHeader ColhKey;
        private System.Windows.Forms.ColumnHeader ColhValue;
    }
}