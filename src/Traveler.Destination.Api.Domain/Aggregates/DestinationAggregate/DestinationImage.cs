using System;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

public class DestinationImage : Entity
{
    public Guid DestinationId { get; }
    public string EncodedImage { get; }

    public DestinationImage(Guid destinationId, string encodedImage)
    {
        DestinationId = destinationId;
        EncodedImage = encodedImage;
    }

    private DestinationImage()
    {
    }
}
