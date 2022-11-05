using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

public class DestinationTags : Enumeration
{
    public static readonly DestinationTags Mountains = new(1, "Montanhas");
    public static readonly DestinationTags Beach = new(2, "Praias");
    public static readonly DestinationTags Waterfalls = new(3, "Cachoeiras");
    public static readonly DestinationTags Trails = new(4, "Trilhas");
    public static readonly DestinationTags TouristSpots = new(5, "Pontos Turísticos");
    public static readonly DestinationTags HistoricalPlaces = new(6, "Lugares Históricos");

    private DestinationTags(int id, string name) : base(id, name)
    {
    }
}
