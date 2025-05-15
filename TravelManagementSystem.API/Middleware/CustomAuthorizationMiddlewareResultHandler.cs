using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Text.Json;
using TravelManagementSystem.Application.Wrappers;

namespace TravelManagementSystem.API.Middleware
{
    public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Succeeded)
            {
                await next(context);
                return;
            }

            context.Response.StatusCode = authorizeResult.Forbidden
                ? (int)HttpStatusCode.Forbidden
                : (int)HttpStatusCode.Unauthorized;

            context.Response.ContentType = "application/json";

            var errorKey = authorizeResult.Forbidden ? "Forbidden" : "Unauthorized";
            var errorMessage = authorizeResult.Forbidden
                ? "Нямате достъп до този ресурс."
                : "Не сте влезли в системата.";

            var errors = new Dictionary<string, List<string>>
            {
                { errorKey, new List<string> { errorMessage } }
            };

            var response = ApiResponse<string>.FailureResponse(
                errors,
                "Достъпът е отказан"
            );

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });

            await context.Response.WriteAsync(json);
        }
    }
}
