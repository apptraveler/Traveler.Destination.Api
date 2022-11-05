using System;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

public class DestinationTag : Entity
{
    public Guid DestinationId { get; private set; }
    public int TagId { get; }

    public DestinationTag(int tagId)
    {
        TagId = tagId;
    }

    private DestinationTag()
    {
    }
}
