namespace TravelManagementSystem.MVC.Models.Trip.ViewModels
{
    public class TripViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int DestinationId { get; set; }
        public string DestinationName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; }
    }

}
