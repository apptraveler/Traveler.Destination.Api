using System;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

public class DestinationImage : Entity
{
    public Guid DestinationId { get; }
    public string Url { get; }

    public DestinationImage(string url)
    {
        Url = url;
    }

    private DestinationImage()
    {
    }
}
