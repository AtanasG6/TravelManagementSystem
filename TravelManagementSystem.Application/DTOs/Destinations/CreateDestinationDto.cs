namespace TravelManagementSystem.Application.DTOs.Destinations
{
    public class CreateDestinationDto
    {
        public string Country { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
