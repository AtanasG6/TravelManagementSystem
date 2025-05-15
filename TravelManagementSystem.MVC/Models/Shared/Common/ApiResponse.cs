namespace TravelManagementSystem.MVC.Models.Shared.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        public Dictionary<string, List<string>>? Errors { get; set; }

        public ApiResponse()
        {
            Success = false;
            Errors = new Dictionary<string, List<string>>();
        }

        public ApiResponse(bool success, string? message = null, T? data = default, Dictionary<string, List<string>>? errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors ?? new Dictionary<string, List<string>>();
        }
    }

}
