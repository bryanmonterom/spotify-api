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

        [HttpGet("/GetArtist/{id}")]
        public async Task<ActionResult> Get(string id= "2Hkut4rAAyrQxRdof7FVJq")
        {
            var artist = new Artist();
            artist = await spotifyService.GetArtist(id);
            if (artist is null) {

                return BadRequest();
            }
            return Ok(artist);
        }

        [HttpGet("/GetAlbums/{id}")]
        public async Task<ActionResult> GetAlbums(string id= "2Hkut4rAAyrQxRdof7FVJq")
        {
           var album = await spotifyService.GetArtistAlbums(id);
            if (album is null)
            {
                return BadRequest();
            }
            return Ok(album);
        }

    }
}