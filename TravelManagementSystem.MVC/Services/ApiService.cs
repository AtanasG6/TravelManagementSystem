using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TravelManagementSystem.MVC.Models.Shared.Common;
using TravelManagementSystem.MVC.Models.User.ViewModels;

namespace TravelManagementSystem.MVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _apiBase;

        public ApiService(HttpClient httpClient, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;

            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7012";
            _apiBase = $"{baseUrl.TrimEnd('/')}/api";
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginViewModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiBase}/authentication/login", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ApiResponse<string>>(responseContent);

            if (response.IsSuccessStatusCode && result?.Success == true && !string.IsNullOrWhiteSpace(result.Data))
            {
                _contextAccessor.HttpContext?.Session.SetString("JWToken", result.Data);
            }

            return result ?? new ApiResponse<string> { Success = false, Message = "Неуспешен отговор от сървъра." };
        }

        public async Task<ApiResponse<string>> RegisterAsync(RegisterViewModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiBase}/authentication/register", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ApiResponse<string>>(responseContent);

            if (response.IsSuccessStatusCode && result?.Success == true && !string.IsNullOrWhiteSpace(result.Data))
            {
                _contextAccessor.HttpContext?.Session.SetString("JWToken", result.Data);
            }

            return result ?? new ApiResponse<string> { Success = false, Message = "Грешка при регистрация." };
        }

        public void Logout()
        {
            _contextAccessor.HttpContext?.Session.Remove("JWToken");
        }

        public string? GetToken()
        {
            return _contextAccessor.HttpContext?.Session.GetString("JWToken");
        }

        private void AddJwtToken()
        {
            var token = GetToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
        {
            AddJwtToken();

            var response = await _httpClient.GetAsync($"{_apiBase}/{endpoint}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<T>>(content)
                   ?? new ApiResponse<T> { Success = false, Message = "Невалиден отговор." };
        }

        public async Task<PagedResponse<T>> GetPagedAsync<T>(string endpoint)
        {
            AddJwtToken();

            var response = await _httpClient.GetAsync($"{_apiBase}/{endpoint}");
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<PagedResponse<T>>(content)
                       ?? throw new Exception("Празен отговор от сървъра.");
            }
            catch (JsonException ex)
            {
                throw new Exception($"Грешка при JSON парсване: {ex.Message}\nСъдържание: {content}");
            }
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data)
        {
            AddJwtToken();

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiBase}/{endpoint}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<T>>(responseContent)
                   ?? new ApiResponse<T> { Success = false, Message = "Невалиден отговор от сървъра." };
        }

        public async Task<ApiResponse<TResponse>> PutAsync<TResponse>(string endpoint, object data)
        {
            AddJwtToken();

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBase}/{endpoint}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<TResponse>>(responseContent)
                   ?? new ApiResponse<TResponse> { Success = false, Message = "Невалиден отговор от сървъра." };
        }

        public async Task<ApiResponse<TResponse>> PatchAsync<TResponse>(string endpoint, object patchDocument)
        {
            AddJwtToken();

            var json = JsonConvert.SerializeObject(patchDocument);
            var content = new StringContent(json, Encoding.UTF8, "application/json-patch+json");

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_apiBase}/{endpoint}")
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<TResponse>>(responseContent)
                   ?? new ApiResponse<TResponse> { Success = false, Message = "Невалиден отговор от сървъра." };
        }

        public async Task<ApiResponse<T>> DeleteAsync<T>(string endpoint)
        {
            AddJwtToken();

            var response = await _httpClient.DeleteAsync($"{_apiBase}/{endpoint}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApiResponse<T>>(content)
                   ?? new ApiResponse<T> { Success = false, Message = "Невалиден отговор от сървъра." };
        }
    }
}
