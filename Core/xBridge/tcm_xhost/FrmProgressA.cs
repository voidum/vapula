using System;
using System.Windows.Forms;

namespace TCM.Controls
{
    public partial class FrmProgressA : xHostForm
    {
        public FrmProgressA(string caption = null)
        {
            InitializeComponent();
            if (caption != null) Text = caption;
        }

        protected override float Progress
        {
            set
            {
                int pbv = (int)value;
                pbv = pbv > 100 ? 100 : pbv;
                progbar.Value = pbv;
                LblProgValue.Text = value.ToString("f2") + "%";
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

        /// <summary>
        /// 追加文字（跨线程调用）
        /// </summary>
        public void Invoke_AppendText(string text)
        {
            if (InvokeRequired)
            {
                Action<string> _invoke = new Action<string>(Invoke_AppendText);
                Invoke(_invoke, text);
            }
            else
            {
                LblDescription.Text += (Environment.NewLine + text);
            }
        }
    }
}
