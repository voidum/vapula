using System;
using Xilium.CefGlue;

namespace TCM.xHost
{
    public abstract class xHostApp : IDisposable
    {
        private const string DumpRequestDomain = "dump-request.demoapp.cefglue.xilium.local";

        #region 构造
        protected xHostApp()
        {
        }

        ~xHostApp()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
        #endregion

        public string Name { get { return "Xilium CefGlue Demo"; } }
        public int DefaultWidth { get { return 800; } }
        public int DefaultHeight { get { return 600; } }
        public string HomeUrl { get { return "http://google.com"; } }

        public int Run(string[] args)
        {
            CefRuntime.Load();

            var settings = new CefSettings();
            settings.MultiThreadedMessageLoop = CefRuntime.Platform == CefRuntimePlatform.Windows;
            settings.ReleaseDCheckEnabled = true;
            settings.LogSeverity = CefLogSeverity.Verbose;
            settings.LogFile = "cef.log";
            settings.ResourcesDirPath = System.IO.Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase).LocalPath);
            settings.RemoteDebuggingPort = 20480;

            var argv = args;
            if (CefRuntime.Platform != CefRuntimePlatform.Windows)
            {
                argv = new string[args.Length + 1];
                Array.Copy(args, 0, argv, 1, args.Length);
                argv[0] = "-";
            }

            var mainArgs = new CefMainArgs(argv);
            var app = new xHostCefApp();

            var exitCode = CefRuntime.ExecuteProcess(mainArgs, app);
            Console.WriteLine("CefRuntime.ExecuteProcess() returns {0}", exitCode);
            if (exitCode != -1)
                return exitCode;

            // guard if something wrong
            foreach (var arg in args) { if (arg.StartsWith("--type=")) { return -2; } }

            CefRuntime.Initialize(mainArgs, settings, app);

            // register custom scheme handler
            CefRuntime.RegisterSchemeHandlerFactory("http", DumpRequestDomain, new DemoAppSchemeHandlerFactory());
            // CefRuntime.AddCrossOriginWhitelistEntry("http://localhost", "http", "", true);

            PlatformInitialize();

            IView view = CreateView();
            PlatformRunMessageLoop();
            view.Dispose();

            CefRuntime.Shutdown();

            PlatformShutdown();
            return 0;
        }

        public void Quit()
        {
            PlatformQuitMessageLoop();
        }

        protected abstract void PlatformInitialize();

        protected abstract void PlatformShutdown();

        protected abstract void PlatformRunMessageLoop();

        protected abstract void PlatformQuitMessageLoop();

        protected abstract IView CreateView();
    }
}
