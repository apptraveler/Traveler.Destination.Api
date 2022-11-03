using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

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

        builder.Property("_averageSpendId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("AverageSpendId")
            .IsRequired();

        builder
            .HasOne(x => x.ClimateAverage)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey<Domain.Aggregates.DestinationAggregate.Destination>(x => x.Id);

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

        // builder.HasData(GenerateData());
    }

    private IEnumerable<Domain.Aggregates.DestinationAggregate.Destination> GenerateData()
    {
        // new Domain.Aggregates.DestinationAggregate.Destination("Toronto", "Esquiar, Algumas outras coisas", DestinationAverageSpend.High.Id, new DestinationClimateAverage(-11.0, -5.0, ClimateStatus.Snowing.Id))

        var coordinates = new[] {new RouteCoordinates(11.2888, 12.8754)};
        var images = new[] {"https://s1.static.brasilescola.uol.com.br/be/2021/04/canada-mapa.jpg"};
        var tags = new[] {DestinationTags.Beach, DestinationTags.Trails, DestinationTags.Waterfalls};


        var canada = new Domain.Aggregates.DestinationAggregate.Destination("Toronto", "Esquiar, Algumas outras coisas", DestinationAverageSpend.High.Id,
            new DestinationClimateAverage(-11.0, -5.0, ClimateStatus.Snowing.Id));

        var reykjavik = new Domain.Aggregates.DestinationAggregate.Destination( "Reykjavik", "Vulcões, Geisers", DestinationAverageSpend.Medium.Id, new DestinationClimateAverage(-20.0, 5.0, ClimateStatus.Cold.Id));

        // canada.Images.AddRange(images.Select(x => new DestinationImage(canada.Id, x)).ToList());
        // canada.Route.AddRange(route);

        return new[]
        {
            canada, reykjavik
        };
    }
}
