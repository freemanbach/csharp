/*
 *
 * Author         : freemanbach
 * email          : flo@radford.edu
 * Date           : 20250101
 * desc           : a simple cmd line calculator
 *
 */

using System;
using System.Linq;

namespace Calc {
    class Program {

        public static Boolean chkData(string t1, string t2) {
           if (t2 == "" || t2 == "") {
                return true;
            } else {
                return false;
            }
        }
        public static double compute(string t) {

            int d1 = 0, d2 = 0;
            double ans = 0.00;
            string[] dt;

            if (!t.Any(Char.IsLetter) == false) {
                Console.WriteLine("Invalid Characters!");
                Environment.Exit(0);
            }
            if (t.Contains("+")) {
                dt = t.Split('+');
                if (chkData(dt[0], dt[1]) == true ) {
                    Console.WriteLine("Insufficent arguments!");
                    Environment.Exit(0);
                }
                ans = Convert.ToDouble(dt[0]) + Convert.ToDouble(dt[1]);
            } else if (t.Contains("-")) {
                dt = t.Split('-');
                if (chkData(dt[0], dt[1]) == true) {
                    Console.WriteLine("Insufficent arguments!");
                    Environment.Exit(0);
                }
                ans = Convert.ToDouble(dt[0]) - Convert.ToDouble(dt[1]);
            } else if (t.Contains("/")) {
                dt = t.Split('/');
                if (chkData(dt[0], dt[1]) == true) {
                    Console.WriteLine("Insufficent arguments!");
                    Environment.Exit(0);
                }
                ans = Convert.ToDouble(dt[0]) / Convert.ToDouble(dt[1]);
            } else if (t.Contains("**")) {
                string a = t.Substring(0, t.IndexOf('*'));
                string b = t.Substring(t.IndexOf('*')+2);
                if (chkData(a, b) == true) {
                    Console.WriteLine("Insufficent arguments!");
                    Environment.Exit(0);
                }
                ans = Math.Pow(Convert.ToDouble(a), Convert.ToDouble(b) );
            } else if (t.Contains("*")) {
                dt = t.Split('*');
                if (chkData(dt[0], dt[1]) == true) {
                    Console.WriteLine("Insufficent arguments!");
                    Environment.Exit(0);
                }
                ans = Convert.ToDouble(dt[0]) * Convert.ToDouble(dt[1]);
            } else if (t.Contains("%")) {
                dt = t.Split('%');
                if (chkData(dt[0], dt[1]) == true) {
                    Console.WriteLine("Insufficent arguments!");
                    Environment.Exit(0);
                } else {
                    if (dt[0].Contains('.') && dt[1].Contains('.')) {
                        d1 = Convert.ToInt32(dt[0].Split('.')[0]);
                        d2 = Convert.ToInt32(dt[1].Split('.')[0]);
                        ans = d1 % d2;
                    } else if (dt[0].Contains('.')) {
                        d1 = Convert.ToInt32(dt[0].Split('.')[0]);
                        d2 = Convert.ToInt32(dt[1]);
                        ans = d1 % d2;
                    } else if (dt[1].Contains('.')) {
                        d1 = Convert.ToInt32(dt[0]);
                        d2 = Convert.ToInt32(dt[1].Split('.')[0]);
                        ans = d1 % d2;
                    } else {
                        d1 = Convert.ToInt32(dt[0]);
                        d2 = Convert.ToInt32(dt[1]);
                        ans = d1 % d2;
                    }
                }
            }
            return ans;
        }
        public static void Main(string[] args) {

            double fans = 0.00; 
            string t = "";

            // menu
            if (args.Length <= 0) {
                Console.WriteLine();
                Console.WriteLine("Insufficient Arguments....          \n\n");
                Console.WriteLine("Ex:                                     ");
                Console.WriteLine("calc.exe  34+45.99                      ");
                Console.WriteLine("calc.exe  34-45.99                      ");
                Console.WriteLine("calc.exe  34*45.99                      ");
                Console.WriteLine("calc.exe  34/45.99                      ");
                Console.WriteLine("calc.exe  2**12                         ");
                Console.WriteLine("calc.exe  34%45                         ");
                Console.WriteLine();
                Environment.Exit(0);
            } else if (args.Length == 1) {

                string tmp = args[0];

                if (tmp.Contains('+') || tmp.Contains('-') || tmp.Contains('*') ||
                     tmp.Contains('/') || tmp.Contains('%') || tmp.Contains("**") ) {
                    fans = compute(args[0]);
                } else {
                    Console.WriteLine("Insufficient Arguments");
                    Environment.Exit(0);
                }
            } else {
                // combine them
                foreach (string tmp in args) {
                    t += tmp;
                }
                fans = compute(t);
            }
            Console.WriteLine($"{String.Format("{0:F5}", fans)}");
        }
    }
}
