using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Infra.Data.Mappings.Database;

public class DestinationTagsMap : IEntityTypeConfiguration<DestinationTags>
{
    public void Configure(EntityTypeBuilder<DestinationTags> builder)
    {
        builder.ToTable("DestinationTags");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Name")
            .IsRequired();

        builder.HasData(Enumeration.GetAll<DestinationTags>());
    }
}
