using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Vapula.Model;

namespace Vapula.Designer
{
    public partial class FrmToolbox
    {
        private void FormLayout_LoadAdvancedTools()
        {
            ListViewGroup lvg = new ListViewGroup("!expert", "专家工具");
            LsvTools.Groups.Add(lvg);
            LsvTools.SetGroupState(lvg,
                ListViewGroupState.Normal |
                ListViewGroupState.Collapsible);
            
            string path = Path.Combine(
                Application.StartupPath, "advtool.list");
            
            XDocument xdoc = XDocument.Load(path);
            foreach (XElement xe in xdoc.Element("root").Elements("tool"))
            {
                string id = xe.Attribute("id").Value;
                string text = xe.Value;
                Image img_l = Image.FromFile(Path.Combine(AppResDir ,id + ".png"));
                Image img_s = Image.FromFile(Path.Combine(AppResDir, id + "_s.png"));
                _LargeIcons.Images.Add("!" + id, img_l);
                _SmallIcons.Images.Add("!" + id, img_s);

                ListViewItem lvi = new ListViewItem(text, "!" + id, lvg);
                lvi.Tag = id;
                LsvTools.Items.Add(lvi);
            }
        }

        public bool IsAdvancedTool(ListViewItem lvi)
        {
            return !(lvi.Tag is Function);
        }
    }
}
