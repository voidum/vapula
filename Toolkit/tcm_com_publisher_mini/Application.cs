using TCM.Model;

namespace TCM.ComPubMini
{
    public class AppData
    {
        private static Component _Component = null;
        public static Component Component
        {
            get { return _Component; }
            set
            {
                if (_Component != null && _Component != value) _Component.Clear();
                _Component = value;
            }
        }

        private static string _PathCfg = "";
        public static string PathCfg
        {
            get { return _PathCfg; }
            set { _PathCfg = value; }
        }
    }
}
