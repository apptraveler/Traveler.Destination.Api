using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Traveler.Destination.Api.Infra.Data.Mappings.Database;

public class DestinationMap : IEntityTypeConfiguration<Domain.Aggregates.DestinationAggregate.Destination>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.DestinationAggregate.Destination> builder)
    {
        builder.ToTable("Destinations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Name")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(800)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Description")
            .IsRequired();

        builder.HasOne(x => x.Temperature)
            .WithMany();

        builder.HasOne(x => x.AverageSpend)
            .WithMany();

        builder.HasMany(x => x.Images)
            .WithOne()
            .HasForeignKey(x => x.DestinationId);

        builder.HasMany(x => x.Tags)
            .WithOne()
            .HasForeignKey(x => x.DestinationId);

        builder.HasMany(x => x.Route)
            .WithOne();

        builder.Property(x => x.CreatedAt)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("CreatedAt")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UpdatedAt")
            .IsRequired();
    }
}
