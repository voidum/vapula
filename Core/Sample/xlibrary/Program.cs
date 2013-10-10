using System.Threading;
using TCM;
using TCM.Runtime;

namespace sample_xcom
{
    public class Program
    {
        public ReturnCode Run(int function, Envelope envelope, Context context)
        {
            Sample sample = new Sample(envelope, context);
            switch (function)
            {
                case 0: return sample.Function_Math();
                case 1: return sample.Function_Out();
                case 2: return sample.Function_Array();
                case 3: return sample.Function_Object();
                case 4: return sample.Function_Context();
                default: return ReturnCode.NullEntry;
            }
        }
    }

    public class Sample
    {
        private Envelope _Envelope;
        private Context _Context;

        public Sample(Envelope env, Context ctx)
        {
            _Envelope = env;
            _Context = ctx;
        }

        public ReturnCode Function_Math()
        {
            int a = int.Parse(_Envelope.Read(0));
            int b = int.Parse(_Envelope.Read(1));
            int c = a + b;
            _Envelope.Write(2, c.ToString());
            return ReturnCode.Normal;
        }

        public ReturnCode Function_Out()
        {
            _Envelope.Write(0, "中文Engligh日本語テスト");
            return ReturnCode.Normal;
        }

        public ReturnCode Function_Array()
        {
            return ReturnCode.NullTask;
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
        public ReturnCode Function_Object()
        {
            /*
            TestClassA* obj = (TestClassA*)envelope->Read<LPVOID>(0);
            bool ifinc = envelope->Read<bool>(1);

            if (ifinc) obj->Inc();
            else obj->Dec();

            envelope->Write(2, (LPVOID)obj);
             */
            //TODO: 实现结构探测
            return ReturnCode.NullTask;
        }

        //第五个任务
        public ReturnCode Function_Context()
        {
            for (int i = 0; i < 1000; i++)
            {
                CtrlCode ctrl = _Context.CtrlCode;
                if (ctrl == CtrlCode.Cancel)
                    return ReturnCode.CancelByMsg;
                if (ctrl == CtrlCode.Pause)
                {
                    _Context.ReplyCtrlCode();
                    while (true)
                    {
                        ctrl = _Context.CtrlCode;
                        if (ctrl == CtrlCode.Resume)
                        {
                            _Context.ReplyCtrlCode();
                            break;
                        }
                        Thread.Sleep(25);
                    }
                }
                _Context.Progress = i / 10.0f;
                Thread.Sleep(25);
            }
            return ReturnCode.Normal;
        }
    }
}
