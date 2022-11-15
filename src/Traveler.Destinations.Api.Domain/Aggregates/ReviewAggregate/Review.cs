using System;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.ReviewAggregate;

public class Review : Entity, IAggregateRoot
{
    public Guid UserId { get; }
    public Guid DestinationId { get; }
    public string ReviewerName { get; }
    public string Message { get; }
    public Rate Rate { get; }

    public Review(Guid userId, Guid destinationId, string reviewerName, string message, int rate)
    {
        UserId = userId;
        DestinationId = destinationId;
        ReviewerName = reviewerName;
        Message = message;
        Rate = new Rate(rate);
    }

    private Review()
    {
    }
}
