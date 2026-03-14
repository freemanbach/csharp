/*
 * 
 * Author         : freemanbach
 * email          : flo@radford.edu
 * Date           : 20260314
 * desc           : NASA Astronomy Picture of the Day
 * archecture     : You can compile it on your Platform ( X86, X64, Arm64 )
 * 
 * Package        : dotnet add package Newtonsoft.Json
 * Warning        : dotnet build --property:WarningLevel=0
 *
 */

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NasaAPOD {

    public class ApodDB {
        public string Copyright { get; set; }
        public string Date { get; set; }
        public string Explanation { get; set; }
        public string HDUrl { get; set; }
        public string Media_Type { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Service_Version { get; set; }
        public string Msg { get; set; }
        public int Code { get; set; }
    }
        class Program {

            public static async Task Main(string[] args) {

            // archive site https://apod.nasa.gov/apod/archivepix.html
            HttpClient httpClient = new HttpClient();
            string KEY = "";

            // it will return json format data
            string url = "https://api.nasa.gov/planetary/apod?api_key=";
            string dop = "&date=", month = "", day = "", year = "";

            url += KEY;

            HttpResponseMessage resp = await httpClient.GetAsync(url);
            resp.EnsureSuccessStatusCode();
            string respData = await resp.Content.ReadAsStringAsync();
            
            Console.WriteLine(respData);

            ApodDB apod = JsonConvert.DeserializeObject<ApodDB>(respData);

            if (apod.Code == 500) {
                Console.WriteLine("Something is wrong with server, we dont know ! \n");
                Console.WriteLine($"{apod.Msg}, Server Code {apod.Code} \n");
                Environment.Exit(0);
            }
        }
    }
}
