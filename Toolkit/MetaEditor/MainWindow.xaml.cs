using MahApps.Metro.Controls;
using Microsoft.Win32;
using Sartrey;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MetaEditor
{
    public partial class MainWindow : MetroWindow, IWindow
    {
        private bool _ShowAssetsToolbar 
            = false;

        public string Id
        {
            get { return "main"; }
        }

        public bool ShowAssetsToolbar 
        {
            get { return _ShowAssetsToolbar; }
            set 
            {
                AssetsToolbarRow.Height
                    = new GridLength(value ? 30 : 0);
                _ShowAssetsToolbar = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            InitLang();
            InitAssets();
            ShowAssetsToolbar = false;
        }

        private void InitLang() 
        {
            var lang = Runtime.Instance.LangPack;
            Title = lang["app_title"];
            BtnSave.Content = lang["main_btn_save"];
        }

        private void InitAssets() 
        {
            var runtime = Runtime.Instance;
            if (runtime.Library == null)
            {
                var node = new TreeViewItem();
                node.Header = "library";
                TrvAssets.Items.Add(node);
            }
            else 
            {
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            App.Current.Shutdown();
        }

        private void TrvAssets_Selected(object sender, RoutedEventArgs e) 
        {
            var tree = sender as TreeView;
            var node = tree.SelectedItem as TreeViewItem;
            if (node == null)
                return;
            ShowAssetsToolbar = true;
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "组件元数据|*.library|组件二进制文件|*.dll";
            dlg.CheckFileExists = true;
            if (!dlg.ShowDialog().Value)
                return;
            MessageBox.Show(dlg.FileName);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("abc");
        }
    }
}
