using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCM.Net
{
    class View
    {
        private Template _Template;
        private List<Action<Model>> _Renders;
    }
}
