using CineListHelper.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineListHelper.Models.Internal
{
    /// <summary>
    /// Just a Movie with a datetime instead of a string
    /// </summary>
    public class Listing
    {
        public DateTime time;
        public string title;
    }
}
