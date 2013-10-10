using System;
using System.Windows.Forms;

namespace TCM
{
    public partial class xHostForm : Form
    {
        protected Action _OnSwitchPause;
        protected Action _OnCancel;
        protected bool _IsShown = false;

        public xHostForm()
        {
            InitializeComponent();
        }

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
        public Action OnCancel
        {
            get { return _OnCancel; }
            set { _OnCancel = value; }
        }

        /// <summary>
        /// 获取进度窗体的显示状态
        /// </summary>
        public bool IsShown
        {
            get { return _IsShown; }
        }

        /// <summary>
        /// 设置进度的实际操作
        /// </summary>
        protected virtual float Progress
        {
            set { }
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
                _OnCancel();
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
                Progress = prog;
            }
        }

        private void xHostForm_Shown(object sender, EventArgs e)
        {
            _IsShown = true;
        }
    }
}
