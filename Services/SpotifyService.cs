using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using spotify_api.DTO;
using spotify_api.Entities;
using System.Net.Mime;
using System.Text;

namespace spotify_api.Services
{
    public class SpotifyService : IMusicService
    {
        private readonly IConfiguration configuration;

        public SpotifyService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<Artist> GetArtist(string id)
        {
            using (var client = new HttpClient())
            {
                string baseURL = $"https://api.spotify.com/v1/artists/{id}";
                var token = await GetToken();

                if (token is  null) {

                    return null;
                }

                using (var request = new HttpRequestMessage()) { 
                    request.Method = HttpMethod.Get;
                    request.RequestUri= new Uri(baseURL);
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token.Access_Token);

                    var response = client.SendAsync(request).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(result))
                        {
                            var artist = JsonConvert.DeserializeObject<Artist>(result);
                            if (artist != null)
                            {
                                var albums = await GetArtistAlbums(id);
                                artist.Albums = albums;
                                //artist.Albums.Items = albums.Items.OrderBy(a=> a.Release_Date).ToArray();
                                return artist;
                            }
                        }

                    }

                }
                //Hay que corregir esto
                return null;


            }

        }

        //public async Task<IEnumerable<Album>> GetArtistAlbums(string id)
        public async Task<Album> GetArtistAlbums(string id)
        {
            using (var client = new HttpClient())
            {
                var query = new Dictionary<string, string>() {

                    ["market"]="US"
                };

                string baseURL = $"https://api.spotify.com/v1/artists/{id}/albums";
                var uri = QueryHelpers.AddQueryString(baseURL, query);

                var token = await GetToken();

                if (token is null)
                {
                    return null;
                }

                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri(uri);
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Access_Token);

                    var response = client.SendAsync(request).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(result))
                        {
                            var albums = JsonConvert.DeserializeObject<Album>(result);
                            if (albums != null)
                            {
                                foreach (var item in albums.Items)
                                {
                                    item.SetReleaseDate();
                                }
                                return albums;
                            }
                        }

                    }

                }
                //Hay que corregir esto
                return null;


            }
        }

        public async Task<AuthenticationResponse> GetToken()
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
                                var objDeserializeObject = JsonConvert.DeserializeObject<AuthenticationResponse>(result);
                                if (objDeserializeObject != null)
                                {
                                    return objDeserializeObject;
                                }
                            }
                        }
                    }
                    return null;
                }
            }
            catch (Exception e)
            {

                throw;
            }

           
        }
    }
}
