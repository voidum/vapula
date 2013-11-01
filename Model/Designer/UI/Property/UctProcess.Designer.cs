namespace TCM.Model.Designer
{
    partial class UctProcess
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
            this.components = new System.ComponentModel.Container();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.panel = new System.Windows.Forms.Panel();
            this.LblId = new System.Windows.Forms.Label();
            this.Lbl2 = new System.Windows.Forms.Label();
            this.CobxSDNodes = new System.Windows.Forms.ComboBox();
            this.ChbxSPP = new System.Windows.Forms.CheckBox();
            this.Lbl1 = new System.Windows.Forms.Label();
            this.LsvParam = new TCM.Model.Designer.GroupListView();
            this.ColhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.LblId);
            this.panel.Controls.Add(this.Lbl2);
            this.panel.Controls.Add(this.CobxSDNodes);
            this.panel.Controls.Add(this.ChbxSPP);
            this.panel.Controls.Add(this.Lbl1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(300, 85);
            this.panel.TabIndex = 0;
            // 
            // LblId
            // 
            this.LblId.AutoSize = true;
            this.LblId.Location = new System.Drawing.Point(55, 10);
            this.LblId.Name = "LblId";
            this.LblId.Size = new System.Drawing.Size(0, 12);
            this.LblId.TabIndex = 4;
            // 
            // Lbl2
            // 
            this.Lbl2.AutoSize = true;
            this.Lbl2.Location = new System.Drawing.Point(10, 58);
            this.Lbl2.Name = "Lbl2";
            this.Lbl2.Size = new System.Drawing.Size(77, 12);
            this.Lbl2.TabIndex = 3;
            this.Lbl2.Text = "强依赖节点：";
            // 
            // CobxSDNodes
            // 
            this.CobxSDNodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CobxSDNodes.FormattingEnabled = true;
            this.CobxSDNodes.Location = new System.Drawing.Point(93, 54);
            this.CobxSDNodes.Name = "CobxSDNodes";
            this.CobxSDNodes.Size = new System.Drawing.Size(180, 20);
            this.CobxSDNodes.TabIndex = 2;
            this.CobxSDNodes.SelectedIndexChanged += new System.EventHandler(this.CobxSDNodes_SelectedIndexChanged);
            // 
            // ChbxSPP
            // 
            this.ChbxSPP.AutoSize = true;
            this.ChbxSPP.Location = new System.Drawing.Point(12, 32);
            this.ChbxSPP.Name = "ChbxSPP";
            this.ChbxSPP.Size = new System.Drawing.Size(156, 16);
            this.ChbxSPP.TabIndex = 1;
            this.ChbxSPP.Text = "启用此节点的起动优先权";
            this.ChbxSPP.UseVisualStyleBackColor = true;
            this.ChbxSPP.CheckedChanged += new System.EventHandler(this.ChbxSPP_CheckedChanged);
            // 
            // Lbl1
            // 
            this.Lbl1.AutoSize = true;
            this.Lbl1.Location = new System.Drawing.Point(10, 10);
            this.Lbl1.Name = "Lbl1";
            this.Lbl1.Size = new System.Drawing.Size(41, 12);
            this.Lbl1.TabIndex = 0;
            this.Lbl1.Text = "标识：";
            // 
            // LsvParam
            // 
            this.LsvParam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhName,
            this.ColhType,
            this.ColhValue});
            this.LsvParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvParam.FullRowSelect = true;
            this.LsvParam.Location = new System.Drawing.Point(0, 85);
            this.LsvParam.Name = "LsvParam";
            this.LsvParam.Size = new System.Drawing.Size(300, 215);
            this.LsvParam.TabIndex = 3;
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
            // UctProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.LsvParam);
            this.Controls.Add(this.panel);
            this.Name = "UctProcess";
            this.Size = new System.Drawing.Size(300, 300);
            this.Enter += new System.EventHandler(this.UctProcess_Enter);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Panel panel;
        private GroupListView LsvParam;
        private System.Windows.Forms.ColumnHeader ColhName;
        private System.Windows.Forms.ColumnHeader ColhType;
        private System.Windows.Forms.ColumnHeader ColhValue;
        private System.Windows.Forms.Label Lbl2;
        private System.Windows.Forms.ComboBox CobxSDNodes;
        private System.Windows.Forms.CheckBox ChbxSPP;
        private System.Windows.Forms.Label Lbl1;
        private System.Windows.Forms.Label LblId;
    }
}
