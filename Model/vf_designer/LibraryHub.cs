using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Vapula.Model;

namespace Vapula.Designer
{
    public partial class LibraryHub
    {
        public enum State
        {
            Unknown = -1,
            Normal = 0,
            Disable = 1
        }

        private List<Library> _Libs
            = new List<Library>();

        public List<Library> Libs
        {
            get { return _Libs; }
        }

        public Library this[string id]
        {
            get
            {
                foreach (var lib in _Libs)
                    if (lib.Id == id)
                        return lib;
                return null;
            }
        }

        public bool Load(string path)
        {
            var xdoc = XDocument.Load(path);
            var xml_libs = xdoc.Element("root").Elements("library");
            foreach(var xml_lib in xml_libs)
            {
                string id = xml_lib.Attribute("id").Value;
                var lib = LoadLibrary(id);

                if (lib != null) 
                {
                    var state = (State)int.Parse(
                        xml_lib.Attribute("state").Value);
                    lib.Attach["state"] = state;
                }
            }
            return true;
        }

        public void Clear()
        {
            foreach (var lib in _Libs)
                lib.Clear();
            _Libs.Clear();
        }

        public Library LoadLibrary(string id)
        {
            var path = Path.Combine(
                AppData.Instance.PathLibrary,
                id + "\\" + id + ".library");
            var lib = Library.Load(path);
            if (lib != null)
                _Libs.Add(lib);
            return lib;
        }

        public bool FreeLibrary(string id)
        {
            return false;
        }

        public void Enable(string id)
        {
        }

        public void Disable(string id)
        {
        }
    }
}
