using spotify_api.DTO;

namespace spotify_api.Factories
{
    public class Factories : IFactories.IFactories
    {
        public AuthenticationBody GetAuthentication(IConfiguration configuration)
        {
            return new AuthenticationBody(configuration);
        }

        public FormUrlEncodedContent GetFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> content)
        {
           return new FormUrlEncodedContent(content);
        }
    }
}
