using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Vapula;
using Vapula.Runtime;

namespace sample_xinvoker
{
    public partial class FrmMain : Form
    {
        private DriverHub drv_hub = null;

        public FrmMain()
        {
            InitializeComponent();
            drv_hub = DriverHub.Instance;
            if (!drv_hub.Link("crt")) 
                return;
            if (!drv_hub.Link("clr")) 
                return;
        }

        private void UpdateLog(string log)
        {
            if (InvokeRequired)
            {
                var action = new Action<string>(UpdateLog);
                this.Invoke(action, new object[] { log });
            }
            else
            {
                TbxLog.Text += log + Environment.NewLine;
            }
        }

        void Test1(Library lib)
        {
            Invoker inv = lib.CreateInvoker("context");
            if (inv == null) 
                return;
            if (!inv.Start()) return;
            Stack stk = inv.Stack;
            Context ctx = stk.Context;
            while (ctx.CurrentState != State.Idle)
            {
                float prog = ctx.Progress;
                if (prog > 10)
                {
                    UpdateLog("进度超过10%，暂停");
                    inv.Pause(50);
                    break;
                }
                Thread.Sleep(50);
            }
            int step = 0;
            UpdateLog("已暂停，稍等片刻");
            while (step < 20)
            {
                step++;
                Thread.Sleep(50);
            }
            inv.Resume();
            UpdateLog("已恢复，进度：" + ctx.Progress.ToString() + "%");
            while (ctx.CurrentState != State.Idle)
                Thread.Sleep(50);
            UpdateLog("测试1完成");
            UpdateLog("-------------");
            inv.Dispose();
        }

        void Test2(Library lib)
        {
            UpdateLog("获取用于功能1的调用器对象");
            Invoker inv = lib.CreateInvoker("math");
            if (inv == null)
                return;
            Stack stk = inv.Stack;

            UpdateLog("获取信封对象");
            Envelope env = stk.Envelope;
            if (env == null) 
                return;

            UpdateLog("设置参数");
            env.Write(1, "12");
            env.Write(2, "23");

            UpdateLog("执行功能");
            if (!inv.Start())
                return;

            Context ctx = stk.Context;
            while (ctx.CurrentState != State.Idle)
                Thread.Sleep(50);

            UpdateLog("验证输出：" + env.Read(3));

            //double td_time = 0;
            for (int i = 0; i < 2000; i++)
            {
                env.Write(1, "12");
                env.Write(2, "23");
                inv.Start();
                ctx = stk.Context;
                //sw = ctx.GetStopwatch();
                while (ctx.CurrentState != State.Idle) 
                    Thread.Sleep(0);
                //td_time += sw.GetElapsedTime();
                int.Parse(env.Read(3));
            }
            inv.Dispose();
        }

        private void BtRun1_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, @"sample_xlib.library");
            Library lib = Library.Load(path);
            if (lib == null) 
                return;
            Thread thread = new Thread(
                new ThreadStart(() => 
                {
                    if (!lib.Mount())
                    {
                        UpdateLog("加载库发生问题");
                        return;
                    }
                    Test1(lib);
                    Test2(lib);
                    lib.Unmount();
                }));
            thread.Start();
        }
    }
}
