using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Infra.Data.Mappings.Database;

public class DestinationTemperatureMap : IEntityTypeConfiguration<DestinationTemperature>
{
    public void Configure(EntityTypeBuilder<DestinationTemperature> builder)
    {
        builder.ToTable("DestinationTemperatures");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Name")
            .IsRequired();

        builder.HasData(Enumeration.GetAll<DestinationTemperature>());
    }
}
