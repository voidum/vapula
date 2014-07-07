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
            task.Stack.Dataset[0].Write();
            task.Start();
            //join
            task.Dispose();
        }
    }
}
