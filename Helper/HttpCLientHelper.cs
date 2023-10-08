using System.Net.Http;

namespace spotify_api.Helper
{
    public class HttpCLientHelper : IHttpClientHelper
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpCLientHelper(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<string> SendAysnc(HttpMethod method, string uri, string token)
        {
            using (var client = httpClientFactory.CreateClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = method;
                    request.RequestUri = new Uri(uri);
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var response = client.SendAsync(request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }
                    }
                    return null;
                }
            }
        }
    }
}