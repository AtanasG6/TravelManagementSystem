using System.IdentityModel.Tokens.Jwt;

namespace TravelManagementSystem.MVC.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetUserRole(this HttpContext context)
        {
            var token = context.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type.EndsWith("role"))?.Value;
            return roleClaim;
        }

        public static bool IsAdmin(this HttpContext context)
        {
            return context.GetUserRole() == "Admin";
        }
    }
}
