using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Infra.Data.Mappings.Database;

public class DestinationAverageSpendMap : IEntityTypeConfiguration<DestinationAverageSpend>
{
    public void Configure(EntityTypeBuilder<DestinationAverageSpend> builder)
    {
        builder.ToTable("DestinationAverageSpend");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Name")
            .IsRequired();

        builder.HasData(Enumeration.GetAll<DestinationAverageSpend>());
    }
}
