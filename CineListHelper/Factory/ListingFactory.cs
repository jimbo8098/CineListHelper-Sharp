using CineListHelper.Models.Responses;
using CineListHelper.Models.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CineListHelper.Models;

namespace CineListHelper.Factory
{
    public class ListingFactory
    {
        public IEnumerable<Listing> Convert(Movie movie, int offset)
        {
            var listings = new List<Listing>();
            foreach (var t in movie.times)
            {
                var hours = Int32.Parse(t.Split(':')[0]);
                var minutes = Int32.Parse(t.Split(':')[1]);
                DateTime time = DateTime.Now;
                time.AddDays(offset);
                time.AddHours(hours);
                time.AddMinutes(minutes);
                listings.Add(new Listing()
                {
                    time = time,
                    title = movie.title
                });
            }
            return listings;
        }
    }
}
