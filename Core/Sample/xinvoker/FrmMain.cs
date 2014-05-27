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
        private Runtime drv_hub = null;

        public FrmMain()
        {
            InitializeComponent();
            Runtime runtime = Runtime.Instance;
            IntPtr driver = Bridge.LoadDriver("");
            runtime.LinkObject(driver);
            driver = Bridge.LoadDriver("");
            runtime.LinkObject(driver);
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

        void Test1(Library library)
        {
            Task task = library.CreateTask("context");
            if (task == null) 
                return;
            task.Start();
            Stack stack = task.Stack;
            Context context = stack.Context;
            while (context.CurrentState != State.Idle)
            {
                float progress = context.Progress;
                if (progress > 10)
                {
                    UpdateLog("进度超过10%，暂停");
                    task.Pause(50);
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
            task.Resume();
            UpdateLog("已恢复，进度：" + context.Progress.ToString() + "%");
            while (context.CurrentState != State.Idle)
                Thread.Sleep(50);
            UpdateLog("测试1完成");
            UpdateLog("-------------");
            task.Dispose();
        }

        void Test2(Library library)
        {
            UpdateLog("获取用于功能1的调用器对象");
            Task task = library.CreateTask("math");
            if (task == null)
                return;
            Stack stack = task.Stack;

            UpdateLog("获取信封对象");
            Dataset dataset = stack.Dataset;
            if (dataset == null) 
                return;

            UpdateLog("设置参数");
            dataset[1].WriteAt(12);
            dataset[2].WriteAt(23);

            UpdateLog("执行功能");
            task.Start();

            Context context = stack.Context;
            while (context.CurrentState != State.Idle)
                Thread.Sleep(50);

            UpdateLog("验证输出：" + dataset[3].ReadAt<int>().ToString());

            //double td_time = 0;
            for (int i = 0; i < 2000; i++)
            {
                dataset[1].WriteAt(12);
                dataset[2].WriteAt(23);
                task.Start();
                context = stack.Context;
                //sw = ctx.GetStopwatch();
                while (context.CurrentState != State.Idle) 
                    Thread.Sleep(0);
                //td_time += sw.GetElapsedTime();
                dataset[3].ReadAt<int>();
            }
            task.Dispose();
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
