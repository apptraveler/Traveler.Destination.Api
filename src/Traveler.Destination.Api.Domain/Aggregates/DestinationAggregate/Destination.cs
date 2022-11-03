using System;
using System.Collections.Generic;
using System.Linq;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

public class Destination : Entity, IAggregateRoot
{
    public string Name { get; }
    public string Description { get; }
    public DestinationClimateAverage ClimateAverage { get; }
    public DestinationAverageSpend AverageSpend { get; private set; }
    public List<DestinationImage> Images { get; } = new();
    public List<RouteCoordinates> Route { get; } = new();
    public List<DestinationTag> Tags { get; } = new();

    private int _averageSpendId;

    public Destination(string name, string description, int averageSpendId, DestinationClimateAverage climateAverage)
    {
        SetId();
        Name = name;
        Description = description;
        _averageSpendId = averageSpendId;
        ClimateAverage = climateAverage;
    }

    public Destination(string name, string description, int averageSpendId, DestinationClimateAverage climateAverage, IEnumerable<RouteCoordinates> route, IEnumerable<string> images, IEnumerable<DestinationTags> tags)
    {
        SetId();
        Name = name;
        Description = description;
        _averageSpendId = averageSpendId;
        ClimateAverage = climateAverage;

        Route.AddRange(route.ToList());
        Images.AddRange(images.Select(x => new DestinationImage(Id, x)).ToList());
        Tags.AddRange(tags.Select(x => new DestinationTag(Id, x.Id)).ToList());

        _averageSpendId = averageSpendId;
    }

    private Destination()
    {
    }
}
