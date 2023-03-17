using spotify_api.DTO;

namespace spotify_api.Entities
{
    public class Album
    {
        public string Name { get; set; }

        public AlbumItem[] Items { get; set; }
    }
}
