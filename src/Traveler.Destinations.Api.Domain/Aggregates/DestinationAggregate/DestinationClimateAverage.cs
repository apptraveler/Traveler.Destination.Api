using System;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

public class DestinationClimateAverage : Entity
{
    public Guid DestinationId { get; }
    public int Min { get; }
    public int Max { get; }
    public int StatusId { get; }

    public DestinationClimateAverage(int min, int max)
    {
        Min = min;
        Max = max;
        StatusId = GenerateStatus().Id;
    }

    private ClimateStatus GenerateStatus()
    {
        if (Min < -10) return ClimateStatus.Snowing;

        if (Max >= 20) return ClimateStatus.Hot;

        return Min switch
        {
            > 0 => ClimateStatus.Rainy,
            < 0 => ClimateStatus.Cold,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private DestinationClimateAverage()
    {
    }
}
