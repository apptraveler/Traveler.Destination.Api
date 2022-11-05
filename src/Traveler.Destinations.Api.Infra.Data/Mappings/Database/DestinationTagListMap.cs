using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destinations.Api.Infra.Data.Mappings.Database;

public class DestinationTagListMap : IEntityTypeConfiguration<DestinationTag>
{
    public void Configure(EntityTypeBuilder<DestinationTag> builder)
    {
        builder.ToTable("DestinationTagList");

        builder.HasKey(x => x.Id);

        builder.HasOne<DestinationTags>()
            .WithMany()
            .HasForeignKey(x => x.TagId);

        builder.HasOne<Destination>()
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.DestinationId);
    }
}
