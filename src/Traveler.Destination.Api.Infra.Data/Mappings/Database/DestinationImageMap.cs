using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destination.Api.Infra.Data.Mappings.Database;

public class DestinationImageMap : IEntityTypeConfiguration<DestinationImage>
{
    public void Configure(EntityTypeBuilder<DestinationImage> builder)
    {
        builder.ToTable("DestinationImages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EncodedImage)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("EncodedImage")
            .IsRequired();

        builder.Property(x => x.DestinationId)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("DestinationId")
            .IsRequired();
    }
}
