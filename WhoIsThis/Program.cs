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

namespace WhoIsThis {
    class Program {

        /*
         * whois.denic.de wont take any requests on port 43, it is the reason why i cant get 
         * data pull from whois.denic.de and had gotten a weird ERROR message.
         * Must use this instead: https://webwhois.denic.de/?lang=en&query=zeit.de
         * 
         * Also, where to find a whois service strictly for IP v4 and v6 only for around the world ?
         * Another Solution would be to do a nslookup inside this code and then pull the dn from the result.
         */

        public static string procURL(string weburl) {
            string[] tmp  = weburl.Trim().Split('.');
            string tldn = tmp[tmp.Length - 1];
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
                description:"Default port is 43, specify other port if necessary.", 
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