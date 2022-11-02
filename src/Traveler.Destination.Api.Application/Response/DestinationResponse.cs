using System.Collections.Generic;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destination.Api.Application.Response;

public class DestinationResponse
{
    public string Name { get; }
    public string Description { get; }
    public DestinationTemperature Temperature { get; }
    public DestinationAverageSpend AverageSpend { get; }
    public List<DestinationImage> Images { get; }
    public ICollection<RouteCoordinates> Route { get; }
    public ICollection<DestinationTags> Tags { get; }

    public DestinationResponse(string name, string description, DestinationTemperature temperature, DestinationAverageSpend averageSpend, List<DestinationImage> images, ICollection<RouteCoordinates> route, ICollection<DestinationTags> tags)
    {
        Name = name;
        Description = description;
        Temperature = temperature;
        AverageSpend = averageSpend;
        Images = images;
        Route = route;
        Tags = tags;
    }
}
