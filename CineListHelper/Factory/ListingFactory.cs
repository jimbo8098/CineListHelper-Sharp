using CineListHelper.Models.Responses;
using CineListHelper.Models.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CineListHelper.Factory
{
    public class ListingFactory
    {
        public Listing Convert(Movie movie, int offset)
        {
            var listing = new Listing();
            listing.title = movie.title;
            var times = new List<DateTime>();
            foreach(var t in movie.times)
            {
                var hours = Int32.Parse(t.Split(':')[0]);
                var minutes = Int32.Parse(t.Split(':')[1]);
                DateTime time = DateTime.Now;
                time.AddDays(offset);
                time.AddHours(hours);
                time.AddMinutes(minutes);
                times.Add(time);
            }
            listing.times = times;
            return listing;
        }
    }
}
