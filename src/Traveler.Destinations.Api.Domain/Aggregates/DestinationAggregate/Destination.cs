using System.Collections.Generic;
using System.Linq;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

public class Destination : Entity, IAggregateRoot
{
    public string Name { get; }
    public string City { get; }
    public string Country { get; }
    public string Description { get; }
    public DestinationClimateAverage ClimateAverage { get; }
    public DestinationAverageSpend AverageSpend { get; private set; }
    public List<DestinationImage> Images { get; } = new();
    public List<RouteCoordinates> Route { get; } = new();
    public List<DestinationTag> Tags { get; } = new();

    private int _averageSpendId;

    public Destination(string name, string city, string country, string description, DestinationAverageSpend averageSpend, DestinationClimateAverage climateAverage)
    {
        SetId();
        Name = name;
        City = city;
        Country = country;
        Description = description;
        _averageSpendId = averageSpend.Id;
        ClimateAverage = climateAverage;
    }

    public Destination(
        string name,
        string city,
        string country,
        string description,
        DestinationAverageSpend averageSpend,
        DestinationClimateAverage climateAverage,
        IEnumerable<RouteCoordinates> route,
        IEnumerable<DestinationImage> images,
        IEnumerable<DestinationTags> tags
    )
    {
        SetId();
        Name = name;
        City = city;
        Country = country;
        Description = description;
        _averageSpendId = averageSpend.Id;
        ClimateAverage = climateAverage;

        Route.AddRange(route);
        Images.AddRange(images);
        Tags.AddRange(tags.Select(x => new DestinationTag(x.Id)));
    }

    private Destination()
    {
    }
}
