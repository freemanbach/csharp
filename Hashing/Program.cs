
/**
** Author      : flo
** email       : flo@radford.edu
** Description : provide 256, 384 and 512 sha hashes on a file or files
**             : on the command prompt in a windows/mac/linux.
** Additional
** Package     : dotnet add package BouncyCastle.Cryptography --version 2.2.1
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
        
        // Generates SHA 1
        public static List<string> gensha1hash(List<string> myfiles) {
            List <string> csumList = new List<string>();

            foreach (string file in myfiles) {
                using (FileStream stream = File.OpenRead(file)){
                    SHA1 sha1 = SHA1.Create();
                    byte[] hashvalue = sha1.ComputeHash(stream);
                    csumList.Add(BitConverter.ToString(hashvalue).Replace("-", String.Empty));
                }
            }
            return csumList;
        }

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
        
        public static List<string> chooseAlgo(List<string> myfiles, string shatype) {
            List <string> csumList = new List<string>();

            if (shatype.ToLower().Trim() == "sha1" || shatype.ToLower().Trim() == "sha") {
                csumList = gensha1hash(myfiles);
                return csumList;

            } else if (shatype.ToLower().Trim() == "sha256" || shatype.ToLower().Trim() == "sha2") {
                csumList = gen256hash(myfiles);
                return csumList;

            } else if (shatype.ToLower().Trim() == "sha384" || shatype.ToLower().Trim() == "sha3") {
                csumList = gen384hash(myfiles);
                return csumList;

            } else if (shatype.ToLower().Trim() == "sha512" || shatype.ToLower().Trim() == "sha5") {
                csumList = gen512hash(myfiles);
                return csumList;

            } else {
                Console.WriteLine("No other Sha Algorithm options, exiting....");
                Environment.Exit(0);
            }

            return csumList;
        }

        /******************************************************************
        * allow an user to choose the algorithm from the following algorithms
        * 
        * Use        : Hashing file1 file2... HashAlgorithm
        * Algorithms : sha1 sha256 sha384 sha512
        * Example    : Hashing resume.docx sha256
        ******************************************************************/
        public static void Main(string [] args) {
            string type = "";
            List <string> files = new List<string>();
            List <string> data = new List<string>();

            if (args.Length == 0) {
                Console.WriteLine("Enter in atleast one file on CMD Prompt.");
            } else {
                // Some Forbidden Magick
                type = args[args.Length - 1];
                List<string> tfiles = new List<string>(args);
                tfiles.RemoveAt(tfiles.Count - 1);

                foreach (string f in tfiles) {
                    if (File.Exists(f)) {
                        files.Add(f);
                    } else {
                        Console.WriteLine("One of the Files doesnt exist.");
                        Environment.Exit(0);
                    }
                }
                data = chooseAlgo(files, type);
                //data = genhash(files);
                for(int a=0; a<data.Count; a++) {
                    Console.WriteLine("*{0}  {1}", data[a], files[a]);
                }
                
            }
        }
    }
}