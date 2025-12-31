/*
 * 
 * Author        : freemanbach
 * email         : flo@radford.edu
 * Date          : 20251229
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
 * Pending items :
 *                 pull version info from python.org
 *                 tqdm tool to show downloading progress <- someone needs to re-write this for C#
 *                 CSharpTQDM is too old doesnt work with dotnet 8 and 10
 *                 
 * use case      : may work well in an University / College Setting for mass deployment
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InstallPython {
    class Program {

        // check whether there is internet connection
        // 200 is A-OK else Not OK
        public static async Task<string> chkConnection(string url) {
            string code = "";
            using (HttpClient client = new HttpClient()) {
                HttpResponseMessage resp = await client.GetAsync(url);
                resp.EnsureSuccessStatusCode();
                if (resp.IsSuccessStatusCode) {
                    code = resp.StatusCode.ToString();
                } 
                return code;
            }
        }

        // function to run the native python installer
        public static void executePythonInstaller(string fn) {

            string parts = fn.Split('-')[1], version="";
            string[] tmp = parts.Split('.');
            foreach (string s in tmp) {
                version += s;
            }

            // custom python params from my CMD scripts
            string param = $"/quiet /passive InstallAllUsers=0 TargetDir=C:\\Python{version} AssociateFiles=1 CompileAll=1 PrependPath=0 Shortcuts=0 Include_doc=1 Include_debug=0 Include_dev=1 Include_exe=1 Include_launcher=1 InstallLauncherAllUsers=1 Include_lib=1 Include_pip=1 Include_symbol=0 Include_tcltk=1 Include_test=1 Include_tools=1";

            // dotnet windows process control mechanism
            var startInfo = new ProcessStartInfo {
                    FileName = fn,
                    Arguments = param
                    // RedirectStandardInput = true,
                    // CreateNoWindow = true,
                    // UseShellExecute = false
            };

            var process = new Process {
                StartInfo = startInfo,
                EnableRaisingEvents = true
            };

            process.Exited += (sender, e) =>
            {
                Console.WriteLine("Process has exited.");
            };

            process.Start();
            Console.WriteLine("Waiting for process to exit...");
            process.WaitForExit();

        }

        // Example: https://www.code4it.dev/blog/download-and-save-files/
        public static async Task DownloadAndSave(string srcFile, string dstFolder, string dstFile) {
            Stream fs = await GetFileStream(srcFile);
            
            if ( fs != Stream.Null) {
                await SaveStream(fs, dstFolder, dstFile);
            }
        }

        // Streaming down the data from python.org
        public static async Task<Stream> GetFileStream(string url) {
            // getstreamasync is reading chunk by chunk
            // getasync is reading everything in the stream
            HttpClient httpClient = new HttpClient();
            Stream fs = await httpClient.GetStreamAsync(url);
            return fs;
        }

        // saving file to the Download location
        public static async Task SaveStream(Stream fs, string dstFolder, string dstFile) {
            if (!Directory.Exists(dstFolder))
                Directory.CreateDirectory(dstFolder);

            string path = Path.Combine(dstFolder, dstFile);

            using (FileStream outputFileStream = new FileStream(path, FileMode.CreateNew)) {
                await fs.CopyToAsync(outputFileStream);
            }
            Console.WriteLine("Download Completed.");
        }

        // Get MD5Sum 
        public static void md5Hash(string fn) {
            using (MD5 md5 = MD5.Create()) {
                byte[] inputBytes = Encoding.UTF8.GetBytes(fn);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++) {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                Console.WriteLine($"MD5 Sum:  {sb.ToString()} \n");
            }
        }
        public static async Task processByArch(string baseurl, string version, string atype, string path) {

            string[] tmp;
            string arch = "";
            
            if ( atype == "X86" ) {
                arch = ".exe";
            } else if ( atype == "X64" ) {
                arch = "-amd64.exe";
            } else if ( atype == "ARM64" ) {
                arch = "-arm64.exe";
            } else {
                // if it isnt the 3 from above, wdk what it could be
                Console.WriteLine("System Architecture is unknown !");
                Environment.Exit(0);
            }
            
            string url = baseurl + version + "/" + "python-" + version + arch;
            tmp = url.Split('/');
            string filename = tmp[tmp.Length - 1];
            Stream fs = await GetFileStream(url);
            await SaveStream(fs, path, filename);

            // installing Python
            if (File.Exists(filename)) {
                md5Hash(Path.Combine(path, filename));
                Thread.Sleep(2000);
                executePythonInstaller(filename);
            } else {
                Console.WriteLine("Something Wrong, File not Found !");
                Environment.Exit(1);
            }
        }

        // Displaying menu
        public static void menu(string latest, string older) {
            Console.WriteLine($"--------------------------------------------------");
            Console.WriteLine($"1) Python {latest} version                        ");
            Console.WriteLine($"2) Python {older} version                         ");
            Console.WriteLine($"3) Exit program                                   ");
            Console.WriteLine($"                                                  ");
            Console.WriteLine($"   Choose 1, 2, or 3                              ");
            Console.WriteLine($"--------------------------------------------------");
            Console.Write(">>> ");
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
            // python website looked like a beast to parse.
            // ///////////////////////////////////////////////////////////////////////////////////

            int choice = 0;
            string baseurl = "https://www.python.org/ftp/python/";

            // menu
            menu(latest, older);
            string temp = Console.ReadLine();
            bool ans = int.TryParse(temp, out choice);

            if ( !ans ) {
                Console.WriteLine("Invalid Selection !");
                Environment.Exit(1);
            } else {

                if (choice == 1) {

                    await processByArch(baseurl, latest, arch, path );

                } else if ( choice == 2) {
                
                    await processByArch(baseurl, older, arch, path);
                }
                else {
                    // exit program
                    Console.WriteLine($"Invalid Selection {choice} ");
                    Environment.Exit(0);
                }

            }
        }  
    }
}