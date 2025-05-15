using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TravelManagementSystem.Application.DTOs.Authentication;
using TravelManagementSystem.Application.Helpers;
using TravelManagementSystem.Application.Services.Interfaces;
using TravelManagementSystem.Domain.Entities;
using TravelManagementSystem.Domain.Repositories.Interfaces;

namespace TravelManagementSystem.Infrastructure.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(RegisterUserDto registerUserDto)
        {
            if (await _userRepository.UsernameExistsAsync(registerUserDto.Username))
            {
                throw new ApplicationException("Потребителското име вече съществува.");
            }

            if (await _userRepository.EmailExistsAsync(registerUserDto.Email))
            {
                throw new ApplicationException("Имейлът вече съществува.");
            }

            var user = _mapper.Map<User>(registerUserDto);
            user.Role = "User";

            await _userRepository.AddUserAsync(user);

            return GenerateJwtToken(user);
        }

        public async Task<string> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _userRepository.FindByUsernameAsync(loginUserDto.Username);

            if (user == null)
            {
                throw new ApplicationException("Невалидно потребителско име или парола.");
            }

            // Проверка на паролата
            var inputPasswordHash = PasswordHelper.HashPassword(loginUserDto.Password);
            if (user.PasswordHash != inputPasswordHash)
            {
                throw new ApplicationException("Невалидно потребителско име или парола.");
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
