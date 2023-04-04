using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleStructure {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private void onLoad(object sender, RoutedEventArgs e) {
            TreeViewItem treeItem = null;

            // North America
            treeItem = new TreeViewItem();
            treeItem.Header = "Sample Countries in World";

            treeItem.Items.Add(new TreeViewItem() { Header = "germany" });
            treeItem.Items.Add(new TreeViewItem() { Header = "spain" });
            treeItem.Items.Add(new TreeViewItem() { Header = "australia" });

            tview.Items.Add(treeItem);
        }
    }
}
