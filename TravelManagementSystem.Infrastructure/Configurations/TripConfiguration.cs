using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.Infrastructure.Configurations
{
    public class TripConfiguration : AuditableEntityConfiguration<Trip>
    {
        public override void Configure(EntityTypeBuilder<Trip> builder)
        {
            base.Configure(builder);

            builder.ToTable("Trips");

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.StartDate)
                .IsRequired();

            builder.Property(t => t.EndDate)
                .IsRequired();

            builder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(t => t.Destination)
                .WithMany(d => d.Trips)
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
