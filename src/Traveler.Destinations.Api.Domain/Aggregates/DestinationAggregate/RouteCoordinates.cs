using System;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

public class RouteCoordinates : Entity
{
    public Guid DestinationId { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    public RouteCoordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
