using System;
using System.Collections.Generic;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

public class Destination : Entity, IAggregateRoot
{
    public string Name { get; }
    public string Description { get; }
    public DestinationTemperature Temperature { get; private set; }
    public DestinationAverageSpend AverageSpend { get; private set; }
    public List<DestinationImage> Images { get; }
    public List<RouteCoordinates> Route { get; }
    public List<DestinationTag> Tags { get; }

    private int _temperatureId;
    private int _averageSpendId;

    protected Destination(int temperatureId, int averageSpendId, string name, string description, List<DestinationImage> images, List<RouteCoordinates> route, List<DestinationTag> tags)
    {
        _temperatureId = temperatureId;
        _averageSpendId = averageSpendId;
        Name = name;
        Description = description;
        Images = images;
        Route = route;
        Tags = tags;
    }

    private Destination()
    {
    }

    public void AddTag(DestinationTags tag)
    {
        var destinationTag = new DestinationTag(Id, tag.Id);
        Tags.Add(destinationTag);
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddImages(IEnumerable<DestinationImage> images)
    {
        Images.AddRange(images);
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddRoute(RouteCoordinates route)
    {
        Route.Add(route);
        UpdatedAt = DateTime.UtcNow;
    }
}
