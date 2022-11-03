using System;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

public class DestinationClimateAverage : Entity
{
    public Guid DestinationId { get; private set; }
    public double Min { get; }
    public double Max { get; }
    public int StatusId { get; }

    public DestinationClimateAverage(double min, double max, int statusId)
    {
        Min = min;
        Max = max;
        StatusId = statusId;
    }

    private DestinationClimateAverage()
    {
    }
}
