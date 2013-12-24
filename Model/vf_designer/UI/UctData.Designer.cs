namespace Vapula.Designer
{
    partial class UctData
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
            this.Grp1 = new System.Windows.Forms.GroupBox();
            this.LsvSupply = new System.Windows.Forms.ListView();
            this.ColhSourceId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhSourceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhSourceType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LblSource = new System.Windows.Forms.Label();
            this.LblRefCount = new System.Windows.Forms.Label();
            this.ChbxExport = new System.Windows.Forms.CheckBox();
            this.ColhTargetId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhTargetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhTargetType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LsvParam = new System.Windows.Forms.ListView();
            this.TbxDescription = new System.Windows.Forms.TextBox();
            this.Grp1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grp1
            // 
            this.Grp1.Controls.Add(this.LsvSupply);
            this.Grp1.Controls.Add(this.LblSource);
            this.Grp1.Controls.Add(this.LblRefCount);
            this.Grp1.Controls.Add(this.ChbxExport);
            this.Grp1.Location = new System.Drawing.Point(315, 25);
            this.Grp1.Name = "Grp1";
            this.Grp1.Padding = new System.Windows.Forms.Padding(20, 15, 20, 20);
            this.Grp1.Size = new System.Drawing.Size(260, 350);
            this.Grp1.TabIndex = 25;
            this.Grp1.TabStop = false;
            this.Grp1.Text = "配置";
            // 
            // LsvSupply
            // 
            this.LsvSupply.CheckBoxes = true;
            this.LsvSupply.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhSourceId,
            this.ColhSourceName,
            this.ColhSourceType});
            this.LsvSupply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvSupply.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.LsvSupply.FullRowSelect = true;
            this.LsvSupply.Location = new System.Drawing.Point(20, 105);
            this.LsvSupply.Name = "LsvSupply";
            this.LsvSupply.Size = new System.Drawing.Size(220, 225);
            this.LsvSupply.TabIndex = 34;
            this.LsvSupply.UseCompatibleStateImageBehavior = false;
            this.LsvSupply.View = System.Windows.Forms.View.Details;
            this.LsvSupply.SelectedIndexChanged += new System.EventHandler(this.LsvSupply_SelectedIndexChanged);
            // 
            // ColhSourceId
            // 
            this.ColhSourceId.Text = "标识";
            // 
            // ColhSourceName
            // 
            this.ColhSourceName.Text = "名称";
            // 
            // ColhSourceType
            // 
            this.ColhSourceType.Text = "类型";
            // 
            // LblSource
            // 
            this.LblSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSource.Location = new System.Drawing.Point(20, 75);
            this.LblSource.Name = "LblSource";
            this.LblSource.Size = new System.Drawing.Size(220, 30);
            this.LblSource.TabIndex = 33;
            this.LblSource.Text = "由以下指定参数数据：";
            this.LblSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblRefCount
            // 
            this.LblRefCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblRefCount.Location = new System.Drawing.Point(20, 45);
            this.LblRefCount.Name = "LblRefCount";
            this.LblRefCount.Size = new System.Drawing.Size(220, 30);
            this.LblRefCount.TabIndex = 31;
            this.LblRefCount.Text = "被引用数量：";
            this.LblRefCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChbxExport
            // 
            this.ChbxExport.AutoSize = true;
            this.ChbxExport.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChbxExport.Location = new System.Drawing.Point(20, 29);
            this.ChbxExport.Name = "ChbxExport";
            this.ChbxExport.Size = new System.Drawing.Size(220, 16);
            this.ChbxExport.TabIndex = 28;
            this.ChbxExport.Text = "作为模型参数暴露给最终用户";
            this.ChbxExport.UseVisualStyleBackColor = true;
            this.ChbxExport.CheckedChanged += new System.EventHandler(this.ChbxExport_CheckedChanged);
            // 
            // ColhTargetId
            // 
            this.ColhTargetId.Text = "标识";
            // 
            // ColhTargetName
            // 
            this.ColhTargetName.Text = "名称";
            // 
            // ColhTargetType
            // 
            this.ColhTargetType.Text = "类型";
            // 
            // LsvParam
            // 
            this.LsvParam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhTargetId,
            this.ColhTargetName,
            this.ColhTargetType});
            this.LsvParam.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.LsvParam.FullRowSelect = true;
            this.LsvParam.Location = new System.Drawing.Point(25, 25);
            this.LsvParam.Name = "LsvParam";
            this.LsvParam.Size = new System.Drawing.Size(270, 300);
            this.LsvParam.TabIndex = 22;
            this.LsvParam.UseCompatibleStateImageBehavior = false;
            this.LsvParam.View = System.Windows.Forms.View.Details;
            this.LsvParam.SelectedIndexChanged += new System.EventHandler(this.LsvParam_SelectedIndexChanged);
            // 
            // TbxDescription
            // 
            this.TbxDescription.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TbxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbxDescription.Location = new System.Drawing.Point(25, 325);
            this.TbxDescription.Multiline = true;
            this.TbxDescription.Name = "TbxDescription";
            this.TbxDescription.Size = new System.Drawing.Size(270, 50);
            this.TbxDescription.TabIndex = 26;
            // 
            // UctData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TbxDescription);
            this.Controls.Add(this.Grp1);
            this.Controls.Add(this.LsvParam);
            this.Name = "UctData";
            this.Size = new System.Drawing.Size(600, 400);
            this.Grp1.ResumeLayout(false);
            this.Grp1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Grp1;
        private System.Windows.Forms.CheckBox ChbxExport;
        private System.Windows.Forms.ColumnHeader ColhTargetId;
        private System.Windows.Forms.ColumnHeader ColhTargetName;
        private System.Windows.Forms.ColumnHeader ColhTargetType;
        private System.Windows.Forms.ListView LsvParam;
        private System.Windows.Forms.TextBox TbxDescription;
        private System.Windows.Forms.Label LblRefCount;
        private System.Windows.Forms.ListView LsvSupply;
        private System.Windows.Forms.ColumnHeader ColhSourceId;
        private System.Windows.Forms.ColumnHeader ColhSourceName;
        private System.Windows.Forms.ColumnHeader ColhSourceType;
        private System.Windows.Forms.Label LblSource;





    }
}
