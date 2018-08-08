using CineListHelper.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CineListHelper
{
    public class CineListHelper
    {
        public CineListHelper(){ }

        public async Task<IEnumerable<Cinema>> GetLocalCinemas(string postcode)
        {
            using (var client = new HttpClient())
            {
                var response = JsonConvert.DeserializeObject<CinemaResponse>(await client.GetStringAsync("http://api.cinelist.co.uk/search/cinemas/postcode/" + postcode));
                return response.cinemas;
            }
        }

        public async Task<IEnumerable<Movie>> GetMovies(string cinemaId, int dayRange)
        {
            var movies = new List<Movie>();
            using (var client = new HttpClient())
            {
                for (var d = 0; d < dayRange; d++)
                {
                    string strResponse;
                    MoviesResponse movResponse;
                    strResponse = await client.GetStringAsync("http://api.cinelist.co.uk/get/times/cinema/" + cinemaId + "?day=" + d);
                    movResponse = JsonConvert.DeserializeObject<MoviesResponse>(strResponse);
                    movies.AddRange(movResponse.listings);
                    Thread.Sleep(1000);
                }
            }
            return movies;
        }

        public async Task<Dictionary<Cinema, IEnumerable<Movie>>> GetLocalMovies(string postcode, int dayRange)
        {
            var movies = new Dictionary<Cinema,IEnumerable<Movie>>();
            foreach(var cinema in await GetLocalCinemas(postcode))
            {
                movies.Add(cinema, await GetMovies(cinema.id, dayRange));
            }
            return movies;
        }
    }
}
