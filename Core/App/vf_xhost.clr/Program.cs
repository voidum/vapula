using System;
using System.Threading;
using System.Windows.Forms;
using Vapula.Runtime;

namespace Vapula.xHost.CLR
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
            if (args.Length != 2)
            {
                MessageBox.Show(
                    "command line: vf_xhost.clr [pipe id]",
                    "Vapula xHost CLR");
                return;
            }
            Pipe pipe = new Pipe();
            if (!pipe.Connect(args[1]))
            {
                MessageBox.Show("invalid pipe id", "Vapula xHost CLR");
                return;
            }

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
                    MessageBox.Show(msg);
                    UIContext ctx = UIContext.Parse(msg);

                    Thread thread = new Thread(
                        new ThreadStart(() => 
                        {
                            Control ctrl = Loader.LoadCLR(ctx.Path, ctx.Clsid);
                            FrmHost host = new FrmHost();
                            host.Controls.Add(ctrl);
                            host.ClientSize = ctrl.Size;
                            ctrl.Dock = DockStyle.Fill;
                            host.ShowDialog();
                        }));
                    thread.Start();
                }
                Thread.Sleep(50);
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string ex = "Message: " + e.Exception.Message;
            ex += Environment.NewLine + "Source: " + e.Exception.Source;
            ex += Environment.NewLine + "StackTrace: " + e.Exception.StackTrace;
            MessageBox.Show(ex, "Vapula xHost CLR");
            Application.Exit();
        }
    }
}
