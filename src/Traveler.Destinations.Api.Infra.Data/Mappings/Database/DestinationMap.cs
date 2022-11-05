using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destinations.Api.Infra.Data.Mappings.Database;

public class DestinationMap : IEntityTypeConfiguration<Destination>
{
    public void Configure(EntityTypeBuilder<Destination> builder)
    {
        builder.ToTable("Destinations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Name")
            .IsRequired();

        builder.Property(x => x.City)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("City")
            .IsRequired();

        builder.Property(x => x.Country)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Country")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(800)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Description")
            .IsRequired();

        builder.Property("_averageSpendId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("AverageSpendId")
            .IsRequired();

        builder.HasOne(x => x.ClimateAverage)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey<DestinationClimateAverage>(x => x.DestinationId);

        builder.HasOne(x => x.AverageSpend)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey("_averageSpendId");

        builder.HasMany(x => x.Images)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(x => x.DestinationId);

        builder.HasMany(x => x.Tags)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.DestinationId);

        builder.HasMany(x => x.Route)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(x => x.DestinationId);

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
