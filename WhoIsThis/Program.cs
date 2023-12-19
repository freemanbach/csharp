using System;
using System.IO;
using System.Text;
using System.Linq;
using System.CommandLine;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.CommandLine.Invocation;
using System.CommandLine.Builder;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
// dotnet build
namespace WhoIsThis {
    class Program {

        public static string procURL(string weburl) {
            string? tldn = "";
            string[] tmp  = weburl.Trim().Split('.');
            tldn = tmp[tmp.Length - 1];
            return tldn;
        }

        public static string procDomainName(string weburl) {
            string[] tmp = weburl.Trim().Split(".");
            string dn = tmp[tmp.Length - 2] + '.' + tmp[tmp.Length - 1];
            return dn;
        }

        static async Task<int> Main(string[] args) {

            var domainOption = new Option<string>
                (name:"--domainname", 
                description: "Provide the domain name of the website you are trying to check.",
                getDefaultValue:()=>"yahoo.com") { IsRequired = true }; ;
            domainOption.AddAlias("-d");

            var portOption = new Option<int>
                (name:"--port", 
                description:"Defalt port is 43, specify other port if necessary.", 
                getDefaultValue: () => 43);
            portOption.AddAlias("-p");

            var typeOption = new Option<string>
                (name:"--type", 
                description: "Types: domain",
                getDefaultValue:() => "domain");
            typeOption.AddAlias("-t");

            var rootCmd = new RootCommand("WhoIsthis whois domain Query CLI");

            rootCmd.AddOption(domainOption);
            rootCmd.AddOption(portOption);
            rootCmd.AddOption(typeOption);

            rootCmd.SetHandler((domainOptionValue, portOptionValue, typeOptionValue) => {
                retrieveData(domainOptionValue, portOptionValue, typeOptionValue);
            }, domainOption, portOption, typeOption);

            return await rootCmd.InvokeAsync(args);
        }//main

         internal static void retrieveData(string dv, int pv, string tv) {

            List<string> data = new List<string>();
            string? content = "";
            string tldn = procURL(dv);
            string domainname = procDomainName(dv);

            // this return value could be blank
            string ws = ConvertData.getURL(tldn);

            if (ws == null || ws == "") {
                Console.WriteLine();
                Console.WriteLine("Missing whois server.");
                Environment.Exit(0);
            }
            else {
                TcpClient whoisthis = new TcpClient();
                whoisthis.Connect(ws, pv);
                string dquery = tv + " " + domainname + "\r\n";
                byte[] buffer = Encoding.ASCII.GetBytes(dquery.ToCharArray());

                Stream whoisthisstream = whoisthis.GetStream();
                whoisthisstream.Write(buffer, 0, buffer.Length);

                StreamReader whoisthisstreamreader = new StreamReader(whoisthis.GetStream(), Encoding.ASCII);

                while (null != (content = whoisthisstreamreader.ReadLine())) {
                    data.Add(content);
                }
                whoisthis.Close();

                foreach (string a in data) {
                    Console.WriteLine(a);
                }
            }
        } // end internal

    }// class program
} // namespace