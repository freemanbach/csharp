
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Security.Cryptography;
// using System.Net.Http;
// using System.Threading;
// using System.Threading.Tasks;

namespace Hashing {

    public class Program {

        public static void Main(string [] args) {
            List <string> files = new List<string>();

            if (args.Length == 0) {
                Console.WriteLine("Enter in atleast one file on CMD Prompt.");
            } else {
                foreach (string f in args) {
                    if (File.Exists(f)) {
                        files.Add(f);
                    } else {
                        Console.WriteLine("One of the Files doesnt exist.");
                        Environment.Exit(0);
                    }
                }
            }

            foreach (string f in files) {
                Console.WriteLine(f);
            }
        }
    }
}