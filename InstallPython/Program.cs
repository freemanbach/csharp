/*
 * 
 * Author        : freemanbach
 * Date          : 20251224
 * desc          : a C# python installer
 * archecture    : ( X86, X64, Arm64 )
 * 
 * 3.14.2_x32    : https://www.python.org/ftp/python/3.14.2/python-3.14.2.exe
 * 3.14.2_x64    : https://www.python.org/ftp/python/3.14.2/python-3.14.2-amd64.exe
 * 3.14.2_arm64  : https://www.python.org/ftp/python/3.14.2/python-3.14.2-arm64.exe
 * 3.14.2_src    : https://www.python.org/ftp/python/3.13.2/Python-3.14.2.tgz
 *
 * 3.13.11_x32   : https://www.python.org/ftp/python/3.13.11/python-3.13.11.exe
 * 3.13.11_x64   : https://www.python.org/ftp/python/3.13.11/python-3.13.11-amd64.exe
 * 3.13.11_arm64 : https://www.python.org/ftp/python/3.13.11/python-3.13.11-arm64.exe
 * 3.13.11_src   : https://www.python.org/ftp/python/3.13.11/Python-3.13.11.tgz
 * 
 * args:   python-%major%.%minor%.%patch%-amd64.exe /quiet /passive InstallAllUsers=0 
 * TargetDir=C:\Python%major%%minor%%patch% AssociateFiles=1 CompileAll=1 PrependPath=0 Shortcuts=0 
 * Include_doc=1 Include_debug=0 Include_dev=1 Include_exe=1 Include_launcher=1 InstallLauncherAllUsers=1 
 * Include_lib=1 Include_pip=1 Include_symbol=0 Include_tcltk=1 Include_test=1 Include_tools=1
 * 
 */

using System;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace InstallPython {
    class Program {

        // check whether there is internet connection
        // 200 is A-OK else Not OK
        public static async Task<string> chkConnection(string url) {
            string code = "";
            using (HttpClient client = new HttpClient()) {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode) {
                    code = response.StatusCode.ToString();
                } 
                return code;
            }
        }

        public static async Task DownloadAndSave(string srcFile, string dstFolder, string dstFile) {
            Stream fs = await GetFileStream(srcFile);

            if ( fs != Stream.Null) {
                await SaveStream(fs, dstFolder, dstFile);
            }
        }

        public static async Task<Stream> GetFileStream(string url) {
            HttpClient httpClient = new HttpClient();
            Stream fs = await httpClient.GetStreamAsync(url);
            return fs;
        }

        public static async Task SaveStream(Stream fs, string dstFolder, string dstFile) {
            if (!Directory.Exists(dstFolder))
                Directory.CreateDirectory(dstFolder);

            string path = Path.Combine(dstFolder, dstFile);

            using (FileStream outputFileStream = new FileStream(path, FileMode.CreateNew)) {
                await fs.CopyToAsync(outputFileStream);
            }
            Console.WriteLine("Completed.");
        }

        public static async Task Main(string[] args) {

            // the two latest versions of Python
            string latest = "3.14.2";
            string older = "3.13.11";

            // user variables
            string userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Directory.SetCurrentDirectory($"{userHome}/Downloads");
            string path = Directory.GetCurrentDirectory();
            string arch = $"{RuntimeInformation.OSArchitecture}";

            // ///////////////////////////////////////////////////////////////////////////////////
            // python website looked like a beast to parse
            // just the two most recent versions of supported python minus alpha and beta releases
            // ///////////////////////////////////////////////////////////////////////////////////

            int choice = 0;
            string[] tmp;
            string url = "", baseurl= "https://www.python.org/ftp/python/", filename = "";

            Console.WriteLine($"--------------------------------------------------");
            Console.WriteLine($"1) Python {latest} version                        ");
            Console.WriteLine($"2) Python {older} version                         ");
            Console.WriteLine($"3) Exit program                                   ");
            Console.WriteLine($"                                                  ");
            Console.WriteLine($"   Choose 1, 2, or 3                              ");
            Console.WriteLine($"--------------------------------------------------");
            Console.Write(">>> ");
            string temp = Console.ReadLine();
            bool ans = int.TryParse(temp, out choice);

            if ( !ans ) {
                Console.WriteLine("Invalid Selection !");
                Environment.Exit(1);
            } else {

                if (choice == 1) {
                    // latest version
                    if (arch == "X86") {
                        url = baseurl + latest + "/" + "python-" + latest + ".exe";
                        tmp = url.Split('/');
                        filename = tmp[tmp.Length - 1];
                        Stream fs = (Stream)await GetFileStream(url);
                        await SaveStream(fs, path, filename);
                    } else if (arch == "X64") {
                        url = baseurl + latest + "/" + "python-" + latest + "-amd64.exe";
                        tmp = url.Split('/');
                        filename = tmp[tmp.Length - 1];
                        Stream fs = (Stream)await GetFileStream(url);
                        await SaveStream(fs, path, filename);
                    } else if (arch == "ARM64") {
                        url = baseurl + latest + "/" + "python-" + latest + "-amd64.exe";
                        tmp = url.Split('/');
                        filename = tmp[tmp.Length - 1];
                        Stream fs = (Stream)await GetFileStream(url);
                        await SaveStream(fs, path, filename);
                    }
                } else if ( choice == 2 ) {
                    // older version
                    if (arch == "X86") {
                        url = baseurl + older + "/" + "python-" + older + ".exe";
                        tmp = url.Split('/');
                        filename = tmp[tmp.Length - 1];
                        Stream fs = (Stream)await GetFileStream(url);
                        await SaveStream(fs, path, filename);
                    } else if (arch == "X64") {
                        url = baseurl + older + "/" + "python-" + older + "-amd64.exe";
                        tmp = url.Split('/');
                        filename = tmp[tmp.Length - 1];
                        Stream fs = (Stream)await GetFileStream(url);
                        await SaveStream(fs, path, filename);
                    } else if (arch == "ARM64") {
                        url = baseurl + older + "/" + "python-" + older + "-amd64.exe";
                        tmp = url.Split('/');
                        filename = tmp[tmp.Length - 1];
                        Stream fs = (Stream)await GetFileStream(url);
                        await SaveStream(fs, path, filename);
                    }
                } else {
                    // exit program
                    Environment.Exit(0);
                }

            }
        }  
    }
}