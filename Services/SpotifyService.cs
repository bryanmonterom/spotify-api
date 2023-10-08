using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using spotify_api.DTO;
using spotify_api.Entities;
using spotify_api.Factories.IFactories;
using spotify_api.Helper;

namespace spotify_api.Services
{
    public class SpotifyService : IMusicService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientHelper httpClientHelper;
        private readonly IFactories factories;
        private readonly ISingleton singleton;
        private readonly string BASEURL = "https://api.spotify.com/v1/artists/";
        private readonly string tokenURL = "https://accounts.spotify.com/api/token";

        public SpotifyService()
        {
        }

        public SpotifyService(IConfiguration configuration, IHttpClientHelper httpClientHelper, IFactories factories, ISingleton singleton )
        {
            this.configuration = configuration;
            this.httpClientHelper = httpClientHelper;
            this.factories = factories;
            this.singleton = singleton;
        }

        public async Task<Artist> GetArtist(string id)
        {
            await GetToken();
            var result = await httpClientHelper.SendAysnc($"{BASEURL}{id}", singleton.GetToken()); ;
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
            var result = await httpClientHelper.SendAysnc(BuilAlbumsURI(id), singleton.GetToken()); ;
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

        private string BuilAlbumsURI(string id)
        {
            var query = new Dictionary<string, string>()
            {
                ["market"] = "US"
            };

            string baseURL = $"{BASEURL}{id}/albums";
            var uri = QueryHelpers.AddQueryString(baseURL, query);
            return uri;
        }

        public async Task GetToken()
        {
            var result = await httpClientHelper.PostAsync( tokenURL, factories.GetAuthentication(configuration).GetBody()); ;
            if (!string.IsNullOrEmpty(result))
            {
                var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(result);
                if (authResponse != null)
                {
                    singleton.SetToken(authResponse.Access_Token);
                }
            }
        }
    }
}
