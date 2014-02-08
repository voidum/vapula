using Vapula.Runtime;

namespace sample_xpipe
{
    public class AppData
    {
        private static AppData _Instance 
            = null;
        public static AppData Instance 
        {
            get
            {
                if (_Instance == null)
                    _Instance = new AppData();
                return _Instance;
            }
        }

        private Pipe _Pipe 
            = null;

        public Pipe Pipe
        {
            get { return _Pipe; }
            set { _Pipe = value; }
        }
    }
}
