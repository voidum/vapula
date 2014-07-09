using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sample_invoker_x
{
    class Program
    {
        static void Main(string[] args)
        {
            App app = new App();
            //app.Start();
            app.Test1();
            app.Stop();
        }
    }
}
