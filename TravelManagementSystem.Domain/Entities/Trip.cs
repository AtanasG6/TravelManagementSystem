using TravelManagementSystem.Domain.Common;

namespace TravelManagementSystem.Domain.Entities
{
    public class Trip : AuditableEntity
    {
        public string Title { get; set; } = string.Empty;
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
