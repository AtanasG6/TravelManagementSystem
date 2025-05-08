namespace TravelManagementSystem.Application.DTOs.Trips
{
    public class PatchTripDto
    {
        public string? Title { get; set; }

        public int? DestinationId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? Price { get; set; }
    }
}
