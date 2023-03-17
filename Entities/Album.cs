namespace spotify_api.Entities
{
    public class Album
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int TotalTracks { get; set; }
        public Artist Artist { get; set; }
    }
}
