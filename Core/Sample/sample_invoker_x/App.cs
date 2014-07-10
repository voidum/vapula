using System;
using System.IO;
using System.Threading;
using Vapula;
using Vapula.Runtime;

namespace sample_invoker_x
{
    public class App
    {
        Library _Library 
            = null;

        public void Start() 
        {
            Runtime.Instance.Start();
            var driver = Driver.Load(Path.Combine(Base.RuntimeDir, "clr.driver"));
            driver.LinkHub();
            _Library = Library.Load(Path.Combine(Base.RuntimeDir, "sample_lib_x.library"));
            _Library.LinkHub();
            _Library.Mount();
        }

        public void Stop() 
        {
            Runtime.Instance.Stop();
        }

        public void Test1() 
        {
            Console.WriteLine("create task: math");
            var task = _Library.CreateTask("math");
            int[] data1 = new int[] { 1, 2, 3, 4 };
            var pointer = new Pointer();
            pointer.WriteArray(data1);
            task.Stack.Dataset[1].Write(pointer.Data, pointer.Size, false);

            Console.WriteLine("invoke task: math");
            task.Start();
            var context = task.Stack.Context;
            while (context.CurrentState != State.Idle)
                Thread.Sleep(50);

            Record record = task.Stack.Dataset[2];
            pointer.Capture(record.Read(), record.Size);
            int result = pointer.ReadValue<int>();
            Console.WriteLine("<valid> - out:" + result.ToString());

            

            //join
            task.Dispose();
        }
    }
}
