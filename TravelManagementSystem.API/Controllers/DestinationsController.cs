using Microsoft.AspNetCore.Authorization;
using TravelManagementSystem.API.Controllers.Base;
using TravelManagementSystem.Application.DTOs.Destinations;
using TravelManagementSystem.Application.Parameters.Destinations;
using TravelManagementSystem.Application.Services.Interfaces;
using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.API.Controllers
{
    [Authorize]
    public class DestinationsController : GenericController<DestinationDto, CreateDestinationDto, UpdateDestinationDto, PatchDestinationDto, DestinationQueryParameters, Destination>
    {
        public DestinationsController(IGenericService<DestinationDto, CreateDestinationDto, UpdateDestinationDto, PatchDestinationDto, DestinationQueryParameters, Destination> service)
            : base(service)
        {
        }
    }
}
