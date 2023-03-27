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
        ** Retrieve Mortgage Rates from the mortgage Rate Daily 
        ** returns mortage_rate[0] is 30 years Mortgage
        ** returns mortage_rate[1] is 15 years Mortgage
        **/
        public static List<string> getRates() {

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

        public static void Main(string [] args) {
            
            List <string> mortage_rate = new List<string>();
            List <string> mortage_items = new List<string>{"30 Years Mortgage Rate", "15 Years Mortgage Rate" };
            mortage_rate = getRates();
            Console.WriteLine("\n\n");
            for (int i = 0; i <mortage_rate.Count; i++) {
                Console.WriteLine($"{mortage_items[i]} {mortage_rate[i]}");
            }
        }
    }
}