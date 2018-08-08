using System;
using System.Collections.Generic;
using System.Text;

namespace CineListHelper.Models.Responses
{
    public class MoviesResponse
    {
        public string day { get; set; }
        public string status { get; set; }
        public IEnumerable<Movie> listings { get; set; }
    }
}
