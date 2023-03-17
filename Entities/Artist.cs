using spotify_api.DTO;

namespace spotify_api.Entities
{
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Image[] Images { get; set; }
        public List<string> Genres { get; set; }
        public Album Albums { get; set; }
    }
}
