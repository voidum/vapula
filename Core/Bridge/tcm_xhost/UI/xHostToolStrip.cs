using System;
using System.Drawing;
using System.Windows.Forms;

namespace TCM.xHost
{
    public class xHostToolStrip : ToolStrip
    {
        private ToolStripButton _BtBack;
        private ToolStripButton _BtRefresh;
        private ToolStripButton _BtForward;
        private ToolStripButton _BtOption;
        private ToolStripTextBox _TbxURL;

        private WebBrowser _Browser = null;

        public WebBrowser Browser
        {
            get { return _Browser; }
            set 
            { 
                _Browser = value;
                OnSizeChanged(null);
            }
        }

        private ToolStripButton FormLayout_CreateButton()
        {
            var bt = new ToolStripButton();
            bt.DisplayStyle = ToolStripItemDisplayStyle.Image;
            bt.ImageTransparentColor = Color.Magenta;
            bt.Overflow = ToolStripItemOverflow.Never;
            bt.Size = new Size(23, 22);
            return bt;
        }

        public xHostToolStrip()
        {
            _BtBack = FormLayout_CreateButton();
            _BtForward = FormLayout_CreateButton();
            _BtRefresh = FormLayout_CreateButton();
            _BtOption = FormLayout_CreateButton();
            _TbxURL = new ToolStripTextBox();

            SuspendLayout();

            GripStyle = ToolStripGripStyle.Hidden;
            ImageScalingSize = new Size(18, 18);
            Items.AddRange(
                new ToolStripItem[] {
                    _BtBack, _BtForward, _BtRefresh,
                    _TbxURL, _BtOption});
            Location = new Point(0, 0);
            Name = "toolbar";
            Padding = new Padding(1, 0, 1, 1);
            Size = new Size(284, 26);
            TabIndex = 0;

            _BtBack.Image = global::TCM.Properties.Resources.back;
            _BtBack.ToolTipText = "Back";
            
            _BtForward.Image = global::TCM.Properties.Resources.forward;
            _BtForward.ToolTipText = "Forward";

            _BtRefresh.Image = global::TCM.Properties.Resources.refresh;
            _BtRefresh.ToolTipText = "Refresh";

            _BtOption.Image = global::TCM.Properties.Resources.menu;
            _BtOption.ToolTipText = "Option";

            _TbxURL.AutoSize = false;
            _TbxURL.Overflow = ToolStripItemOverflow.Never;
            _TbxURL.Size = new Size(50, 25);
            _TbxURL.ToolTipText = "URL";
            _TbxURL.KeyPress += new KeyPressEventHandler(TbxURL_KeyPress);
            
            ResumeLayout(false);
            PerformLayout();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _TbxURL.Width = Width
                - _BtBack.Width - _BtForward.Width
                - _BtOption.Width - _BtRefresh.Width
                - 4;
        }

        private void TbxURL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                _Browser.CefBrowser.GetMainFrame().LoadUrl(_TbxURL.Text);
                e.Handled = true;
            }
        }
    }
}
