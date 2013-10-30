using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace TCM.Model.Designer
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
                foreach (Library lib in _Libraries)
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
            foreach (Library lib in _Libraries)
            {
                foreach (Function func in lib.Functions)
                {
                    var tags = (Dictionary<string, object>)func.Tag;
                    if (tags.ContainsKey("LargeIcon"))
                        ((Image)tags["LargeIcon"]).Dispose();
                    if (tags.ContainsKey("SmallIcon"))
                        ((Image)tags["SmallIcon"]).Dispose();
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
                id + ".tcm.xml"));
            if (lib != null)
            {
                _Libraries.Add(lib);
                lib.Tag = state;
                foreach (Function func in lib.Functions)
                {
                    string path_pre = Path.Combine(
                        AppData.Instance.PathResource,
                        id + "." + func.Id.ToString());
                    Dictionary<string, object> tags 
                        = new Dictionary<string, object>();
                    string path1 = path_pre + ".tcm.png";
                    string path2 = path_pre + "_s.tcm.png";
                    if (File.Exists(path1) && File.Exists(path2)) 
                    {
                        Image icon1 = Image.FromFile(path1);
                        Image icon2 = Image.FromFile(path2);
                        tags.Add("LargeIcon", icon1);
                        tags.Add("SmallIcon", icon2);
                    }
                    func.Tag = tags;
                }
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
