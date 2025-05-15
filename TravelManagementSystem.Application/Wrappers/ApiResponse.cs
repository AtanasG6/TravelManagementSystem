namespace TravelManagementSystem.Application.Wrappers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        public List<string> Errors { get; set; }

        public ApiResponse()
        {
            Success = false;
            Errors = new List<string>();
        }

        public ApiResponse(bool success, string? message = null, T? data = default, List<string>? errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors ?? new List<string>();
        }

        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> FailureResponse(List<string> errors, string? message = null)
        {
            return new ApiResponse<T>(false, message, default, errors);
        }

        public static ApiResponse<T> FailureResponse(string error, string? message = null)
        {
            return new ApiResponse<T>(false, message, default, new List<string> { error });
        }
    }
}