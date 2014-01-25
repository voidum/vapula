using System;
using System.Windows.Forms;
using Vapula.Flow;

namespace graph_runner
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Model Graph|*.graph";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            var graph = Graph.Load(dlg.FileName);
            try
            {
                graph.Start();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
