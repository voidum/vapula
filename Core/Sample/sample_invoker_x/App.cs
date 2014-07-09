using System;
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
            var driver = Driver.Load("");
            driver.LinkHub();
            _Library = Library.Load("");
            _Library.LinkHub();
        }

        public void Stop() 
        {
            Runtime.Instance.Stop();
        }

        public void Test1() 
        {
            var pointer = new Pointer();
            int[] data1 = new int[] { 1, 2, 3, 4 };
            pointer.WriteArray(data1);
            byte[] data2 = pointer.ReadArray<byte>();

            var task = _Library.CreateTask("math");
            pointer = new Pointer();
            pointer.WriteArray(new int[] { 0, 1, 2, 3 });
            task.Stack.Dataset[0].Write(pointer.Data, (UInt32)pointer.Size, false);
            task.Start();
            //join
            task.Dispose();
        }
    }
}
