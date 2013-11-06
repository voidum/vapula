namespace TCM.Model.Designer
{
    partial class UctNode
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtAdvConfig = new System.Windows.Forms.Button();
            this.LblId = new System.Windows.Forms.Label();
            this.Lbl2 = new System.Windows.Forms.Label();
            this.CobxSDNodes = new System.Windows.Forms.ComboBox();
            this.ChbxSPP = new System.Windows.Forms.CheckBox();
            this.Lbl1 = new System.Windows.Forms.Label();
            this.LsvParamSrc = new TCM.Model.Designer.GroupListView();
            this.LsvParamDst = new TCM.Model.Designer.GroupListView();
            this.ColhDstId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhDstName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhDstType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhSrcId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhSrcName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhSrcType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtAdvConfig);
            this.panel1.Controls.Add(this.LblId);
            this.panel1.Controls.Add(this.Lbl2);
            this.panel1.Controls.Add(this.CobxSDNodes);
            this.panel1.Controls.Add(this.ChbxSPP);
            this.panel1.Controls.Add(this.Lbl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(270, 90);
            this.panel1.TabIndex = 19;
            // 
            // BtAdvConfig
            // 
            this.BtAdvConfig.Location = new System.Drawing.Point(162, 8);
            this.BtAdvConfig.Name = "BtAdvConfig";
            this.BtAdvConfig.Size = new System.Drawing.Size(100, 25);
            this.BtAdvConfig.TabIndex = 17;
            this.BtAdvConfig.Text = "高级配置...";
            this.BtAdvConfig.UseVisualStyleBackColor = true;
            this.BtAdvConfig.Click += new System.EventHandler(this.BtAdvConfig_Click);
            // 
            // LblId
            // 
            this.LblId.AutoSize = true;
            this.LblId.Location = new System.Drawing.Point(55, 14);
            this.LblId.Name = "LblId";
            this.LblId.Size = new System.Drawing.Size(29, 12);
            this.LblId.TabIndex = 16;
            this.LblId.Text = "标识";
            // 
            // Lbl2
            // 
            this.Lbl2.AutoSize = true;
            this.Lbl2.Location = new System.Drawing.Point(12, 39);
            this.Lbl2.Name = "Lbl2";
            this.Lbl2.Size = new System.Drawing.Size(41, 12);
            this.Lbl2.TabIndex = 15;
            this.Lbl2.Text = "依赖：";
            // 
            // CobxSDNodes
            // 
            this.CobxSDNodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CobxSDNodes.FormattingEnabled = true;
            this.CobxSDNodes.Location = new System.Drawing.Point(57, 36);
            this.CobxSDNodes.Name = "CobxSDNodes";
            this.CobxSDNodes.Size = new System.Drawing.Size(205, 20);
            this.CobxSDNodes.TabIndex = 14;
            this.CobxSDNodes.SelectedIndexChanged += new System.EventHandler(this.CobxSDNodes_SelectedIndexChanged);
            // 
            // ChbxSPP
            // 
            this.ChbxSPP.AutoSize = true;
            this.ChbxSPP.Location = new System.Drawing.Point(57, 64);
            this.ChbxSPP.Name = "ChbxSPP";
            this.ChbxSPP.Size = new System.Drawing.Size(132, 16);
            this.ChbxSPP.TabIndex = 13;
            this.ChbxSPP.Text = "要求此节点优先起动";
            this.ChbxSPP.UseVisualStyleBackColor = true;
            this.ChbxSPP.CheckedChanged += new System.EventHandler(this.ChbxSPP_CheckedChanged);
            // 
            // Lbl1
            // 
            this.Lbl1.AutoSize = true;
            this.Lbl1.Location = new System.Drawing.Point(12, 14);
            this.Lbl1.Name = "Lbl1";
            this.Lbl1.Size = new System.Drawing.Size(41, 12);
            this.Lbl1.TabIndex = 12;
            this.Lbl1.Text = "节点：";
            // 
            // LsvParamSrc
            // 
            this.LsvParamSrc.CheckBoxes = true;
            this.LsvParamSrc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhSrcId,
            this.ColhSrcName,
            this.ColhSrcType});
            this.LsvParamSrc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LsvParamSrc.FullRowSelect = true;
            this.LsvParamSrc.Location = new System.Drawing.Point(0, 250);
            this.LsvParamSrc.Name = "LsvParamSrc";
            this.LsvParamSrc.Size = new System.Drawing.Size(270, 150);
            this.LsvParamSrc.TabIndex = 21;
            this.tooltip.SetToolTip(this.LsvParamSrc, "可关联到当前节点的参数");
            this.LsvParamSrc.UseCompatibleStateImageBehavior = false;
            this.LsvParamSrc.View = System.Windows.Forms.View.Details;
            // 
            // LsvParamDst
            // 
            this.LsvParamDst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhDstId,
            this.ColhDstName,
            this.ColhDstType});
            this.LsvParamDst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvParamDst.FullRowSelect = true;
            this.LsvParamDst.Location = new System.Drawing.Point(0, 90);
            this.LsvParamDst.Name = "LsvParamDst";
            this.LsvParamDst.Size = new System.Drawing.Size(270, 160);
            this.LsvParamDst.TabIndex = 22;
            this.tooltip.SetToolTip(this.LsvParamDst, "当前节点的参数");
            this.LsvParamDst.UseCompatibleStateImageBehavior = false;
            this.LsvParamDst.View = System.Windows.Forms.View.Details;
            // 
            // ColhDstId
            // 
            this.ColhDstId.Text = "标识";
            // 
            // ColhDstName
            // 
            this.ColhDstName.Text = "名称";
            this.ColhDstName.Width = 135;
            // 
            // ColhDstType
            // 
            this.ColhDstType.Text = "类型";
            // 
            // ColhSrcId
            // 
            this.ColhSrcId.Text = "标识";
            // 
            // ColhSrcName
            // 
            this.ColhSrcName.Text = "名称";
            this.ColhSrcName.Width = 135;
            // 
            // ColhSrcType
            // 
            this.ColhSrcType.Text = "类型";
            // 
            // UctNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.LsvParamDst);
            this.Controls.Add(this.LsvParamSrc);
            this.Controls.Add(this.panel1);
            this.Name = "UctNode";
            this.Size = new System.Drawing.Size(270, 400);
            this.Resize += new System.EventHandler(this.UctNode_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtAdvConfig;
        private System.Windows.Forms.Label LblId;
        private System.Windows.Forms.Label Lbl2;
        private System.Windows.Forms.ComboBox CobxSDNodes;
        private System.Windows.Forms.CheckBox ChbxSPP;
        private System.Windows.Forms.Label Lbl1;
        private GroupListView LsvParamSrc;
        private GroupListView LsvParamDst;
        private System.Windows.Forms.ColumnHeader ColhDstId;
        private System.Windows.Forms.ColumnHeader ColhDstName;
        private System.Windows.Forms.ColumnHeader ColhDstType;
        private System.Windows.Forms.ColumnHeader ColhSrcId;
        private System.Windows.Forms.ColumnHeader ColhSrcName;
        private System.Windows.Forms.ColumnHeader ColhSrcType;
    }
}
