using System;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

public class RouteCoordinates : Entity
{
    public Guid DestinationId { get; }
    public string Name { get; }
    public string Description { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    public RouteCoordinates(string name, string description, double latitude, double longitude)
    {
        Name = name;
        Description = description;
        Latitude = latitude;
        Longitude = longitude;
    }

    private RouteCoordinates()
    {
    }
}
