using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destination.Api.Infra.Data.Mappings.Database;

public class DestinationTagListMap : IEntityTypeConfiguration<DestinationTag>
{
    public void Configure(EntityTypeBuilder<DestinationTag> builder)
    {
        builder.ToTable("DestinationTagList");

        builder.HasKey(x => x.Id);

        builder.HasOne<DestinationTags>()
            .WithMany()
            .HasForeignKey(x => x.TagId);

        builder.HasOne<Domain.Aggregates.DestinationAggregate.Destination>()
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.DestinationId);
    }
}
