using CineListHelper.Factory;
using CineListHelper.Models;
using CineListHelper.Models.Internal;
using CineListHelper.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CineListHelper
{
    public class CineListHelper
    {
        public event EventHandler<string> OnTrivialError;
        public event EventHandler<string> OnInformationalMessage;
        public CineListHelper(){}

        public async Task<IEnumerable<Cinema>> GetLocalCinemas(string postcode)
        {
            using (var client = new HttpClient())
            {
                var response = JsonConvert.DeserializeObject<CinemaResponse>(await client.GetStringAsync("http://api.cinelist.co.uk/search/cinemas/postcode/" + postcode));
                return response.cinemas;
            }
        }

        public async Task<IEnumerable<Listing>> GetMovies(Cinema cinema, int dayRange)
        {
            var lFactory = new ListingFactory();
            var movies = new List<Listing>();
            using (var client = new HttpClient())
            {
                for (var d = 0; d < dayRange; d++)
                {
                    string strResponse;
                    MoviesResponse movResponse = null;
                    try
                    {
                        InformationalMessage("cinema [" + cinema.id + "] day [" + d + "]");
                        strResponse = await client.GetStringAsync("http://api.cinelist.co.uk/get/times/cinema/" + cinema.id + "?day=" + d);
                        movResponse = JsonConvert.DeserializeObject<MoviesResponse>(strResponse);
                    }
                    catch(HttpRequestException e)
                    {
                        HandleTrivialError(e.Message);
                    }
                    if (movResponse != null)
                    {
                        movies.AddRange(lFactory.Convert(movResponse.listings, d));
                    }
                }
            }

            return movies;
        }

        public void HandleTrivialError(string Message)
        {
            OnTrivialError?.Invoke(this, Message);
        }

        public void InformationalMessage(string Message)
        {
            OnInformationalMessage?.Invoke(this, Message);
        }


        public async Task<Dictionary<Cinema, IEnumerable<Listing>>> GetLocalMovies(string postcode, int dayRange)
        {
            var movies = new Dictionary<Cinema,IEnumerable<Listing>>();
            foreach(var cinema in await GetLocalCinemas(postcode))
            {
                try
                {
                    movies.Add(cinema, await GetMovies(cinema, dayRange));
                }
                catch(Exception e)
                {
                    OnTrivialError?.Invoke(this,e.Message);
                }
                Thread.Sleep(500);
            }
            return movies;
        }
    }
}
