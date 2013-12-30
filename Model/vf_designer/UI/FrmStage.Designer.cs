namespace Vapula.Designer
{
    partial class FrmStage
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
            this.LsvStages = new System.Windows.Forms.ListView();
            this.ColhStageId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColhStageNodes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // LsvStages
            // 
            this.LsvStages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColhStageId,
            this.ColhStageNodes});
            this.LsvStages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LsvStages.Location = new System.Drawing.Point(0, 0);
            this.LsvStages.Name = "LsvStages";
            this.LsvStages.Size = new System.Drawing.Size(284, 262);
            this.LsvStages.TabIndex = 0;
            this.LsvStages.UseCompatibleStateImageBehavior = false;
            this.LsvStages.View = System.Windows.Forms.View.Details;
            // 
            // ColhStageId
            // 
            this.ColhStageId.Text = "标识";
            // 
            // ColhStageNodes
            // 
            this.ColhStageNodes.Text = "节点";
            this.ColhStageNodes.Width = 200;
            // 
            // FrmStage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.LsvStages);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmStage";
            this.Text = "阶段";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmStage_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LsvStages;
        private System.Windows.Forms.ColumnHeader ColhStageId;
        private System.Windows.Forms.ColumnHeader ColhStageNodes;
    }
}