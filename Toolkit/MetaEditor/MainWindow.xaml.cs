using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace MetaEditor
{
    public partial class MainWindow : MetroWindow
    {
        public Runtime Runtime
        {
            get
            {
                var app = (App.Current as App);
                return app.Runtime;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            var node = new TreeViewItem();
            node.Header = "123";
            TrvAssets.Items.Add(node);
        }

        private void TrvAssets_Selected(object sender, RoutedEventArgs e) 
        {
            var tree = sender as TreeView;
            var node = tree.SelectedItem as TreeViewItem;
            MessageBox.Show(node.Header.ToString());
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
            MessageBox.Show("save");
        }
    }
}
