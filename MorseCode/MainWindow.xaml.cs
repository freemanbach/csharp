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

namespace MorseCode {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e) {
            txtEnglish.Focus();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }

        private void Clear_Click(object sender, RoutedEventArgs e) {
            txtEnglish.Text = string.Empty;
            txtMorsecode.Text = string.Empty;
            txtEnglish.Focus();
        }

        private void MC2E_Click(object sender, RoutedEventArgs e) {
            var mc2c = new Dictionary<string, string>() {
                {".-", "A"}, {"-...","B"}, {"-.-.","C"}, {"-..", "D"}, {".", "E"}, {"..-.", "F" },
                {"--.", "G"}, {"....","H"}, {"..","I"}, {".---","J" }, {"-.-","K" }, {".-..","L"},
                {"--", "M"}, {"-.", "N" }, {"---", "O"}, {".--." , "P"}, {"--.-", "Q" }, {".-.", "R" },
                {"...", "S"}, {"-", "T" }, {"..-", "U" },{"...-", "V" }, {".--" , "W" }, {"-..-", "X" },
                {"-.--", "Y"}, {"--..", "Z" }, {"-----" , "0" }, {".----", "1" }, {"..---", "2" }, 
                {"...--", "3" },{"....-", "4"}, {".....", "5" }, {"-....", "6" },{"--...", "7" }, 
                {"---..", "8" }, {"----.", "9" },{"--..--",","}, {".-.-.-","." }, {"..--..","?" }, 
                {"-..-.","/" }, {"-....-","-" }, {"-.--.","(" }, {"-.--.-",")" }, {"|"," " }, {" ",""}
                };
            string mess = txtEnglish.Text.ToUpper().Trim();
            string ans = "";
            // .... . .-.. .-.. --- | -.- .. -.. ... 
            string[] tmp = mess.Split(' ');
            if (mc2c.ContainsKey(tmp[0].ToString())) {
                for (int a=0; a <tmp.Length; a++) {
                    ans += mc2c[tmp[a]];
                }
                txtMorsecode.Text = ans;
            } else {
                MessageBox.Show("Doesnt contain keys !");
                Environment.Exit(0);
            }
        }

        private void E2MC_Click(object sender, RoutedEventArgs e) {
            var c2mc = new Dictionary<string, string>() {
                {"A", ".-" }, {"B","-..."}, {"C","-.-."}, {"D","-.."}, {"E","."}, {"F","..-." },
                {"G","--."}, {"H","...."}, {"I",".."}, {"J",".---" }, {"K","-.-" }, {"L",".-.."},
                {"M","--"}, {"N","-." }, {"O","---"}, {"P",".--." }, {"Q","--.-" }, {"R",".-." },
                {"S","..."}, {"T","-" }, {"U","..-" },{"V","...-" }, {"W",".--" }, {"X","-..-" },
                {"Y","-.--"}, {"Z","--.." }, {"0","-----"  }, {"1",".----" }, {"2","..---" },
                {"3", "...--" },{"4","....-"}, {"5","....." }, {"6","-...." },{"7","--..." },
                {"8","---.." }, {"9","----." }, {",","--..--"}, {".",".-.-.-" },
                {"?","..--.." }, {"/","-..-." }, {"-","-....-" }, {"(","-.--." },
                {")","-.--.-"},{" ","|"}, {""," "}
                };

            string mess = txtEnglish.Text.ToUpper().Trim();
            string ans = "";
            if (c2mc.ContainsKey(mess[0].ToString())) {
                foreach (char i in mess) {
                    ans += c2mc[i.ToString()] + " ";
                }
                txtMorsecode.Text = ans;
            } else {
                MessageBox.Show("Doesnt contain keys !");
                Environment.Exit(0);
            }

        }
    }
}
