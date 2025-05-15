using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using TravelManagementSystem.MVC.Services.Interfaces;

namespace TravelManagementSystem.MVC.Services.Implementations
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;

        public AiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetInterestingFactsAsync(string destinationName)
        {
            var prompt = $"Напиши ми 3 интересни факта за \"{destinationName}\" – всеки на нов ред с тире отпред. Използвай български език.";

            var body = new
            {
                model = "mistral",
                prompt,
                stream = false
            };

            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:11434/api/generate", content);
            var json = await response.Content.ReadAsStringAsync();

            var result = JObject.Parse(json);
            return result["response"]?.ToString() ?? "Няма отговор.";
        }
    }
}
