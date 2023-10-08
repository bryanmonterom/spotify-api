namespace spotify_api.Helper
{
    public interface IHttpClientHelper
    {
        Task<string> SendAysnc(HttpMethod method, string uri, string token);
    }
}
