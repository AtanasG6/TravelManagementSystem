namespace TravelManagementSystem.Application.DTOs.Destinations
{
    public class PatchDestinationDto
    {
        public string? Country { get; set; }

        public string? City { get; set; }

        public string? Description { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
