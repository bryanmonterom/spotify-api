using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using spotify_api.DTO;
using spotify_api.Entities;
using spotify_api.Helper;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using static System.Net.WebRequestMethods;

namespace spotify_api.Services
{
    public class SpotifyService : IMusicService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientHelper httpClientHelper;
        private readonly IHttpClientFactory httpClientFactory;
        Singleton singleton = new Singleton();
        string BASEURL = "https://api.spotify.com/v1/artists/";

        public SpotifyService()
        {
        }

        public SpotifyService(IConfiguration configuration, IHttpClientHelper httpClientHelper, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientHelper = httpClientHelper;
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<Artist> GetArtist(string id)
        {
            await GetToken();
            var token = singleton.token;

            var result = await httpClientHelper.SendAysnc(HttpMethod.Get, $"{BASEURL}{id}", token); ;
            if (!string.IsNullOrEmpty(result))
            {
                var artist = JsonConvert.DeserializeObject<Artist>(result);
                if (artist != null)
                {
                    var albums = await GetArtistAlbums(id);
                    if (albums is null)
                    {
                        return null;
                    }
                    artist.Albums = albums;
                    return artist;
                }
            }
            return null;

        }

        public async Task<Album> GetArtistAlbums(string id)
        {
            var query = new Dictionary<string, string>()
            {
                ["market"] = "US"
            };

            string baseURL = $"{BASEURL}{id}/albums";
            var uri = QueryHelpers.AddQueryString(baseURL, query);

            var result = await httpClientHelper.SendAysnc(HttpMethod.Get, uri, singleton.token); ;
            if (!string.IsNullOrEmpty(result))
            {
                var albums = JsonConvert.DeserializeObject<Album>(result);
                if (albums != null)
                {
                    return albums;
                }
            }
            return null;
        }

        public async Task GetToken()
        {
            var authenticationBody = new AuthenticationBody(configuration);

            try
            {
                using (var client = new HttpClient())
                {
                    string baseURL = "https://accounts.spotify.com/api/token";

                    using (var content = new FormUrlEncodedContent(authenticationBody.Body()))
                    {
                        content.Headers.Clear();
                        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                        var response = client.PostAsync(baseURL, content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            if (!string.IsNullOrEmpty(result))
                            {
                                var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(result);
                                if (authResponse != null)
                                {
                                    singleton.token = authResponse.Access_Token;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
