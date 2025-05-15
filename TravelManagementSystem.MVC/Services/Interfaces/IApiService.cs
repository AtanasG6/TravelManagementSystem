using TravelManagementSystem.MVC.Models.Shared.Common;
using TravelManagementSystem.MVC.Models.User.ViewModels;

namespace TravelManagementSystem.MVC.Services.Interfaces
{
    public interface IApiService
    {
        Task<ApiResponse<string>> LoginAsync(LoginViewModel model);
        Task<ApiResponse<string>> RegisterAsync(RegisterViewModel model);
        void Logout();
        string? GetToken();
        Task<ApiResponse<T>> GetAsync<T>(string endpoint);
        Task<PagedResponse<T>> GetPagedAsync<T>(string endpoint);
        Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data);
        Task<ApiResponse<TResponse>> PutAsync<TResponse>(string endpoint, object data);
        Task<ApiResponse<TResponse>> PatchAsync<TResponse>(string endpoint, object patchDocument);
        Task<ApiResponse<T>> DeleteAsync<T>(string endpoint);
    }
}
