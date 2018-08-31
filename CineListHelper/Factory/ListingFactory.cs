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
            if (movie != null)
            {
                var listings = new List<Listing>();
                foreach (var t in movie.times)
                {
                    var hours = Int32.Parse(t.Split(':')[0]);
                    var minutes = Int32.Parse(t.Split(':')[1]);
                    DateTime time = DateTime.Today
                        .AddDays(offset)
                        .AddHours(hours)
                        .AddMinutes(minutes);
                    listings.Add(new Listing()
                    {
                        time = time,
                        title = movie.title
                    });
                }
                return listings;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Listing> Convert (IEnumerable<Movie> movies, int offset)
        {
            return movies.SelectMany(m => Convert(m, offset));
        }
    }
}
