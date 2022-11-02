using System;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.BookmarkedDestination;

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
