using System;
using System.Linq;

namespace Calc {
    class Program {
        public static void Main(string[] args) {
            double ans = 0.00;
            string t = args[0];
            char symbol;
            int d1 =0, d2 = 0;

            if ( !t.Any(Char.IsLetter) == false) {
                Console.WriteLine("Invalid Characters!");
                Environment.Exit(0);
            }

            if (args.Length < 1 ) {
                Console.WriteLine("Insufficent arguments!");
                Environment.Exit(0);
            } else if (args.Length == 3) {
                symbol = Convert.ToChar(args[1].Trim());

                if (args[0].Trim() == "" || args[2].Trim() == "") {
                    Console.WriteLine("Insufficent arguments!");
                    Environment.Exit(0);
                }

                d1 = Convert.ToInt32(args[0].Split('.')[0]);
                d2 = Convert.ToInt32(args[2].Split('.')[0]);
                if (symbol == '+') { ans = Convert.ToDouble(args[0].Trim()) + Convert.ToDouble(args[2].Trim()); }
                if (symbol == '-') { ans = Convert.ToDouble(args[0].Trim()) - Convert.ToDouble(args[2].Trim()); }
                if (symbol == '*') { ans = Convert.ToDouble(args[0].Trim()) * Convert.ToDouble(args[2].Trim()); }
                if (symbol == '/') { ans = Convert.ToDouble(args[0].Trim()) / Convert.ToDouble(args[2].Trim()); }
                if (symbol == '%') { ans = d1 % d2; }
            } else {
                string[] data;
                if (t.Contains("+")) {
                    data = t.Split('+');
                    ans = Convert.ToDouble(data[0]) + Convert.ToDouble(data[1]);
                }
                else if (t.Contains("-")) {
                    data = t.Split('-');
                    ans = Convert.ToDouble(data[0]) - Convert.ToDouble(data[1]);
                }
                else if (t.Contains("/")) {
                    data = t.Split('/');
                    ans = Convert.ToDouble(data[0]) / Convert.ToDouble(data[1]);
                }
                else if (t.Contains("*")) {
                    data = t.Split('*');
                    ans = Convert.ToDouble(data[0]) * Convert.ToDouble(data[1]);
                } else if (t.Contains("%")) {
                    data = t.Split('%');
                    if (data[0] == "" || data[1] == "") {
                        Console.WriteLine("Insufficent arguments!");
                        Environment.Exit(1);
                    } else {
                        if (data[0].Contains('.') && data[1].Contains('.')) {
                            d1 = Convert.ToInt32(data[0].Split('.')[0]);
                            d2 = Convert.ToInt32(data[1].Split('.')[0]);
                            ans = d1 % d2;
                        } else if (data[0].Contains('.')) {
                            d1 = Convert.ToInt32(data[0].Split('.')[0]);
                            d2 = Convert.ToInt32(data[1]);
                            ans = d1 % d2;
                        } else if (data[1].Contains('.')) {
                            d1 = Convert.ToInt32(data[0]);
                            d2 = Convert.ToInt32(data[1].Split('.')[0]);
                            ans = d1 % d2;
                        } else {
                            d1 = Convert.ToInt32(data[0]);
                            d2 = Convert.ToInt32(data[1]);
                            ans = d1 % d2;
                        }
                    }
                }
            }
            Console.WriteLine($" {String.Format("{0:F5}",ans)}" );
        }
    }
}
