using System.Collections.Generic;

namespace Vapula.Dispatcher
{
    public class Dispatcher
    {
        private List<Host> _Hosts 
            = new List<Host>();

        public bool Load(Target target) 
        {
            foreach (Host host in _Hosts)
            {
                if (host.Type == target.Type)
                {
                    bool ret = host.Load(target);
                    return ret;
                }
            }
            Host newhost = new Host(target.Type);
            return false;
        }
    }
}
