using System.Windows;

namespace MetaEditor
{
    public partial class App : Application
    {
        private Runtime _Runtime 
            = null;

        public Runtime Runtime
        {
            get { return _Runtime; }
            set { _Runtime = value; }
        }
    }
}
