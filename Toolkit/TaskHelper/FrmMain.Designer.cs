namespace Vapula.Toolkit.TaskHelper
{
    partial class FrmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.progbar = new System.Windows.Forms.ProgressBar();
            this.LsvParam = new System.Windows.Forms.ListView();
            this.ColhId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhParamValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.BtBrowse = new System.Windows.Forms.Button();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.split1 = new System.Windows.Forms.ToolStripSeparator();
            this.GrpExtension = new System.Windows.Forms.GroupBox();
            this.BtConfigXproc = new System.Windows.Forms.Button();
            this.BtAttachTags = new System.Windows.Forms.Button();
            this.ChbxEnableXhost = new System.Windows.Forms.CheckBox();
            this.CobxXproc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CobxFuncId = new System.Windows.Forms.ComboBox();
            this.LblFuncName = new System.Windows.Forms.Label();
            this.LblCom = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.GrpBasic = new System.Windows.Forms.GroupBox();
            this.GrpProcess = new System.Windows.Forms.GroupBox();
            this.split2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtNew = new System.Windows.Forms.ToolStripButton();
            this.BtLoad = new System.Windows.Forms.ToolStripButton();
            this.BtSave = new System.Windows.Forms.ToolStripButton();
            this.BtStart = new System.Windows.Forms.ToolStripButton();
            this.BtStop = new System.Windows.Forms.ToolStripButton();
            this.BtConfig = new System.Windows.Forms.ToolStripButton();
            this.dlgopen = new System.Windows.Forms.OpenFileDialog();
            this.toolbar.SuspendLayout();
            this.GrpExtension.SuspendLayout();
            this.GrpBasic.SuspendLayout();
            this.GrpProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // progbar
            // 
            this.progbar.Location = new System.Drawing.Point(15, 22);
            this.progbar.Name = "progbar";
            this.progbar.Size = new System.Drawing.Size(400, 18);
            this.progbar.TabIndex = 0;
            // 
            // LsvParam
            // 
            this.LsvParam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhId,
            this.ColhName,
            this.ColhType,
            this.ColhParamValue});
            this.LsvParam.FullRowSelect = true;
            this.LsvParam.GridLines = true;
            this.LsvParam.Location = new System.Drawing.Point(15, 95);
            this.LsvParam.Name = "LsvParam";
            this.LsvParam.Size = new System.Drawing.Size(401, 202);
            this.LsvParam.TabIndex = 1;
            this.LsvParam.UseCompatibleStateImageBehavior = false;
            this.LsvParam.View = System.Windows.Forms.View.Details;
            // 
            // ColhId
            // 
            this.ColhId.Text = "索引";
            this.ColhId.Width = 45;
            // 
            // ColhName
            // 
            this.ColhName.Text = "名称";
            this.ColhName.Width = 90;
            // 
            // ColhType
            // 
            this.ColhType.Text = "类型";
            this.ColhType.Width = 90;
            // 
            // ColhParamValue
            // 
            this.ColhParamValue.Text = "值";
            this.ColhParamValue.Width = 170;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "组件：";
            // 
            // BtBrowse
            // 
            this.BtBrowse.Location = new System.Drawing.Point(346, 18);
            this.BtBrowse.Name = "BtBrowse";
            this.BtBrowse.Size = new System.Drawing.Size(70, 23);
            this.BtBrowse.TabIndex = 4;
            this.BtBrowse.Text = "浏览...";
            this.BtBrowse.UseVisualStyleBackColor = true;
            this.BtBrowse.Click += new System.EventHandler(this.BtBrowse_Click);
            // 
            // toolbar
            // 
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtNew,
            this.BtLoad,
            this.BtSave,
            this.split1,
            this.BtStart,
            this.BtStop,
            this.split2,
            this.BtConfig});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(454, 27);
            this.toolbar.TabIndex = 5;
            // 
            // split1
            // 
            this.split1.Name = "split1";
            this.split1.Size = new System.Drawing.Size(6, 27);
            // 
            // GrpExtension
            // 
            this.GrpExtension.Controls.Add(this.BtConfigXproc);
            this.GrpExtension.Controls.Add(this.BtAttachTags);
            this.GrpExtension.Controls.Add(this.ChbxEnableXhost);
            this.GrpExtension.Controls.Add(this.CobxXproc);
            this.GrpExtension.Controls.Add(this.label2);
            this.GrpExtension.Location = new System.Drawing.Point(12, 349);
            this.GrpExtension.Name = "GrpExtension";
            this.GrpExtension.Size = new System.Drawing.Size(430, 80);
            this.GrpExtension.TabIndex = 6;
            this.GrpExtension.TabStop = false;
            this.GrpExtension.Text = "扩展";
            // 
            // BtConfigXproc
            // 
            this.BtConfigXproc.Location = new System.Drawing.Point(296, 18);
            this.BtConfigXproc.Name = "BtConfigXproc";
            this.BtConfigXproc.Size = new System.Drawing.Size(120, 23);
            this.BtConfigXproc.TabIndex = 7;
            this.BtConfigXproc.Text = "配置通信...";
            this.BtConfigXproc.UseVisualStyleBackColor = true;
            // 
            // BtAttachTags
            // 
            this.BtAttachTags.Location = new System.Drawing.Point(296, 45);
            this.BtAttachTags.Name = "BtAttachTags";
            this.BtAttachTags.Size = new System.Drawing.Size(120, 23);
            this.BtAttachTags.TabIndex = 6;
            this.BtAttachTags.Text = "附加信息...";
            this.BtAttachTags.UseVisualStyleBackColor = true;
            // 
            // ChbxEnableXhost
            // 
            this.ChbxEnableXhost.AutoSize = true;
            this.ChbxEnableXhost.Location = new System.Drawing.Point(60, 52);
            this.ChbxEnableXhost.Name = "ChbxEnableXhost";
            this.ChbxEnableXhost.Size = new System.Drawing.Size(150, 16);
            this.ChbxEnableXhost.TabIndex = 5;
            this.ChbxEnableXhost.Text = "启用图形界面（xHost）";
            this.ChbxEnableXhost.UseVisualStyleBackColor = true;
            // 
            // CobxXproc
            // 
            this.CobxXproc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CobxXproc.FormattingEnabled = true;
            this.CobxXproc.Items.AddRange(new object[] {
            "NULL 不进行任何通信",
            "MMF 基于MMF直接通信",
            "UDF 通过脚本间接通信"});
            this.CobxXproc.Location = new System.Drawing.Point(60, 20);
            this.CobxXproc.Name = "CobxXproc";
            this.CobxXproc.Size = new System.Drawing.Size(230, 20);
            this.CobxXproc.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "通信：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "功能：";
            // 
            // CobxFuncId
            // 
            this.CobxFuncId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CobxFuncId.FormattingEnabled = true;
            this.CobxFuncId.Location = new System.Drawing.Point(53, 49);
            this.CobxFuncId.Name = "CobxFuncId";
            this.CobxFuncId.Size = new System.Drawing.Size(100, 20);
            this.CobxFuncId.TabIndex = 9;
            this.CobxFuncId.SelectedIndexChanged += new System.EventHandler(this.CobxFuncId_SelectedIndexChanged);
            // 
            // LblFuncName
            // 
            this.LblFuncName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblFuncName.Location = new System.Drawing.Point(159, 49);
            this.LblFuncName.Name = "LblFuncName";
            this.LblFuncName.Size = new System.Drawing.Size(257, 20);
            this.LblFuncName.TabIndex = 10;
            // 
            // LblCom
            // 
            this.LblCom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblCom.Location = new System.Drawing.Point(53, 20);
            this.LblCom.Name = "LblCom";
            this.LblCom.Size = new System.Drawing.Size(287, 20);
            this.LblCom.TabIndex = 11;
            this.LblCom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "参数：";
            // 
            // GrpBasic
            // 
            this.GrpBasic.Controls.Add(this.LsvParam);
            this.GrpBasic.Controls.Add(this.LblCom);
            this.GrpBasic.Controls.Add(this.label5);
            this.GrpBasic.Controls.Add(this.BtBrowse);
            this.GrpBasic.Controls.Add(this.CobxFuncId);
            this.GrpBasic.Controls.Add(this.LblFuncName);
            this.GrpBasic.Controls.Add(this.label1);
            this.GrpBasic.Controls.Add(this.label4);
            this.GrpBasic.Location = new System.Drawing.Point(12, 30);
            this.GrpBasic.Name = "GrpBasic";
            this.GrpBasic.Size = new System.Drawing.Size(430, 313);
            this.GrpBasic.TabIndex = 13;
            this.GrpBasic.TabStop = false;
            this.GrpBasic.Text = "基本";
            // 
            // GrpProcess
            // 
            this.GrpProcess.Controls.Add(this.progbar);
            this.GrpProcess.Location = new System.Drawing.Point(12, 435);
            this.GrpProcess.Name = "GrpProcess";
            this.GrpProcess.Size = new System.Drawing.Size(430, 54);
            this.GrpProcess.TabIndex = 14;
            this.GrpProcess.TabStop = false;
            this.GrpProcess.Text = "进程";
            // 
            // split2
            // 
            this.split2.Name = "split2";
            this.split2.Size = new System.Drawing.Size(6, 27);
            // 
            // BtNew
            // 
            this.BtNew.Image = ((System.Drawing.Image)(resources.GetObject("BtNew.Image")));
            this.BtNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtNew.Name = "BtNew";
            this.BtNew.Size = new System.Drawing.Size(56, 24);
            this.BtNew.Text = "新建";
            this.BtNew.Click += new System.EventHandler(this.BtNew_Click);
            // 
            // BtLoad
            // 
            this.BtLoad.Image = ((System.Drawing.Image)(resources.GetObject("BtLoad.Image")));
            this.BtLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtLoad.Name = "BtLoad";
            this.BtLoad.Size = new System.Drawing.Size(65, 24);
            this.BtLoad.Text = "载入...";
            // 
            // BtSave
            // 
            this.BtSave.Image = ((System.Drawing.Image)(resources.GetObject("BtSave.Image")));
            this.BtSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtSave.Name = "BtSave";
            this.BtSave.Size = new System.Drawing.Size(56, 24);
            this.BtSave.Text = "保存";
            // 
            // BtStart
            // 
            this.BtStart.Image = ((System.Drawing.Image)(resources.GetObject("BtStart.Image")));
            this.BtStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtStart.Name = "BtStart";
            this.BtStart.Size = new System.Drawing.Size(56, 24);
            this.BtStart.Text = "启动";
            // 
            // BtStop
            // 
            this.BtStop.Image = ((System.Drawing.Image)(resources.GetObject("BtStop.Image")));
            this.BtStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtStop.Name = "BtStop";
            this.BtStop.Size = new System.Drawing.Size(56, 24);
            this.BtStop.Text = "终止";
            // 
            // BtConfig
            // 
            this.BtConfig.Image = global::Vapula.Toolkit.TaskHelper.Properties.Resources.wrench_24;
            this.BtConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtConfig.Name = "BtConfig";
            this.BtConfig.Size = new System.Drawing.Size(65, 24);
            this.BtConfig.Text = "配置...";
            // 
            // dlgopen
            // 
            this.dlgopen.DefaultExt = "dll";
            this.dlgopen.Filter = "组件二进制文件|*.dll";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 502);
            this.Controls.Add(this.GrpProcess);
            this.Controls.Add(this.GrpBasic);
            this.Controls.Add(this.GrpExtension);
            this.Controls.Add(this.toolbar);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(470, 540);
            this.MinimumSize = new System.Drawing.Size(470, 540);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vapula任务助手";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.GrpExtension.ResumeLayout(false);
            this.GrpExtension.PerformLayout();
            this.GrpBasic.ResumeLayout(false);
            this.GrpBasic.PerformLayout();
            this.GrpProcess.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progbar;
        private System.Windows.Forms.ListView LsvParam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtBrowse;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton BtNew;
        private System.Windows.Forms.ToolStripButton BtLoad;
        private System.Windows.Forms.ToolStripButton BtSave;
        private System.Windows.Forms.ToolStripButton BtStart;
        private System.Windows.Forms.GroupBox GrpExtension;
        private System.Windows.Forms.ComboBox CobxXproc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator split1;
        private System.Windows.Forms.ToolStripButton BtStop;
        private System.Windows.Forms.CheckBox ChbxEnableXhost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CobxFuncId;
        private System.Windows.Forms.Label LblFuncName;
        private System.Windows.Forms.Label LblCom;
        private System.Windows.Forms.ColumnHeader ColhId;
        private System.Windows.Forms.ColumnHeader ColhName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader ColhType;
        private System.Windows.Forms.ColumnHeader ColhParamValue;
        private System.Windows.Forms.GroupBox GrpBasic;
        private System.Windows.Forms.GroupBox GrpProcess;
        private System.Windows.Forms.Button BtAttachTags;
        private System.Windows.Forms.Button BtConfigXproc;
        private System.Windows.Forms.ToolStripButton BtConfig;
        private System.Windows.Forms.ToolStripSeparator split2;
        private System.Windows.Forms.OpenFileDialog dlgopen;
    }
}

