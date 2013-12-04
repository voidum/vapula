using System;
using System.IO;
using System.Reflection;
using Xilium.CefGlue;

namespace TCM.xHost.Web
{
    public abstract class xHostApp : IDisposable
    {
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

        public int Run(string[] args)
        {
            CefRuntime.Load();

            var settings = new CefSettings();
            settings.MultiThreadedMessageLoop = (CefRuntime.Platform == CefRuntimePlatform.Windows);
            settings.ReleaseDCheckEnabled = true;
            settings.LogSeverity = CefLogSeverity.Error;
            settings.LogFile = "cef.log";
            settings.ResourcesDirPath = Path.GetDirectoryName(new Uri(Assembly.GetEntryAssembly().CodeBase).LocalPath);
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
            foreach (var arg in args) 
            { if (arg.StartsWith("--type=")) { return -2; } }

            CefRuntime.Initialize(mainArgs, settings, app);

            // register custom scheme handler
            //CefRuntime.RegisterSchemeHandlerFactory("http", ??? , new xHostSchemeHandlerFactory());
            // CefRuntime.AddCrossOriginWhitelistEntry("http://localhost", "http", "", true);

            Initialize();

            IView view = CreateView();
            RunMessageLoop();
            view.Dispose();

            CefRuntime.Shutdown();

            Shutdown();
            return 0;
        }

        public void Quit()
        {
            QuitMessageLoop();
        }

        protected abstract void Initialize();

        protected abstract void Shutdown();

        protected abstract void RunMessageLoop();

        protected abstract void QuitMessageLoop();

        protected abstract IView CreateView();
    }
}
