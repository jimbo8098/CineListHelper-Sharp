using System;
using System.Collections.Generic;
using System.Text;

namespace CineListHelper.Models.Responses
{
    public class Movie
    {
        public IEnumerable<string> times { get; set; }
        public string title { get; set; }
    }
}
