/*
 * 
 * Author         : freemanbach
 * email          : flo@radford.edu
 * Date           : 20260312
 * archecture     : ( X86, X64, Arm64 )
 * desc           : a C# OMDB movie Data application
 * Package        : dotnet add package Newtonsoft.Json
 * Warning        : dotnet build --property:WarningLevel=0
 * 
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleCSMovieDB {

    public class Rating {
        public string Source { get; set; }
        public string Value { get; set; }
    }
    public class MovieDB {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }

        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public List<Rating> Ratings { get; set; }

        public string Metascore { get; set; }
        public string IMDBRating { get; set; }
        public string IMDBVotes{get; set;}
        public string IMDBID { get; set; }
        public string Type { get; set; }
        public string DVD { get; set; }
        public string BoxOffice { get; set; }
        public string Production { get; set; }
        public string Website { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }
    }
     class Program {

        public static async Task Main(string[] args) {

            HttpClient httpClient = new HttpClient();
            string APIKEY = "";
            string url = $"http://www.omdbapi.com/?apikey={APIKEY}&t=";
            string movie = "";

            Console.Write("Enter in a movie >>> ");
            movie = Console.ReadLine();
            url += movie;

            HttpResponseMessage response = await httpClient.GetAsync(url);
            // response.EnsureSuccessStatusCode();
            string respBody = await response.Content.ReadAsStringAsync();

            // result JSON format
            // JSON Error: {"Response":"False","Error":"Movie not found!"}
            // Console.WriteLine(respBody);
            MovieDB mdb = JsonConvert.DeserializeObject<MovieDB>(respBody);

            if ( mdb.Response == "False") {
                Console.WriteLine($"{mdb.Error}");
                Environment.Exit(0);
            }

            // testing String from JSON result Title
            Console.WriteLine(mdb.Title);

            // prnting out ratings
            foreach (var rating in mdb.Ratings) {
                Console.WriteLine($"Source: {rating.Source}, Value: {rating.Value}");
            }

        }
    }
}
