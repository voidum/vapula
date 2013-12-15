using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Linq;
using Vapula.Model;

namespace Vapula.Designer
{
    public enum LibraryState
    {
        Unknown = -1,
        Normal = 0,
        Disable = 1
    }

    public class LibraryManager
    {
        private List<Library> _Libraries
            = new List<Library>();
        public List<Library> Libraries
        {
            get { return _Libraries; }
        }

        public Library this[string id]
        {
            get
            {
                foreach (var lib in _Libraries)
                    if (lib.Id == id)
                        return lib;
                return null;
            }
        }

        public bool Load(string path)
        {
            XDocument xdoc = XDocument.Load(path);
            var xml_libs = xdoc.Element("root").Elements("library");
            foreach(XElement xml_lib in xml_libs)
            {
                string id = xml_lib.Attribute("id").Value;
                LibraryState state = (LibraryState)int.Parse(
                    xml_lib.Attribute("state").Value);
                Mount(id);
            }
            return true;
        }

        public void Clear()
        {
            foreach (var lib in _Libraries)
            {
                foreach (var func in lib.Functions)
                {
                    if (func.Tag["LargeIcon"] != null)
                        ((Image)func.Tag["LargeIcon"]).Dispose();
                    if (func.Tag["SmallIcon"] != null)
                        ((Image)func.Tag["SmallIcon"]).Dispose();
                }
                lib.Clear();
            }
            _Libraries.Clear();
        }

        public bool Mount(string id, LibraryState state = LibraryState.Normal)
        {
            Library lib = Library.Load(
                Path.Combine(
                AppData.Instance.PathLibrary,
                id + "\\" + id + ".tcm.xml"));
            if (lib != null)
            {
                _Libraries.Add(lib);
                lib.Tag["State"] = state;
            }
            return true;
        }

        public bool Unmount(string id)
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
