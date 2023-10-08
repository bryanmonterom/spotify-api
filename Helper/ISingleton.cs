namespace spotify_api.Helper
{
    public interface ISingleton
    {
        Singleton GetInstance();
        string GetToken();
        void SetToken(string token);

    }
}
