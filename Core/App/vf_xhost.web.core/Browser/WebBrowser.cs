using System;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace Vapula.xHost.Web
{
    /// <summary>
    /// 基于CEF的嵌入式浏览器
    /// </summary>
    public sealed class WebBrowser : Control
    {
        private readonly CefBrowserSettings _Settings;
        private CefClient _Client;
        private CefBrowser _Browser;
        private IntPtr _BrowserHandle
            = IntPtr.Zero;

        public WebBrowser()
            : this(new CefBrowserSettings())
        {
        }

        public WebBrowser(CefBrowserSettings settings)
        {
            SetStyle(
                ControlStyles.ContainerControl | 
                ControlStyles.ResizeRedraw |
                ControlStyles.FixedWidth |
                ControlStyles.FixedHeight | 
                ControlStyles.StandardClick |
                ControlStyles.UserMouse | 
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.StandardDoubleClick |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.CacheText |
                ControlStyles.EnableNotifyMessage | 
                ControlStyles.DoubleBuffer | 
                ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.UseTextForAccessibility |
                ControlStyles.Opaque,
                false);

            SetStyle(
                ControlStyles.UserPaint | 
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.Selectable,
                true);

            _Settings = settings;
            // _Settings.ImageLoading = CefState.Disabled;
            // _Settings.AcceleratedCompositing = CefState.Disabled;
        }

        public CefBrowser CefBrowser
        {
            get { return _Browser; }
        }

        public void Create(CefWindowInfo windowInfo)
        {
            if (_Client == null)
                _Client = new WebClient(this);
            CefBrowserHost.CreateBrowser(windowInfo, _Client, _Settings, "about:blank");
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (DesignMode)
            {
                // if (!_handleCreated) Paint += PaintInDesignMode;
            }
            else
            {
                var wi = CefWindowInfo.Create();
                wi.SetAsChild(Handle,
                    new CefRectangle(0, 0, Width, Height));
                Create(wi);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            var form = TopLevelControl as Form;
            if (form != null &&
                form.WindowState != FormWindowState.Minimized)
            {
                ResizeWindow(Width, Height);
            }
        }

        #region WebLifeSpanHandler
        /// <summary>
        /// 待研
        /// </summary>
        //public event EventHandler Created;

        /// <summary>
        /// 内部方法，用于触发Created事件
        /// </summary>
        internal void OnCreated(CefBrowser browser)
        {
            _Browser = browser;
            _BrowserHandle = _Browser.GetHost().GetWindowHandle();
            ResizeWindow(Width, Height);
            /*
            //if (_created) throw new InvalidOperationException("Browser already created.");
            if (Created != null)
                Created(this, EventArgs.Empty);
             */
        }
        #endregion

        #region WebDisplayHandler
        /// <summary>
        /// 当前页面的标题发生变更
        /// </summary>
        public event EventHandler<TitleChangedEventArgs> TitleChanged;

        /// <summary>
        /// 当前页面的URL发生变更
        /// </summary>
        public event EventHandler<AddressChangedEventArgs> AddressChanged;

        /// <summary>
        /// 目标URL发生变更
        /// </summary>
        public event EventHandler<TargetUrlChangedEventArgs> TargetUrlChanged;

        /// <summary>
        /// 内部方法，用于触发TitleChanged事件
        /// </summary>
        internal void OnTitleChanged(string title)
        {
            if (TitleChanged != null)
                TitleChanged(this, new TitleChangedEventArgs(title));
        }

        /// <summary>
        /// 内部方法，用于触发AddressChanged事件
        /// </summary>
        internal void OnAddressChanged(string address)
        {
            if (AddressChanged != null)
                AddressChanged(this, new AddressChangedEventArgs(address));
        }

        /// <summary>
        /// 内部方法，用于触发TargetUrlChanged事件
        /// </summary>
        internal void OnTargetUrlChanged(string targetUrl)
        {
            if (TargetUrlChanged != null)
                TargetUrlChanged(this, new TargetUrlChangedEventArgs(targetUrl));
        }
        #endregion

        #region WebLoadHandler
        /// <summary>
        /// 加载状态发生变更
        /// </summary>
        public event EventHandler<LoadingStateChangedEventArgs> LoadingStateChanged;

        /// <summary>
        /// 内部方法，用于触发LoadingStateChanged事件
        /// </summary>
        internal void OnLoadingStateChanged(bool isLoading, bool canGoBack, bool canGoForward)
        {
            if (LoadingStateChanged != null)
                LoadingStateChanged(this, new LoadingStateChangedEventArgs(isLoading, canGoBack, canGoForward));
        }
        #endregion

        private void ResizeWindow(int width, int height)
        {
            if (_BrowserHandle != IntPtr.Zero)
            {
                NativeMethods.SetWindowPos(
                    _BrowserHandle, IntPtr.Zero,
                    0, 0, width, height,
                    SetWindowPosFlags.NoMove | SetWindowPosFlags.NoZOrder);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_Browser != null)
            {
                var host = _Browser.GetHost();
                host.CloseBrowser();
                host.ParentWindowWillClose();
                host.Dispose();
                _Browser.Dispose();
                _Browser = null;
                _BrowserHandle = IntPtr.Zero;
            }
            base.Dispose(disposing);
        }

        /*
        private void PaintInDesignMode(object sender, PaintEventArgs e)
        {
            var width = this.Width;
            var height = this.Height;
            if (width > 1 && height > 1)
            {
                var brush = new SolidBrush(this.ForeColor);
                var pen = new Pen(this.ForeColor);
                pen.DashStyle = DashStyle.Dash;

                e.Graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);

                var fontHeight = (int)(this.Font.GetHeight(e.Graphics) * 1.25);

                var x = 3;
                var y = 3;

                e.Graphics.DrawString("CefWebBrowser", Font, brush, x, y + (0 * fontHeight));
                e.Graphics.DrawString(string.Format("StartUrl: {0}", StartUrl), Font, brush, x, y + (1 * fontHeight));

                brush.Dispose();
                pen.Dispose();
            }
        }
        */
    }
}
