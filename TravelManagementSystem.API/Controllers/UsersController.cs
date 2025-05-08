using Microsoft.AspNetCore.Authorization;
using TravelManagementSystem.API.Controllers.Base;
using TravelManagementSystem.Application.DTOs.Users;
using TravelManagementSystem.Application.Parameters.Users;
using TravelManagementSystem.Application.Services.Interfaces;
using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : GenericController<UserDto, CreateUserDto, UpdateUserDto, PatchUserDto, UserQueryParameters, User>
    {
        public UsersController(IGenericService<UserDto, CreateUserDto, UpdateUserDto, PatchUserDto, UserQueryParameters, User> service)
            : base(service)
        {
        }
    }
}
