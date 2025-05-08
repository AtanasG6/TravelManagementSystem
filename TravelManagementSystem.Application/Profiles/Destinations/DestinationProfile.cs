using AutoMapper;
using TravelManagementSystem.Application.DTOs.Destinations;
using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.Application.Profiles.Destinations
{
    public class DestinationProfile : Profile
    {
        public DestinationProfile()
        {
            // Create -> Entity
            CreateMap<CreateDestinationDto, Destination>();

            // Update -> Entity
            CreateMap<UpdateDestinationDto, Destination>();

            // Patch -> Entity
            CreateMap<PatchDestinationDto, Destination>();

            // Entity -> DTO
            CreateMap<Destination, DestinationDto>()
                .ForMember(dest => dest.CreatedBy,
                           opt => opt.MapFrom(src => src.CreatedBy != null ? src.CreatedBy.Username : string.Empty))
                .ForMember(dest => dest.UpdatedBy,
                           opt => opt.MapFrom(src => src.UpdatedBy != null ? src.UpdatedBy.Username : null));

            CreateMap<Destination, PatchDestinationDto>();
        }
    }
}
