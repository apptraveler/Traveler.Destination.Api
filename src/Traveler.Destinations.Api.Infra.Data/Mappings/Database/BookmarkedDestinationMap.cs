using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destinations.Api.Domain.Aggregates.BookmarkedDestination;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destinations.Api.Infra.Data.Mappings.Database;

public class BookmarkedDestinationMap : IEntityTypeConfiguration<BookmarkedDestination>
{
    public void Configure(EntityTypeBuilder<BookmarkedDestination> builder)
    {
        builder.ToTable("BookmarkedDestinations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UserId")
            .IsRequired();

        builder.HasOne<Destination>()
            .WithMany()
            .HasForeignKey(x => x.DestinationId)
            .IsRequired();
    }
}
