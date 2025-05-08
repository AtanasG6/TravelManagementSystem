using AutoMapper;
using TravelManagementSystem.Application.DTOs.Authentication;
using TravelManagementSystem.Application.DTOs.Users;
using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.Application.Profiles.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash,
                           opt => opt.MapFrom<PasswordHashResolver>());

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.PasswordHash,
                           opt => opt.MapFrom<PasswordHashResolver>());

            CreateMap<PatchUserDto, User>()
                .ForMember(dest => dest.PasswordHash,
                           opt => opt.MapFrom<PasswordHashResolver>());

            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.PasswordHash,
                           opt => opt.MapFrom<PasswordHashResolver>());

            CreateMap<User, UserDto>();

            CreateMap<User, PatchUserDto>();
        }
    }
}
