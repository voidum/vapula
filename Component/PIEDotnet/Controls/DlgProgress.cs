using System;
using System.Windows.Forms;

namespace PIE.Controls
{
    public partial class DlgProgress : Form
    {
        private Action _OnSwitchPause;
        private Func<bool> _OnCancel;
        private bool _IsShown = false;
        private bool _IfForceClose = false;

        /// <summary>
        /// 切换暂停/恢复状态的回调
        /// </summary>
        public Action OnSwitchPause
        {
            get { return _OnSwitchPause; }
            set { _OnSwitchPause = value; }
        }

        /// <summary>
        /// 取消任务的回调，建议使用同步方法
        /// </summary>
        public Func<bool> OnCancel
        {
            get { return _OnCancel; }
            set { _OnCancel = value; }
        }

        /// <summary>
        /// 获取进度窗体的显示状态
        /// </summary>
        public bool IsShown { get { return _IsShown; } }

        public DlgProgress(string caption = null)
        {
            InitializeComponent();
            if (caption != null) Text = caption;
        }

        /// <summary>
        /// 关闭进度窗体（跨线程调用）
        /// </summary>
        public void Invoke_CloseForm()
        {
            if (InvokeRequired)
            {
                Action _invoke = new Action(Invoke_CloseForm);
                Invoke(_invoke, null);
            }
            else
            {
                _IfForceClose = true;
                Close();
            }
        }

        /// <summary>
        /// 更新进度（跨线程调用）
        /// </summary>
        public void Invoke_UpdateProgress(float prog)
        {
            if (InvokeRequired)
            {
                Action<float> _invoke = new Action<float>(Invoke_UpdateProgress);
                Invoke(_invoke, prog);
            }
            else
            {
                int pbv = (int)(prog > 100 ? 100 : prog);
                progbar.Value = pbv;
                LblProgValue.Text = prog.ToString("f2") + "%";
            }
        }

        /// <summary>
        /// 更新文字（跨线程调用）
        /// </summary>
        public void Invoke_UpdateText(string text)
        {
            if (InvokeRequired)
            {
                Action<string> _invoke = new Action<string>(Invoke_UpdateText);
                Invoke(_invoke, text);
            }
            else
            {
                LblDescription.Text = text;
            }
        }

        private void BtRunning_Click(object sender, EventArgs e)
        {
            if (_OnSwitchPause != null) _OnSwitchPause();
        }

        private void FrmProgress_Shown(object sender, EventArgs e)
        {
            _IsShown = true;
        }

        private void FrmProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_IfForceClose)
            {
                LblDescription.Text += "\n正在取消任务...";
                if (!_OnCancel())
                {
                    LblDescription.Text += "\n任务取消失败。";
                    e.Cancel = true;
                }
            }
        }
    }
}
