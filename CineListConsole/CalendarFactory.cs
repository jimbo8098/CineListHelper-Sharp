using CineListHelper.Models.Internal;
using CineListHelper.Models.Responses;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineListHelper.Factory
{
    public class CalendarFactory
    {
        public CalendarEvent Convert(Cinema cinema, Listing listing)
        {
            return new CalendarEvent()
            {
                Start = new CalDateTime(listing.time),
                End = new CalDateTime(listing.time.AddHours(1)),
                //Name = cinema.name + ": " + listing.title,
                Name = listing.title,
                Description = cinema.name + ": " + listing.title,
                Location = cinema.name
            };
        }

        public IEnumerable<CalendarEvent> Convert(KeyValuePair<Cinema,IEnumerable<Listing>> cinemaListings)
        {
            var events = new List<CalendarEvent>();
            foreach(var l in cinemaListings.Value)
            {
                events.Add(Convert(cinemaListings.Key, l));
            }
            return events;
        }

        public Calendar Convert(Dictionary<Cinema,IEnumerable<Listing>> cinemaListings)
        {
            var c = new Calendar();
            foreach(var cl in cinemaListings)
            {
                c.Events.AddRange(Convert(cl));
            }
            return c;
        }
    }
}
