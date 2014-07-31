using Vapula.Model;

namespace MetaEditor
{
    public class Runtime
    {
        private Library _Library 
            = null;
        public Library Library 
        {
            get { return _Library; }
            set { _Library = value; }
        }
    }
}
