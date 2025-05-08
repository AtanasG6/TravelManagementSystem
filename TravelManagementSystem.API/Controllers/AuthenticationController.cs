using Microsoft.AspNetCore.Mvc;
using TravelManagementSystem.Application.DTOs.Authentication;
using TravelManagementSystem.Application.Services.Interfaces;
using TravelManagementSystem.Application.Wrappers;

namespace TravelManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                var token = await _authenticationService.RegisterAsync(registerUserDto);
                return Ok(ApiResponse<string>.SuccessResponse(
                    token,
                    $"Потребителят беше регистриран успешно. Добре дошли в системата за управление на пътувания!"
                ));
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return BadRequest(ApiResponse<string>.FailureResponse(
                    errors,
                    "Неуспешна регистрация. Моля, проверете въведените данни и опитайте отново."
                ));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                var token = await _authenticationService.LoginAsync(loginUserDto);
                return Ok(ApiResponse<string>.SuccessResponse(
                    token,
                    $"Успешно влизане в системата. Добре дошли отново!"
                ));
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return BadRequest(ApiResponse<string>.FailureResponse(
                    errors,
                    "Неуспешно влизане. Моля, проверете потребителското име и паролата и опитайте отново."
                ));
            }
        }
    }
}