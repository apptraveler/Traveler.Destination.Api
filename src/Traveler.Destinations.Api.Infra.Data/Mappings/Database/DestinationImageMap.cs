using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destinations.Api.Infra.Data.Mappings.Database;

public class DestinationImageMap : IEntityTypeConfiguration<DestinationImage>
{
    public void Configure(EntityTypeBuilder<DestinationImage> builder)
    {
        builder.ToTable("DestinationImages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Url)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Url")
            .IsRequired();

        builder.HasOne<Destination>()
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.DestinationId);
    }
}
