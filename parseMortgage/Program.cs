using System;
using System.IO;
using System.Text;
using System.Xml;
using HtmlAgilityPack;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Linq;
// using System.Net.Http;
// using System.Threading;
// using System.Threading.Tasks;

// dotnet build
namespace parseMortgage {
    public class Program {
        /**
        ** Retrieve Mortgage News Daily Mort Rates 
        ** https://www.mortgagenewsdaily.com/mortgage-rates
        ** returns rate[0] is 30 years Mortgage
        ** returns rate[1] is 15 years Mortgage
        **/
        public static List<string> getNewsDailyRates() {

            List<string> rates = new List<string>();
            var tmp = "";
            string url = "https://www.mortgagenewsdaily.com/mortgage-rates";
            HtmlWeb web = new HtmlWeb();
            var html = web.Load(url);
            var items = html.DocumentNode.SelectNodes("//div[@class='rate']");

            foreach (var a in items){
                tmp = a.InnerHtml.ToString().Trim();
                tmp = tmp.Remove(tmp.Length - 1, 1);
                rates.Add(tmp);
            }
            rates.RemoveRange(2,rates.Count-2);

            return rates;
        }

        /**
        ** Retrieve Freddie Mac Mortgage Rates 
        ** https://www.freddiemac.com/index.html
        ** returns rate[0] is 30 years Mortgage
        ** returns rate[1] is 15 years Mortgage
        **/
        public static List<string> getFreddieMacRates() {

            List<string> rates = new List<string>();
            var tmp = "";
            string url = "https://www.freddiemac.com/index.html";
            HtmlWeb web = new HtmlWeb();
            var html = web.Load(url);
            var node = html.DocumentNode.SelectNodes("//div[contains(@class,'rate-percent')]");

            foreach (var a in node){
                tmp = a.InnerHtml.ToString().Trim();
                string [] line = tmp.Split('<');
                rates.Add(line[0]);
            }
            return rates;
        }

        /**
        ** Retrieve Bankrate Mortgage Rates 
        ** https://www.bankrate.com/mortgages/mortgage-rates/#mortgage-industry-insights
        ** returns rate[0] is 30 years Mortgage
        ** returns rate[1] is 15 years Mortgage
        **/
        public static List<string> getBankrateRates() {

            List<string> rates = new List<string>();
            var tmp = "";
            string url = "https://www.bankrate.com/mortgages/mortgage-rates/#mortgage-industry-insights";
            HtmlWeb web = new HtmlWeb();
            var html = web.Load(url);
            var node = html.DocumentNode.SelectNodes("//td[contains(@class,'series-percent')]");

            foreach (var a in node){
                tmp = a.InnerHtml.ToString().Trim();
                tmp = tmp.Remove(tmp.Length - 1);
                rates.Add(tmp);
            }
            rates.RemoveRange(2,rates.Count-2);

            return rates;
        }


        public static void Main(string [] args) {
            
            List <string> mortage_rate = new List<string>();
            List <string> mortage_items = new List<string>{"30 Years Mortgage Rate", "15 Years Mortgage Rate" };

            Console.WriteLine("\n");
            mortage_rate = getNewsDailyRates();
            Console.WriteLine("From News Daily Rate:\n");
            for (int i = 0; i <mortage_rate.Count; i++) {
                Console.WriteLine($"{mortage_items[i]} {mortage_rate[i]}%");
            }
            Console.WriteLine("\n");
            mortage_rate.Clear();
            
            mortage_rate = getFreddieMacRates();
            Console.WriteLine("\n");
            Console.WriteLine("From Freddie Mac\n");
            for (int i = 0; i <mortage_rate.Count; i++) {
                Console.WriteLine($"{mortage_items[i]} {mortage_rate[i]}%");
            }
            Console.WriteLine("\n");
            mortage_rate.Clear();

            mortage_rate = getBankrateRates();
            Console.WriteLine("\n");
            Console.WriteLine("From Bankrate\n");
            for (int i = 0; i <mortage_rate.Count; i++) {
                Console.WriteLine($"{mortage_items[i]} {mortage_rate[i]}%");
            }
            Console.WriteLine("\n");
            mortage_rate.Clear();
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
    }
}