using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TCM.xHost
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static int Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] cmds = Environment.GetCommandLineArgs();
            if (cmds.Length >= 2)
            {
                CheckOption(cmds);
                /*
      			Task* task = Task::Parse(argv[1]);
	    		if(!task) return -1;
		    	task->Mount();
			    task->Run(_FlagApp);
                 */
            }
            else
        	{
                ShowHelp();
        	}
            return 0;
        }

        static void CheckOption(string[] cmds)
        {
            AppData.Flags.Add("rtmon", cmds.Contains("rtmon"));
        }

        static void ShowHelp()
        {
            string str = "command lines:\n";
            str += " tcm_host [task file] [option]\n";
            str += "option:\n";
            str += " \"rtmon\" - to monitor in high CPU usage\n";
            MessageBox.Show(str, "TCM xHost");
        }
    }
}
