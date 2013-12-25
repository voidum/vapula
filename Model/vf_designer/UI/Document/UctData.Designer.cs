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
            this.Grp2 = new System.Windows.Forms.GroupBox();
            this.ChbxExport = new System.Windows.Forms.CheckBox();
            this.TbxDescription = new System.Windows.Forms.TextBox();
            this.Grp1 = new System.Windows.Forms.GroupBox();
            this.LsvTarget = new System.Windows.Forms.ListView();
            this.ColhTargetId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhTargetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhTargetType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChbxOptional = new System.Windows.Forms.CheckBox();
            this.LblSource = new System.Windows.Forms.Label();
            this.LsvSupply = new System.Windows.Forms.ListView();
            this.ColhSupplyId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhSupplyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhSupplyType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Grp2.SuspendLayout();
            this.Grp1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grp2
            // 
            this.Grp2.Controls.Add(this.LsvSupply);
            this.Grp2.Controls.Add(this.LblSource);
            this.Grp2.Controls.Add(this.ChbxOptional);
            this.Grp2.Controls.Add(this.ChbxExport);
            this.Grp2.Location = new System.Drawing.Point(310, 25);
            this.Grp2.Name = "Grp2";
            this.Grp2.Padding = new System.Windows.Forms.Padding(20, 15, 20, 20);
            this.Grp2.Size = new System.Drawing.Size(265, 350);
            this.Grp2.TabIndex = 25;
            this.Grp2.TabStop = false;
            this.Grp2.Text = "配置";
            // 
            // ChbxExport
            // 
            this.ChbxExport.Checked = true;
            this.ChbxExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChbxExport.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChbxExport.Location = new System.Drawing.Point(20, 29);
            this.ChbxExport.Name = "ChbxExport";
            this.ChbxExport.Size = new System.Drawing.Size(225, 30);
            this.ChbxExport.TabIndex = 28;
            this.ChbxExport.Text = "导出为模型参数，要求用户输入";
            this.ChbxExport.UseVisualStyleBackColor = true;
            this.ChbxExport.CheckedChanged += new System.EventHandler(this.ChbxExport_CheckedChanged);
            // 
            // TbxDescription
            // 
            this.TbxDescription.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TbxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbxDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TbxDescription.Location = new System.Drawing.Point(20, 270);
            this.TbxDescription.Multiline = true;
            this.TbxDescription.Name = "TbxDescription";
            this.TbxDescription.Size = new System.Drawing.Size(225, 60);
            this.TbxDescription.TabIndex = 26;
            // 
            // Grp1
            // 
            this.Grp1.Controls.Add(this.LsvTarget);
            this.Grp1.Controls.Add(this.TbxDescription);
            this.Grp1.Location = new System.Drawing.Point(25, 25);
            this.Grp1.Name = "Grp1";
            this.Grp1.Padding = new System.Windows.Forms.Padding(20, 15, 20, 20);
            this.Grp1.Size = new System.Drawing.Size(265, 350);
            this.Grp1.TabIndex = 27;
            this.Grp1.TabStop = false;
            this.Grp1.Text = "所有参数";
            // 
            // LsvTarget
            // 
            this.LsvTarget.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhTargetId,
            this.ColhTargetName,
            this.ColhTargetType});
            this.LsvTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvTarget.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.LsvTarget.FullRowSelect = true;
            this.LsvTarget.HideSelection = false;
            this.LsvTarget.Location = new System.Drawing.Point(20, 29);
            this.LsvTarget.Name = "LsvTarget";
            this.LsvTarget.Size = new System.Drawing.Size(225, 241);
            this.LsvTarget.TabIndex = 28;
            this.LsvTarget.UseCompatibleStateImageBehavior = false;
            this.LsvTarget.View = System.Windows.Forms.View.Details;
            this.LsvTarget.SelectedIndexChanged += new System.EventHandler(this.LsvTarget_SelectedIndexChanged);
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
            // ChbxOptional
            // 
            this.ChbxOptional.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChbxOptional.Location = new System.Drawing.Point(20, 59);
            this.ChbxOptional.Name = "ChbxOptional";
            this.ChbxOptional.Size = new System.Drawing.Size(225, 30);
            this.ChbxOptional.TabIndex = 39;
            this.ChbxOptional.Text = "标记为可选参数";
            this.ChbxOptional.UseVisualStyleBackColor = true;
            this.ChbxOptional.CheckedChanged += new System.EventHandler(this.ChbxOptional_CheckedChanged);
            // 
            // LblSource
            // 
            this.LblSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSource.Location = new System.Drawing.Point(20, 89);
            this.LblSource.Name = "LblSource";
            this.LblSource.Size = new System.Drawing.Size(225, 30);
            this.LblSource.TabIndex = 41;
            this.LblSource.Text = "映射以下参数：";
            this.LblSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LsvSupply
            // 
            this.LsvSupply.CheckBoxes = true;
            this.LsvSupply.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhSupplyId,
            this.ColhSupplyName,
            this.ColhSupplyType});
            this.LsvSupply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvSupply.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.LsvSupply.FullRowSelect = true;
            this.LsvSupply.Location = new System.Drawing.Point(20, 119);
            this.LsvSupply.Name = "LsvSupply";
            this.LsvSupply.Size = new System.Drawing.Size(225, 211);
            this.LsvSupply.TabIndex = 42;
            this.LsvSupply.UseCompatibleStateImageBehavior = false;
            this.LsvSupply.View = System.Windows.Forms.View.Details;
            this.LsvSupply.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LsvSupply_ItemChecked);
            // 
            // ColhSupplyId
            // 
            this.ColhSupplyId.Text = "标识";
            // 
            // ColhSupplyName
            // 
            this.ColhSupplyName.Text = "名称";
            // 
            // ColhSupplyType
            // 
            this.ColhSupplyType.Text = "类型";
            // 
            // UctData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Grp1);
            this.Controls.Add(this.Grp2);
            this.Name = "UctData";
            this.Size = new System.Drawing.Size(600, 400);
            this.Grp2.ResumeLayout(false);
            this.Grp1.ResumeLayout(false);
            this.Grp1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grp2;
        private System.Windows.Forms.CheckBox ChbxExport;
        private System.Windows.Forms.TextBox TbxDescription;
        private System.Windows.Forms.GroupBox Grp1;
        private System.Windows.Forms.ListView LsvTarget;
        private System.Windows.Forms.ColumnHeader ColhTargetId;
        private System.Windows.Forms.ColumnHeader ColhTargetName;
        private System.Windows.Forms.ColumnHeader ColhTargetType;
        private System.Windows.Forms.ListView LsvSupply;
        private System.Windows.Forms.ColumnHeader ColhSupplyId;
        private System.Windows.Forms.ColumnHeader ColhSupplyName;
        private System.Windows.Forms.ColumnHeader ColhSupplyType;
        private System.Windows.Forms.Label LblSource;
        private System.Windows.Forms.CheckBox ChbxOptional;





    }
}
