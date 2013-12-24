﻿using System;
using System.Threading;
using System.Windows.Forms;
using Vapula.Helper;
using Vapula.Runtime;
using xDockPanel;

namespace Vapula.Designer
{
    public partial class FrmMain : Form
    {
        private FrmDocument doc;

        private AppData App
        {
            get { return AppData.Instance; }
        }

        private void FormAction_StartModel()
        {
            App.FormLog.WriteLog(LogType.Event, "开始执行模型");
            GraphProxy proxy = new GraphProxy(doc.Model);
            proxy.Logger = App.FormLog;
            proxy.Start();
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            App.FormToolbox.Show(paneldock, DockState.Left);
            App.FormDebug.Show(paneldock, DockState.AutoHideRight);
            App.FormLog.Show(paneldock, DockState.AutoHideBottom);
            doc = new FrmDocument();
            doc.Show(paneldock, DockState.Document);
        }

        private void MnuModel_Start_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(FormAction_StartModel);
            thread.Start();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                if(MessageBox.Show(
                    "是否确定退出模型设计器？", "询问",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void MnuFile_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}