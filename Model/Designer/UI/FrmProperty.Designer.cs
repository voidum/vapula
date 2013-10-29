namespace TCM.Model.Designer
{
    partial class FrmProperty
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
            this.LsvParam = new TCM.Model.Designer.GroupListView();
            this.ColhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // LsvParam
            // 
            this.LsvParam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhName,
            this.ColhType,
            this.ColhValue});
            this.LsvParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvParam.FullRowSelect = true;
            this.LsvParam.Location = new System.Drawing.Point(0, 0);
            this.LsvParam.Name = "LsvParam";
            this.LsvParam.Size = new System.Drawing.Size(284, 262);
            this.LsvParam.TabIndex = 0;
            this.LsvParam.UseCompatibleStateImageBehavior = false;
            this.LsvParam.View = System.Windows.Forms.View.Details;
            // 
            // ColhName
            // 
            this.ColhName.Text = "名称";
            this.ColhName.Width = 80;
            // 
            // ColhType
            // 
            this.ColhType.Text = "类型";
            // 
            // ColhValue
            // 
            this.ColhValue.Text = "值";
            this.ColhValue.Width = 135;
            // 
            // FrmProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.LsvParam);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmProperty";
            this.Text = "参数";
            this.ResumeLayout(false);

        }

        #endregion

        private GroupListView LsvParam;
        private System.Windows.Forms.ColumnHeader ColhName;
        private System.Windows.Forms.ColumnHeader ColhType;
        private System.Windows.Forms.ColumnHeader ColhValue;





    }
}