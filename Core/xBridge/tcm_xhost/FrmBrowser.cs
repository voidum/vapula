using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using TCM.Helper;

namespace TCM.xHost
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisibleAttribute(true)]
    public partial class FrmBrowser : Form
    {
        private AppData app = AppData.Instance;

        public FrmBrowser()
        {
            InitializeComponent();
            if (browser.Version.Major < 9)
            {
                MessageBox.Show("IE version is too low", "TCM xHost");
                Application.Exit();
            }
            FormLayout_LoadHostConfig();
        }

        private void FormLayout_LoadHostConfig()
        {
            XmlReaderSettings xrs = new XmlReaderSettings();
            xrs.ValidationType = ValidationType.Schema;
            xrs.Schemas.Add(null,
                Path.Combine(IOHelper.AppDir, "xhost.xsd"));
            XmlReader xr = null;
            XDocument xml = null;
            try
            {
                string path = Path.Combine(app.PathUI, "global.xml");
                xr = XmlReader.Create(path, xrs);
                xml = XDocument.Load(xr);
                while (xr.Read()) { }
            }
            catch (Exception ex)
            {
                Base.Logger.WriteLog(LogType.Error, ex.Message);
            }
            finally
            {
                xr.Close();
            }
            if (xml != null)
            {
                XElement xeroot = xml.Element("host");
                XElement xesize = xeroot.Element("size");
                Width = int.Parse(xesize.Element("width").Value);
                Height = int.Parse(xesize.Element("height").Value);
                bool lock_size = (xesize.Attribute("lock").Value == "true");
                if(lock_size)
                {
                    MaximumSize = MinimumSize = Size;
                    MaximizeBox = false;
                }
                XElement xestart = xeroot.Element("start");
                StartPosition = (FormStartPosition)(int.Parse(xestart.Element("at").Value));
            }
        }

        private void FrmBrowser_Load(object sender, EventArgs e)
        {
            browser.Navigate(app.PathUIIndex);
        }
    }
}
