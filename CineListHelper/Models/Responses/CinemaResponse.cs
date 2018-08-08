using System;
using System.Collections.Generic;
using System.Text;

namespace CineListHelper.Models.Responses
{
    public class CinemaResponse
    {
        public string postcode { get; set; }
        public IEnumerable<Cinema> cinemas { get; set; }
    }
}
