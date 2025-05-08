using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using TravelManagementSystem.Application.Wrappers;
using System.Text.Json;

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

            context.Response.StatusCode = authorizeResult.Forbidden ? (int)HttpStatusCode.Forbidden : (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<string>.FailureResponse(
                new List<string> { authorizeResult.Forbidden ? "Нямате достъп до този ресурс." : "Не сте влезли в системата." },
                "Достъпът е отказан"
            );

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}