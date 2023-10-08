using spotify_api.DTO;

namespace spotify_api.Factories.IFactories
{
    public interface IFactories
    {
        AuthenticationBody GetAuthentication(IConfiguration configuration);
        FormUrlEncodedContent GetFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> content);
    }
}
