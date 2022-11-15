using System;
using System.Collections.Generic;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

namespace Traveler.Destinations.Api.Application.Response;

public class DestinationResponse
{
    public Guid Id { get; }
    public string Name { get; }
    public string City { get; }
    public string Country { get; }
    public string Description { get; }
    public bool Bookmarked { get; set; }
    public ClimateAverageDto ClimateAverage { get; }
    public DestinationAverageSpend AverageSpend { get; }
    public List<string> Images { get; }
    public ICollection<RouteCoordinateDto> Route { get; }
    public ICollection<DestinationTags> Tags { get; }

    public DestinationResponse(Guid id, string name, string city, string country, string description, ClimateAverageDto climateAverage, DestinationAverageSpend averageSpend, List<string> images, ICollection<RouteCoordinateDto> route, ICollection<DestinationTags> tags)
    {
        Id = id;
        Name = name;
        City = city;
        Country = country;
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
    public string Name { get; }
    public string Description { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    public RouteCoordinateDto(string name, string description, double latitude, double longitude)
    {
        Name = name;
        Description = description;
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
