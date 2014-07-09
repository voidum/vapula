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
            var task = _Library.CreateTask("math");
            var pointer = new Pointer();
            pointer.WriteArray(new int[] { 0, 1, 2, 3 });
            task.Stack.Dataset[0].Write(pointer.Data, pointer.Size, false);
            task.Start();
            //join
            task.Dispose();
        }
    }
}
