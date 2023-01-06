using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizManager.Web.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> HttpGetAsync<T>(string uri) where T : class
        {
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            return await FromHttpResponseMessageAsync<T>(response);
        }

        public async Task<T> HttpPostAsync<T>(string uri, object model) where T : class
        {
            var content = CreateHttpContent(model);

            var response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            return await FromHttpResponseMessageAsync<T>(response);
        }

        private StringContent CreateHttpContent(object model)
        {
            var json = JsonSerializer.Serialize(model);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<T> FromHttpResponseMessageAsync<T>(HttpResponseMessage response) 
        {
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
