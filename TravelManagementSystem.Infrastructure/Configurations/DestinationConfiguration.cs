using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelManagementSystem.Domain.Entities;

namespace TravelManagementSystem.Infrastructure.Configurations
{
    public class DestinationConfiguration : AuditableEntityConfiguration<Destination>
    {
        public override void Configure(EntityTypeBuilder<Destination> builder)
        {
            base.Configure(builder);

            builder.ToTable("Destinations");

            builder.Property(d => d.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(d => d.Latitude)
                .IsRequired();

            builder.Property(d => d.Longitude)
                .IsRequired();
        }
    }
}
