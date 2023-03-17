using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using spotify_api.DTO;
using spotify_api.Entities;
using spotify_api.Services;

namespace spotify_api.Controllers
{
    [ApiController]
    [Route("api/artist")]
    public class ArtistController : ControllerBase
    {
        private readonly IMusicService spotifyService;
        private readonly IConfiguration configuration;

        public ArtistController(IMusicService spotifyService, IConfiguration configuration)
        {
            this.spotifyService = spotifyService;
            this.configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<Artist> Get(string id)
        {
            var artist = new Artist();
            var AuthenticationBody = new AuthenticationBody(configuration);
            artist = await spotifyService.GetArtist(id);
            return artist;
        }
        
    }
}