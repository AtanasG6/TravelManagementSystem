namespace TravelManagementSystem.Application.DTOs.Trips
{
    public class UpdateTripDto
    {
        public string Title { get; set; } = string.Empty;

        public int DestinationId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }
    }
}
