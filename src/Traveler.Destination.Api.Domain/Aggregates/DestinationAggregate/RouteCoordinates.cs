using System;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

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
