using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

public class DestinationTemperature : Enumeration
{
    private DestinationTemperature(int id, string name) : base(id, name)
    {
    }
}
