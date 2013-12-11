using System;
using System.Threading;
using System.Windows.Forms;
using TCM.Runtime;

namespace TCM.Dispatcher
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();
            if(args.Length != 1)
            {
                MessageBox.Show(
                    "Command Line: tcm_disp [pipe id]", 
                    "TCM Dispatcher");
            }
            Pipe pipe = new Pipe();
            if (!pipe.Connect(args[0])) 
            {
                MessageBox.Show("err id");
                return;
            }
            while (true)
            {
                if (pipe.HasNewData)
                {
                    string msg = pipe.Read();
                    MessageBox.Show(msg);
                }
                Thread.Sleep(50);
            }
        }
    }
}
