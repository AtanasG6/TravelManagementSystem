using AutoMapper;
using TravelManagementSystem.Application.DTOs.Authentication;
using TravelManagementSystem.Application.DTOs.Users;
using TravelManagementSystem.Application.Helpers;
using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.Application.Profiles.Users
{
    public class PasswordHashResolver : IValueResolver<object, User, string>
    {
        public string Resolve(object source, User destination, string destMember, ResolutionContext context)
        {
            string? plainPassword = null;

            switch (source)
            {
                case CreateUserDto createUser:
                    plainPassword = createUser.Password;
                    break;
                case UpdateUserDto updateUser:
                    plainPassword = updateUser.Password;
                    break;
                case PatchUserDto patchUser:
                    plainPassword = patchUser.Password;
                    break;
                case RegisterUserDto registerUser:
                    plainPassword = registerUser.Password;
                    break;
            }

            if (string.IsNullOrEmpty(plainPassword))
            {
                return destination.PasswordHash; // не променяй, ако няма нова парола
            }

            return PasswordHelper.HashPassword(plainPassword);
        }
    }
}
