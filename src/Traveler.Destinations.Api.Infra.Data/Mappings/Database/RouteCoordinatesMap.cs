﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destinations.Api.Infra.Data.Mappings.Database;

public class RouteCoordinatesMap : IEntityTypeConfiguration<RouteCoordinates>
{
    public void Configure(EntityTypeBuilder<RouteCoordinates> builder)
    {
        builder.ToTable("RouteCoordinates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Name")
            .IsRequired();

        builder.Property(x => x.Description)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Description")
            .IsRequired();

        builder.Property(x => x.Latitude)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Latitude")
            .IsRequired();

        builder.Property(x => x.Longitude)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Longitude")
            .IsRequired();

        builder.HasOne<Destination>()
            .WithMany(x => x.Route)
            .HasForeignKey(x => x.DestinationId);
    }
}
