using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public int? UpdatedById { get; set; }
        public User? UpdatedBy { get; set; }
    }
}
