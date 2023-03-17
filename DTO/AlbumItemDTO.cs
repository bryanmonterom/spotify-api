namespace spotify_api.DTO
{
    public class AlbumItemDTO : AlbumItem
    {
        public string BackgroundColor { get;  set;} 

        public void DefineColor()
        {

            var date = DateTime.Parse(Release_Date);
            if (date.Year % 3 == 0 && date.Year % 5 == 0)
            {
                BackgroundColor= "#eeffee";
            }
            else if (date.Year % 3 == 0)
            {
                BackgroundColor ="#ffeeee";
            }
            else if (date.Year % 5 == 0)
            {
                BackgroundColor= "#eeeeff";
            }
        }
    }
}
