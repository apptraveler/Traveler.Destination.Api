using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

public class ClimateStatus : Enumeration
{
    public static readonly ClimateStatus Hot = new(1, "Quente");
    public static readonly ClimateStatus Rainy = new(2, "Chuvoso");
    public static readonly ClimateStatus Snowing = new(3, "Nevando");
    public static readonly ClimateStatus Cold = new(4, "Frio");

    private ClimateStatus(int id, string name) : base(id, name)
    {
    }
}
