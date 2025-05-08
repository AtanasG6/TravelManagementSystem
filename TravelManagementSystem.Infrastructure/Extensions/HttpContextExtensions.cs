using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TravelManagementSystem.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static int? GetUserId(this HttpContext httpContext)
        {
            var userIdClaim = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            return null;
        }

        public static bool IsAdmin(this HttpContext httpContext)
        {
            return httpContext?.User?.IsInRole("Admin") ?? false;
        }
    }
}
