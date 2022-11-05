using System;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.BookmarkedDestination;

public class BookmarkedDestination : Entity, IAggregateRoot
{
    public Guid UserId { get; }
    public Guid DestinationId { get; }

    public BookmarkedDestination(Guid userId, Guid destinationId)
    {
        UserId = userId;
        DestinationId = destinationId;
    }

    private BookmarkedDestination()
    {
    }
}
