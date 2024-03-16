using Newtonsoft.Json;

namespace CharacterCreator.Services
{
    public class ApiService
    {
        protected HttpClient _client;
        protected string BaseUrl = "https://www.dnd5eapi.co/api/";

        public ApiService()
        {
            _client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
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