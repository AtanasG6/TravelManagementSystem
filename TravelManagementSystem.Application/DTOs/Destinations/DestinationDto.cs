namespace TravelManagementSystem.Application.DTOs.Destinations
{
    public class DestinationDto
    {
        public int Id { get; set; }

        public string Country { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string CreatedBy { get; set; } = string.Empty; 

        public string? UpdatedBy { get; set; } 
    }
}
