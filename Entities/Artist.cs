namespace spotify_api.Entities
{
    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImgURL { get; set; }
        public List<string> Generes { get; set; }
        public List<Album> Albums { get; set; }
    }
}
