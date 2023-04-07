using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Policy;

namespace processCSV {
    internal class Program {

        /*
         * using httpClient lib forced method to be async with await keyword
         * https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-7.0
         */
        static async Task SaveData(string url, string fileName) {
            var hc = new HttpClient();

            using (var stream = await hc.GetStreamAsync(url)) {
                using (var fileStream = new FileStream(fileName, FileMode.CreateNew)) {
                    await stream.CopyToAsync(fileStream);
                }
            }
        }

        public static void parsedata(string fn) {
            List<string> mylist = new List<string>();

            // Read file using StreamReader. Reads file line by line
            using (StreamReader file = new StreamReader(fn)) {
                string mess;
                int cnt = 0;

                // Count the number of High-Maintenance Vehicles in DS
                while ((mess = file.ReadLine()) != null) {
                    string [] a = mess.Split(',');
                    if (a[0].ToLower() == "high") {
                        cnt++;
                    }
                }

                file.Close();
                Console.WriteLine("the number of vehicles with High Maintenance is: " + cnt.ToString());
            }
        }

        /*
         * Main has to be static async task Main 
         * instead of static async void Main
         * otherwise it wont work apparently.
         */
         static async Task Main(string[] args) {

            // File Location
            string url = "https://archive.ics.uci.edu/ml/machine-learning-databases/car/car.data";

            // using URI lib to get filepath
            Uri uri = new Uri(url);
            string fn = System.IO.Path.GetFileName(uri.LocalPath);

            // Download Data file to runtime location
            await SaveData( url, fn);

            // Pass Data File to Function
            // string mess1 = Environment.CurrentDirectory;
            string localpath = Directory.GetCurrentDirectory();
            localpath += "\\" + fn;
            parsedata(localpath);
        }
    }
}
