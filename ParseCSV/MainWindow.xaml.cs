using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

namespace ParseCSV {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private void OnLoad(object sender, RoutedEventArgs e) {
            txtBox1.Focus();
            loadData();
        }
        public void loadData() {
            lstBox1.Items.Add("One");
            lstBox1.Items.Add("Two");
            lstBox1.Items.Add("Three");
        }

        private void btnButton1_Click(object sender, RoutedEventArgs e) {
            txtBox1.Clear();
            txtBox1.Focus();
        }

        private void btnButton2_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }

        // https://begincodingnow.com/wpf-listbox-selection/
        // https://www.c-sharpcorner.com/UploadFile/mahesh/listbox-in-wpf/
        private void lstBox1_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            string abc = lstBox1.SelectedItem.ToString();
            MessageBox.Show(abc);
        }

        private void btnButton3_Click(object sender, RoutedEventArgs e) {
            // <TextBlock x:Name="tbxBox1" HorizontalAlignment="Left" Height="159" Margin="321,66,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="320"  />

            string fip = @"C:\Users\flo1\csharp\ParseCSV\files\car.data";
            // string fop = @"C:\Users\flo1\csharp\ParseCSV\files\test.txt";

            string line = "";
            // learn.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=net-7.0
            try {
                // Create an instance of StreamReader to read from a file.
                using (StreamReader sr = new StreamReader(fip)) {
                    // Read and display lines from the file until the end of file
                    while ((line = sr.ReadLine()) != null) {
                        // txtBox2.Text += line + "\n";
                        txtBox2.Text += line + "\n";
                    }
                }
            }
            catch (Exception err) {
                MessageBox.Show("The file could not be read:" + err.ToString());
            }
        }
    }
}
