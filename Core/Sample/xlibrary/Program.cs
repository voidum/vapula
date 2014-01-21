using System.Threading;
using Vapula.Runtime;

namespace sample_xlib
{
    public class Sample
    {
        public void Function_Math()
        {
            Stack stack = Stack.Instance;
            Envelope env = stack.Envelope;
            int a = int.Parse(env.Read(1));
            int b = int.Parse(env.Read(2));
            int c = a + b;
            env.Write(3, c.ToString());
            stack.Context.ReturnCode = ReturnCode.Normal;
        }

        public void Function_Out()
        {
            Stack stack = Stack.Instance;
            Envelope env = stack.Envelope;
            env.Write(1, "中文English日本語テスト");
            stack.Context.ReturnCode = ReturnCode.Normal;
        }

        public void Function_Array()
        {
            Stack stack = Stack.Instance;
            stack.Context.ReturnCode = ReturnCode.NullTask;
            /*
            int count = int.Parse(_Envelope.Read(0));
            //TODO: 实现读取数组
            int[] data = new int[3];
                //new IntPtr(int.Parse(_Envelope.Read(1)));
            int result = 0;
            for (int i = 0; i < count; i++)
                result += data[i];
            _Envelope.Write(2, result.ToString());
            return ReturnCode.Normal;
             */
        }

        //第四个任务
        public void Function_Object()
        {
            Stack stack = Stack.Instance;
            stack.Context.ReturnCode = ReturnCode.NullTask;
            /*
            TestClassA* obj = (TestClassA*)envelope->Read<LPVOID>(0);
            bool ifinc = envelope->Read<bool>(1);

            if (ifinc) obj->Inc();
            else obj->Dec();

            envelope->Write(2, (LPVOID)obj);
             */
            //TODO: 实现结构探测
        }

        //第五个任务
        public void Function_Context()
        {
            Stack stack = Stack.Instance;
            Context ctx = stack.Context;
            for (int i = 0; i < 1000; i++)
            {
                CtrlCode ctrl = ctx.CtrlCode;
                if (ctrl == CtrlCode.Cancel)
                    ctx.ReturnCode = ReturnCode.Cancel;
                if (ctrl == CtrlCode.Pause)
                {
                    ctx.SwitchHold();
                    while (true)
                    {
                        ctrl = ctx.CtrlCode;
                        if (ctrl == CtrlCode.Resume)
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
