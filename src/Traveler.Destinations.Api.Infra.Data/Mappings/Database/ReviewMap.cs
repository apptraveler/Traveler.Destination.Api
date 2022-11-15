using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destinations.Api.Domain.Aggregates.ReviewAggregate;

namespace Traveler.Destinations.Api.Infra.Data.Mappings.Database;

public class ReviewMap : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Message)
            .HasMaxLength(3000)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Message")
            .IsRequired(false);

        builder.OwnsOne(x => x.Rate, x =>
        {
            x.Property(c => c.Value)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RateValue")
                .IsRequired();

            x.WithOwner();
        });

        builder.Property(x => x.UserId)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UserId")
            .IsRequired();

        builder.Property(x => x.ReviewerName)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ReviewerName")
            .IsRequired();

        builder.HasOne<Destination>()
            .WithMany()
            .HasForeignKey(x => x.DestinationId)
            .IsRequired();
    }
}
