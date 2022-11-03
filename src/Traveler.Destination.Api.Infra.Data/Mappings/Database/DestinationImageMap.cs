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

        builder.Property(x => x.Url)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Url")
            .IsRequired();

        builder.HasOne<Domain.Aggregates.DestinationAggregate.Destination>()
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.DestinationId);
    }
}
