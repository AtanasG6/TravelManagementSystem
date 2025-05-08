using TravelManagementSystem.Domain.Common;

namespace TravelManagementSystem.Domain.Entities
{
    public class Destination : AuditableEntity
    {
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Навигации
        public ICollection<Trip> Trips { get; set; }
    }
}
