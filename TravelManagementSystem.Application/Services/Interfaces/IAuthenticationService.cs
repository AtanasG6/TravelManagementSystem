using TravelManagementSystem.Application.DTOs.Authentication;

namespace TravelManagementSystem.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(RegisterUserDto registerUserDto);

        Task<string> LoginAsync(LoginUserDto loginUserDto);
    }
}
