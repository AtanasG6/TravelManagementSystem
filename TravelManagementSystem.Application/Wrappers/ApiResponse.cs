namespace TravelManagementSystem.Application.Wrappers
{
    using Newtonsoft.Json;

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

        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> FailureResponse(Dictionary<string, List<string>> errors, string? message = null)
        {
            return new ApiResponse<T>(false, message, default, errors);
        }

        public static ApiResponse<T> FailureResponse(string errorMessage, string? message = null)
        {
            var errors = new Dictionary<string, List<string>>
        {
            { "General", new List<string> { errorMessage } }
        };
            return new ApiResponse<T>(false, message, default, errors);
        }
    }

}