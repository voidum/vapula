using MahApps.Metro.Controls;
using Microsoft.Win32;
using Sartrey;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            ShowAssetsToolbar = false;
            InitLang();
        }

        private void InitLang() 
        {
            var lang = Runtime.Instance.LangPack;
            Title = lang["app_title"];
            BtnSave.Content = lang["main_btn_save"];
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

        private void BtnNew_Click(object sender, RoutedEventArgs e) 
        {
            var node = new TreeViewItem();
            node.Header = "library";
            TrvAssets.Items.Add(node);
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

        private void BtnSave_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
                MessageBox.Show("save");
            else
                MessageBox.Show("save as");
        }
    }
}
