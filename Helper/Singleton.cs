namespace spotify_api.Helper
{
    public sealed class Singleton
    {
        private static Singleton _instance;



        public string token;

        public Singleton()
        {
        }


        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

    }
}
