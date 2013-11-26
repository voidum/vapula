﻿using System;
using Xilium.CefGlue;

namespace TCM.xHost
{
    internal sealed class BrowserProcessHandler : CefBrowserProcessHandler
    {
        protected override void OnBeforeChildProcessLaunch(CefCommandLine commandLine)
        {
            Console.WriteLine("AppendExtraCommandLineSwitches: {0}", commandLine);
            Console.WriteLine(" Program == {0}", commandLine.GetProgram());
            Console.WriteLine("  -> {0}", commandLine);
        }
    }
}
