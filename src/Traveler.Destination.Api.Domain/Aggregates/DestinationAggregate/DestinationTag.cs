using System;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

public class DestinationTag : Entity
{
    public Guid DestinationId { get; private set; }
    public int TagId { get; }

    public DestinationTag(Guid destinationId, int tagId)
    {
        DestinationId = destinationId;
        TagId = tagId;
    }

    private DestinationTag()
    {
    }
}
