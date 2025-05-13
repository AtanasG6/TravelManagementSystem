using Microsoft.AspNetCore.Authorization;
using TravelManagementSystem.API.Controllers.Base;
using TravelManagementSystem.Application.DTOs.Trips;
using TravelManagementSystem.Application.Parameters.Trips;
using TravelManagementSystem.Application.Services.Interfaces;
using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.API.Controllers
{
    [Authorize]
    public class TripsController : GenericController<TripDto, CreateTripDto, UpdateTripDto, PatchTripDto, TripQueryParameters, Trip>
    {
        public TripsController(IGenericService<TripDto, CreateTripDto, UpdateTripDto, PatchTripDto, TripQueryParameters, Trip> service)
            : base(service)
        {
        }
    }
}
