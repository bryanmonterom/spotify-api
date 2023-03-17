using spotify_api.DTO;
using spotify_api.Entities;

namespace spotify_api.Services
{
    public interface IMusicService
    {
        public Task<AuthenticationResponse> GetToken();
        public Task<Artist> GetArtist(string id);
        public Task<Album> GetArtistAlbums(string artistId);
    }
}
