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
                            var objDeserializeObject = JsonConvert.DeserializeObject<Artist>(result);
                            if (objDeserializeObject != null)
                            {
                                return objDeserializeObject;
                            }
                        }

                    }

                }
                //Hay que corregir esto
                return null;


            }

        }

        public async Task<IEnumerable<Album>> GetArtistAlbums(string artistId)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResponse> GetToken()
        {
            var authenticationBody = new AuthenticationBody(configuration);

            using (var client = new HttpClient())
            {
                string baseURL = "https://accounts.spotify.com/api/token";
              

                using (var content = new FormUrlEncodedContent(authenticationBody.Body())) {

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
    }
}
