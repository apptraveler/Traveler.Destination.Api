using System;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.ReviewAggregate;

public class Rate : ValueObject
{
    public int Value { get; }

    public Rate(int value)
    {
        if (value is > 5 or < 0) throw new ArgumentOutOfRangeException(nameof(Rate), "A nota de avaliação deve estar entre 0 e 5");

        Value = value;
    }
}
