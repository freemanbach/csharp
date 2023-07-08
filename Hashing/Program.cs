
/**
** Author      : flo
** email       : flo@radford.edu
** Description : provide 256, 384 and 512 sha hashes on a file or files
**             : on the command prompt in a windows/mac/linux.
**/

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

        // Generates SHA 512
        public static List<string> gen512hash(List<string> myfiles) {
            List <string> csumList = new List<string>();

            foreach (string file in myfiles) {
                using (FileStream stream = File.OpenRead(file)){
                    SHA512 sha512 = SHA512.Create();
                    byte[] hashvalue = sha512.ComputeHash(stream);
                    csumList.Add(BitConverter.ToString(hashvalue).Replace("-", String.Empty));
                }
            }
            return csumList;
        }

        // Generates SHA 384
        public static List<string> gen384hash(List<string> myfiles) {
            List <string> csumList = new List<string>();

            foreach (string file in myfiles) {
                using (FileStream stream = File.OpenRead(file)){
                    SHA384 sha384 = SHA384.Create();
                    byte[] hashvalue = sha384.ComputeHash(stream);
                    csumList.Add(BitConverter.ToString(hashvalue).Replace("-", String.Empty));
                }
            }
            return csumList;
        }

        // Generates SHA 256
        public static List<string> gen256hash(List<string> myfiles) {
            List <string> csumList = new List<string>();

            foreach (string file in myfiles) {
                using (FileStream stream = File.OpenRead(file)){
                    SHA256 sha256 = SHA256.Create();
                    byte[] hashvalue = sha256.ComputeHash(stream);
                    csumList.Add(BitConverter.ToString(hashvalue).Replace("-", String.Empty));
                }
            }
            return csumList;
        }

        // Defaults to SHA256
        public static List<string> genhash(List<string> myfiles) {
            List <string> csumList = new List<string>();

            foreach (string file in myfiles) {
                using (FileStream stream = File.OpenRead(file)){
                    SHA256 sha256 = SHA256.Create();
                    byte[] hashvalue = sha256.ComputeHash(stream);
                    csumList.Add(BitConverter.ToString(hashvalue).Replace("-", String.Empty));
                }
            }
            return csumList;
        }

        public static void Main(string [] args) {
            List <string> files = new List<string>();
            List <string> data = new List<string>();

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
                data = genhash(files);
                for(int a=0; a<data.Count; a++) {
                    Console.WriteLine("*{0}  {1}", data[a], files[a]);
                }
                
            }
        }
    }
}