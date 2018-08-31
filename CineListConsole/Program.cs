using CineListHelper.Factory;
using Ical.Net.Serialization;
using System;
using System.IO;

namespace CineListConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var clHelper = new CineListHelper.CineListHelper();
            clHelper.OnTrivialError += (sender, message) =>
            {
                Console.WriteLine(message);
            };

            clHelper.OnInformationalMessage += (sender, message) =>
            {
                Console.WriteLine("INFO: " + message);
            };
            var cFactory = new CalendarFactory();
            try
            {
                var cinemaListings = clHelper.GetLocalMovies("G731JN", 10).Result;
                var cal = cFactory.Convert(cinemaListings);
                
                //cal.TimeZones.Add(new Ical.Net.CalendarComponents.VTimeZone("Europe/London"));
                //cal.TimeZones.Add(new Ical.Net.CalendarComponents.VTimeZone("GMT"));
                //cal.Name = "CinemaList Calendar";
                //cal.Version = "4.0";
                if (cal != null)
                {
                    File.Delete("./calendar.ical");
                    var calSerializer = new CalendarSerializer();
                    var calString = calSerializer.SerializeToString(cal);
                    using (var writer = new StreamWriter("./calendar.ical"))
                    {
                        writer.Write(calString);
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
