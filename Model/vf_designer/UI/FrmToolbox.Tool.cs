using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Irisecol;
using Vapula.Model;

namespace Vapula.Designer
{
    public partial class FrmToolbox
    {
        private void FormLayout_LoadAdvancedTools()
        {
            var lvg = new ListViewGroup("!expert", "专家工具");
            LsvTools.Groups.Add(lvg);
            LsvTools.SetGroupState(lvg,
                IricListView.GroupState.Normal |
                IricListView.GroupState.Collapsible);
            
            string path = Path.Combine(
                Application.StartupPath, "advtool.list");
            
            var xdoc = XDocument.Load(path);
            var xmls_tool = xdoc.Element("root").Elements("tool");
            foreach (var xe in xmls_tool)
            {
                string id = xe.Attribute("id").Value;
                string text = xe.Value;
                Image img_l = Image.FromFile(Path.Combine(AppResDir ,id + "_l.png"));
                Image img_s = Image.FromFile(Path.Combine(AppResDir, id + "_s.png"));
                _LargeIcons.Images.Add("!" + id, img_l);
                _SmallIcons.Images.Add("!" + id, img_s);

                var lvi = new ListViewItem(text, "!" + id, lvg);
                lvi.Tag = id;
                LsvTools.Items.Add(lvi);
            }
        }

        private void FormLayout_LoadLibraries()
        {
            var hub = App.LibraryHub;
            var libs = hub.Libs;
            foreach (var lib in libs)
            {
                string lvg_header =
                    (lib.Name == "" ? "（" + lib.Id + "）" : lib.Name);
                var lvg = new ListViewGroup(lib.Id, lvg_header);
                LsvTools.Groups.Add(lvg);
                LsvTools.SetGroupState(lvg,
                    IricListView.GroupState.Normal |
                    IricListView.GroupState.Collapsible);

                foreach (var func in lib.Functions)
                {
                    int idx = func.Id - 1;
                    string path_pre = Path.Combine(
                        App.PathResource,
                        func.Library.Id + "." + func.Id.ToString());
                    string path1 = path_pre + "_l.png";
                    string path2 = path_pre + "_s.png";
                    var icon1 = File.Exists(path1) ? Image.FromFile(path1) : null;
                    var icon2 = File.Exists(path2) ? Image.FromFile(path2) : null;
                    func.Attach["LargeIcons"] = icon1;
                    func.Attach["SmallIcons"] = icon1;
                    string lvi_text =
                        (func.Name == "" ? "（" + func.Id + "）" : func.Name);
                    string icon_key = "!process";
                    if (func.Attach["LargeIcons"] != null ||
                        func.Attach["SmallIcons"] != null)
                    {
                        icon_key = lib.Id + ":" + func.Id.ToString();
                        _LargeIcons.Images.Add(icon_key, (Image)func.Attach["LargeIcons"]);
                        _SmallIcons.Images.Add(icon_key, (Image)func.Attach["SmallIcons"]);
                    }

                    var lvi = new ListViewItem(lvi_text, icon_key, lvg);
                    lvi.Tag = func;

                    LsvTools.Items.Add(lvi);
                }
            }
        }

        public bool IsAdvancedTool(ListViewItem lvi)
        {
            return !(lvi.Tag is Function);
        }
    }
}
