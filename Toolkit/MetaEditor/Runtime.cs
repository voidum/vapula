using Sartrey;
using System;
using System.IO;
using Vapula.Model;

namespace MetaEditor
{
    public class Runtime
    {
        public static Runtime Instance 
        {
            get { return (App.Current as App).Runtime; }
        }

        public string StartupDirectory
        {
            get 
            {
                var args = Environment.GetCommandLineArgs();
                return Path.GetDirectoryName(args[0]);
            }
        }

        private Table<string> _LangPack 
            = new Table<string>();
        public Table<string> LangPack
        {
            get { return _LangPack; }
        }

        private WindowHub _Windows
            = new WindowHub();

        public WindowHub Windows 
        {
            get { return _Windows; }
        }

        private Library _Library 
            = null;
        public Library Library 
        {
            get { return _Library; }
            set { _Library = value; }
        }
    }
}
