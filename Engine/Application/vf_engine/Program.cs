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
            Application.ThreadException += 
                new ThreadExceptionEventHandler(Application_ThreadException);

            string[] args = Environment.GetCommandLineArgs();
            if(args.Length != 2)
            {
                MessageBox.Show(
                    "command line: vf_disp [pipe id]", 
                    "Vapula Dispatcher");
                return;
            }
            Pipe pipe = new Pipe();
            if (!pipe.Connect(args[1])) 
            {
                MessageBox.Show("invalid pipe id", "Vapula Dispatcher");
                return;
            }

            Dispatcher disp = new Dispatcher();
            while (true)
            {
                if (pipe.IsClose) 
                {
                    MessageBox.Show("信道已关闭，分发器将退出。");
                    break;
                }
                if (pipe.HasNewData)
                {
                    string msg = pipe.Read();
                    MessageBox.Show("Received:" + msg, "Vapula Dispatcher");
                    try
                    {
                        Target tar = Target.Parse(msg);
                        disp.Load(tar);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Vapula Dispatcher");
                    }
                }
                Thread.Sleep(50);
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string ex = "Message: " + e.Exception.Message;
            ex += Environment.NewLine + "Source: " + e.Exception.Source;
            ex += Environment.NewLine + "StackTrace: " + e.Exception.StackTrace;
            MessageBox.Show(ex, "Vapula Dispatcher");
            Application.Exit();
        }
    }
}
