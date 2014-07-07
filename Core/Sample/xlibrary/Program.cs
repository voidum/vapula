using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Vapula;
using Vapula.Runtime;

namespace sample_xlib
{
    public class Sample
    {
        public void Math()
        {
            Stack stack = Stack.Instance;
            Dataset dataset = stack.Dataset;
            IntPtr handle = dataset[1].Read();
            int size = (int)dataset[1].Size / sizeof(int);
            int[] data = new int[size];
            Marshal.Copy(handle, data, 0, size);
            int[] result = new int[1];
            foreach (int v in data)
                result[0] += v;
            IntPtr handle_out = Bridge.NewData(sizeof(int));
            Marshal.Copy(result, 0, handle_out, 1);
            dataset[2].Write(handle_out, sizeof(int));
            stack.Context.ReturnCode = ReturnCode.Normal;
        }

        public void Out()
        {
            Stack stack = Stack.Instance;
            Dataset dataset = stack.Dataset;
            string data = "中文English日本語テスト";
            IntPtr handle_out = Bridge.NewData((uint)(data.Length * sizeof(char)));
            Marshal.Copy(data.ToCharArray(), 0, handle_out, data.Length);
            dataset[1].Write(handle_out, (uint)(data.Length * sizeof(char)));
            stack.Context.ReturnCode = ReturnCode.Normal;
        }

        public void Context()
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
