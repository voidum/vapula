using System.Threading;
using Vapula;
using Vapula.Runtime;

namespace sample_lib_x
{
    public class Sample
    {
        public void Math()
        {
            Stack stack = Stack.Instance;
            Dataset dataset = stack.Dataset;
            Record record = dataset[1];
            Pointer pointer = new Pointer();
            pointer.Capture(record.Read(), record.Size);
            int[] data = pointer.ReadArray<int>();
            int result = 0;
            foreach (int v in data)
                result += v;
            pointer.Release();
            pointer.WriteValue(result);
            dataset[2].Write(pointer.Data, pointer.Size);
            stack.Context.ReturnCode = ReturnCode.Normal;
        }

        public void Out()
        {
            Stack stack = Stack.Instance;
            Dataset dataset = stack.Dataset;
            string data = "中文English日本語テスト";
            Pointer pointer = new Pointer();
            pointer.WriteArray(data.ToCharArray());
            dataset[1].Write(pointer.Data, pointer.Size);
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
