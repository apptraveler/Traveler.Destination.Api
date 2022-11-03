using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destination.Api.Infra.Data.Mappings.Database;

public class RouteCoordinatesMap : IEntityTypeConfiguration<RouteCoordinates>
{
    public void Configure(EntityTypeBuilder<RouteCoordinates> builder)
    {
        builder.ToTable("RouteCoordinates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Latitude)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Latitude")
            .IsRequired();

        builder.Property(x => x.Longitude)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Longitude")
            .IsRequired();

        builder.HasOne<Domain.Aggregates.DestinationAggregate.Destination>()
            .WithMany(x => x.Route)
            .HasForeignKey(x => x.DestinationId);
    }
}
