using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destination.Api.Infra.Data.Mappings.Database;

public class DestinationClimateAverageMap : IEntityTypeConfiguration<DestinationClimateAverage>
{
    public void Configure(EntityTypeBuilder<DestinationClimateAverage> builder)
    {
        builder.ToTable("DestinationClimateAverage");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Max)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Max")
            .IsRequired();

        builder.Property(x => x.Min)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Min")
            .IsRequired();

        builder.HasOne<ClimateStatus>()
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .IsRequired();

        builder.HasOne<Domain.Aggregates.DestinationAggregate.Destination>()
            .WithOne(x => x.ClimateAverage)
            .HasForeignKey<DestinationClimateAverage>(x => x.DestinationId);
    }
}
