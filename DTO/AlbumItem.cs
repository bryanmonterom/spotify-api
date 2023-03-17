namespace spotify_api.DTO
{
    public class AlbumItem
    {
        public string Album_Type { get; set; }
        public int Total_Tracks { get; set; }
        public string Release_Date { get; set; }
        public Image[] Images { get; set; }
        public string Name { get; set; }



    }
}
