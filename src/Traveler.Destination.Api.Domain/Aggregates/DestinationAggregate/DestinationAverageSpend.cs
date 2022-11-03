using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

public class DestinationAverageSpend : Enumeration
{
    public static readonly DestinationAverageSpend Low = new(1, "Baixo");
    public static readonly DestinationAverageSpend Medium = new(2, "Médio");
    public static readonly DestinationAverageSpend High = new(3, "Alto");

    private DestinationAverageSpend(int id, string name) : base(id, name)
    {
    }
}
