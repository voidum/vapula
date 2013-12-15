using System;
using System.Threading;
using System.Windows.Forms;
using Vapula.Runtime;

namespace Vapula.Dispatcher
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
                    "Command Line: vf_disp [pipe id]", 
                    "Vapula Dispatcher");
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
