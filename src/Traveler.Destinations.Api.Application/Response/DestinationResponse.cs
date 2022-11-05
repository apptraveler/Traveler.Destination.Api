using System;
using System.Collections.Generic;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destinations.Api.Application.Response;

public class DestinationResponse
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public ClimateAverageDto ClimateAverage { get; }
    public DestinationAverageSpend AverageSpend { get; }
    public List<string> Images { get; }
    public ICollection<RouteCoordinateDto> Route { get; }
    public ICollection<DestinationTags> Tags { get; }

    public DestinationResponse(Guid id, string name, string description, ClimateAverageDto climateAverage, DestinationAverageSpend averageSpend, List<string> images, ICollection<RouteCoordinateDto> route, ICollection<DestinationTags> tags)
    {
        Id = id;
        Name = name;
        Description = description;
        ClimateAverage = climateAverage;
        AverageSpend = averageSpend;
        Images = images;
        Route = route;
        Tags = tags;
    }
}

public class RouteCoordinateDto
{
    public double Latitude { get; }
    public double Longitude { get; }

    public RouteCoordinateDto(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}

public class ClimateAverageDto
{
    public double Min { get; }
    public double Max { get; }
    public ClimateStatus Status { get; }

    public ClimateAverageDto(double min, double max, ClimateStatus status)
    {
        Min = min;
        Max = max;
        Status = status;
    }
}
