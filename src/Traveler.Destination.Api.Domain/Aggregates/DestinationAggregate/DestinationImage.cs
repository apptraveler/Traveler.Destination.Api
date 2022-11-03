using System;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

public class DestinationImage : Entity
{
    public Guid DestinationId { get; }
    public string Url { get; }

    public DestinationImage(Guid destinationId, string url)
    {
        DestinationId = destinationId;
        Url = url;
    }

    private DestinationImage()
    {
    }
}
