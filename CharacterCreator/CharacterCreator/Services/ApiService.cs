using Newtonsoft.Json;

namespace CharacterCreator.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://www.dnd5eapi.co/api/";

        protected ApiService()
        {
            _client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
        }

        protected async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Exception caught: {e.Message}");
                return default(T);
            }
        }
    }
}