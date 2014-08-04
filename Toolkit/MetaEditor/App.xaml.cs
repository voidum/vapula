using System;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace MetaEditor
{
    public partial class App : Application
    {
        private Runtime _Runtime 
            = new Runtime();

        public Runtime Runtime
        {
            get { return _Runtime; }
            set { _Runtime = value; }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoadLangPack();
            LoadWindows();
            Runtime.Windows["start"].Show();
        }

        private void LoadLangPack() 
        {
            var args = Environment.GetCommandLineArgs();
            var lang_path = Path.Combine(
                Runtime.StartupDirectory,
                (args.Length > 1 ? args[1] : "en-us") + ".xml");
            var xml = XElement.Load(lang_path);
            Runtime.LangPack.Load(xml, "lang|item|key");
        }

        private void LoadWindows()
        {
            var windows = Runtime.Windows;
            windows.LinkWindow(new StartWindow());
            windows.LinkWindow(new MainWindow());
        }
    }
}
