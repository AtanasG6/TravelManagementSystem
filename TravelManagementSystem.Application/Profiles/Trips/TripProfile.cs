using AutoMapper;
using TravelManagementSystem.Application.DTOs.Trips;
using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.Application.Profiles.Trips
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            // Create -> Entity
            CreateMap<CreateTripDto, Trip>();

            // Update -> Entity
            CreateMap<UpdateTripDto, Trip>();

            // Patch -> Entity
            CreateMap<PatchTripDto, Trip>();

            // Entity -> DTO
            CreateMap<Trip, TripDto>()
                .ForMember(dest => dest.DestinationName,
                           opt => opt.MapFrom(src => src.Destination != null ? src.Destination.City : string.Empty))
                .ForMember(dest => dest.CreatedBy,
                           opt => opt.MapFrom(src => src.CreatedBy != null ? src.CreatedBy.Username : string.Empty))
                .ForMember(dest => dest.UpdatedBy,
                           opt => opt.MapFrom(src => src.UpdatedBy != null ? src.UpdatedBy.Username : null));

            CreateMap<Trip, PatchTripDto>();
        }
    }
}
