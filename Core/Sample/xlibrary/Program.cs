using System;
using System.Threading;
using System.Windows.Forms;
using Vapula;
using Vapula.Runtime;

namespace sample_xlib
{
    public class Sample
    {
        public void Function_Math()
        {
            Stack stack = Stack.Instance;
            Dataset dataset = stack.Dataset;
            IntPtr data = dataset[1].Read();
            //int[] 
            int c = a + b;
            dataset[3].Write(c);
            stack.Context.ReturnCode = ReturnCode.Normal;
        }

        public void Function_Out()
        {
            Stack stack = Stack.Instance;
            Dataset dataset = stack.Dataset;
            dataset[1].WriteText("中文English日本語テスト");
            stack.Context.ReturnCode = ReturnCode.Normal;
        }

        public void Function_Context()
        {
            Stack stack = Stack.Instance;
            Context ctx = stack.Context;
            for (int i = 0; i < 1000; i++)
            {
                ControlCode ctrl = ctx.ControlCode;
                if (ctrl == ControlCode.Cancel)
                    ctx.ReturnCode = ReturnCode.Cancel;
                if (ctrl == ControlCode.Pause)
                {
                    ctx.SwitchHold();
                    while (true)
                    {
                        ctrl = ctx.ControlCode;
                        if (ctrl == ControlCode.Resume)
                        {
                            ctx.SwitchHold();
                            break;
                        }
                        Thread.Sleep(25);
                    }
                }
                ctx.Progress = i / 10.0f;
                Thread.Sleep(25);
            }
            ctx.ReturnCode = ReturnCode.Normal;
        }
    }
}
