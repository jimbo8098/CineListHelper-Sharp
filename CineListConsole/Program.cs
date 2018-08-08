using System;

namespace CineListConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var clHelper = new CineListHelper.CineListHelper();
            try
            {
                var cinemaListings = clHelper.GetLocalMovies("G731JN", 1).Result;
                foreach(var cinemaListing in cinemaListings)
                {
                    Console.WriteLine(cinemaListing.Key.name);
                    foreach(var listing in cinemaListing.Value)
                    {
                        Console.WriteLine("\t" + listing.title);
                        /*foreach(var time in listing.times)
                        {
                            Console.WriteLine("\t\t" + time);
                        }*/
                    }
                }
            }
            catch(System.Net.Http.HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
