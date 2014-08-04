using MahApps.Metro.Controls;
using Sartrey;
using System.ComponentModel;

namespace MetaEditor
{
    public partial class StartWindow : MetroWindow, IWindow
    {
        public string Id
        {
            get { return "start"; }
        }

        public StartWindow()
        {
            InitializeComponent();
            InitLang();
        }

        private void InitLang() 
        {
            var lang = Runtime.Instance.LangPack;
            Title = lang["app_title"];
            TxtStart.Text = lang["start_text"];
            BtnNew.Content = lang["start_btn_new"];
            BtnOpen.Content = lang["start_btn_open"];
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            App.Current.Shutdown();
        }

        private void BtnNew_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var runtime = Runtime.Instance;
            runtime.Windows["main"].Show();
            Hide();
        }

        private void BtnOpen_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
